using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlockStudioCS {
    internal class ConnectionHost {

        private readonly TcpListener listener;
        private readonly List<TcpClient> clients;

        private readonly CancellationTokenSource cancel;

        public delegate void PacketHandler(Packet p);
        private PacketHandler Handler;

        public ConnectionHost(int port, PacketHandler handler) {

            Handler = handler;

            listener = new TcpListener(IPAddress.Any, port);
            cancel = new CancellationTokenSource();
            clients = new List<TcpClient>();
            listener.Start();

            ListenForConnections();
            
        }

        public void CancelListen() {
            cancel.Cancel();
            listener.Stop();
            listener.Dispose();
        }

        async private void ListenForConnections() {
            while (!cancel.IsCancellationRequested) {

                try {
                    TcpClient temp = await listener.AcceptTcpClientAsync(cancel.Token);
                    temp.NoDelay = true;
                    HandleClientAsync(temp, cancel.Token);
                    clients.Add(temp);
                } catch (Exception ex) {
                    Debug.WriteLine($"Exception in ListenForConnections:\n{ex.Message}");
                }

            }
        }

        private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken) {

            using NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            while (!cancellationToken.IsCancellationRequested) {
                var bgruh = await Packet.ReceiveAsync(stream, cancellationToken);
                //int bytesRead = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken);
                //if (bytesRead == 0) break;

                Handler.Invoke(bgruh);
                SendToAllClientsAsync(bgruh);
                //Debug.WriteLine($"Recieved: {Encoding.UTF8.GetString(buffer, 0, bytesRead)}");
            }

            clients.Remove(client);
            client.Close();

        }

        // You can call this to broadcast a Packet object:
        public async Task SendToAllClientsAsync(Packet packet, CancellationToken ct = default) {
            // 1) Serialize once, up front
            byte[] body = packet.ToBytes();
            byte[] prefix = BitConverter.GetBytes(body.Length);
            //  If you need big‐endian, wrap with IPAddress.HostToNetworkOrder, but
            //  make sure your receiver calls IPAddress.NetworkToHostOrder.

            // 2) For each client, grab the NetworkStream (but DO NOT dispose it here)
            var sendTasks = new List<Task>(clients.Count);
            foreach (var client in clients) {
                // If the client has already disconnected, skip it
                if (!client.Connected)
                    continue;
                Debug.WriteLine($"{client.Client.LocalEndPoint}");
                NetworkStream stream = client.GetStream();
                sendTasks.Add(SendToSingleClientAsync(stream, prefix, body, ct));
            }

            // 3) Wait for all the sends to complete (or fail) in “parallel”
            //    This ensures a slow client doesn’t block the others.
            try {
                await Task.WhenAll(sendTasks);
            } catch {
                // We’re already catching per‐client errors below, so
                // this will usually be empty. You can log or ignore.
            }
        }

        private async Task SendToSingleClientAsync(
            NetworkStream stream,
            byte[] prefix,
            byte[] body,
            CancellationToken ct
        ) {
            try {
                // Write length‐prefix (4 bytes), then the actual packet data
                await stream.WriteAsync(prefix, 0, prefix.Length, ct);
                await stream.WriteAsync(body, 0, body.Length, ct);
            } catch (IOException ioEx) {
                Debug.WriteLine($"[SendToClient] I/O error: {ioEx.Message}");
                // If this fails, you may want to mark that client for removal, e.g.:
                //   client.Dispose();  // close the socket
                //   clients.Remove(associatedTcpClient);
            } catch (Exception ex) {
                Debug.WriteLine($"[SendToClient] Unexpected error: {ex}");
            }
        }

    }
}
