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

        public ConnectionHost(int port) {

            listener = new TcpListener(IPAddress.Any, port);
            cancel = new CancellationTokenSource();
            clients = new List<TcpClient>();
            listener.Start();

            ListenForConnections();
            
        }

        public void CancelListen() {
            cancel.Cancel();
        }

        async private void ListenForConnections() {
            while (!cancel.IsCancellationRequested) {

                try {
                    TcpClient temp = await listener.AcceptTcpClientAsync(cancel.Token);
                    temp.NoDelay = true;
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
                int bytesRead = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken);
                if (bytesRead == 0) break;

                // Handle received data
            }

            clients.Remove(client);
            client.Close();

        }

        public async Task SendToAllClientsAsync(byte[] payload) {
            foreach (TcpClient client in clients) {
                try {
                    using NetworkStream stream = client.GetStream();
                    await stream.WriteAsync(BitConverter.GetBytes(payload.Length), 0, 4);
                    await stream.WriteAsync(payload, 0, payload.Length);
                } catch (Exception ex) {
                    Debug.WriteLine($"Exception in SendToAllClientsAsync:\n{ex.Message}");
                }
            }
        }

    }
}
