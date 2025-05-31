using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

namespace NoteBlockStudioCS {
    public partial class Form1: Form {

        private ConnectionHost server;
        private TcpClient client;

        private bool connected = false;
        private bool hosting = false;

        private ConnectionHost.PacketHandler handler;

        private CancellationTokenSource ctsClient;

        private void tsi_HostJoin_Click(object sender, EventArgs e) {
            ConnectDialog dlg = new ConnectDialog();

            if(dlg.ShowDialog() == DialogResult.OK) {
                connected = true;
                if (dlg.ConnectionMode == "host") {
                    hosting = true;
                    tsi_HostJoin.Enabled = false;
                    handler = PacketHandlerMethod;
                    server = new ConnectionHost(dlg.PortUsed, handler);
                    tsi_StopHosting.Enabled = true;
                } else if (dlg.ConnectionMode == "join") {
                    hosting = false;
                    client = dlg.ConnectionSocket;
                    ctsClient = new CancellationTokenSource();
                    _ = ClientReceiveLoopAsync(ctsClient.Token);
                }
            }
        }

        private async Task ClientReceiveLoopAsync(CancellationToken ct) {
            try {
                // 1) Make sure client is non‐null and Connected
                if (client == null || !client.Connected)
                    return;

                NetworkStream stream = client.GetStream();

                // 2) Buffer loop: keep reading until the token is cancelled or the socket closes
                while (!ct.IsCancellationRequested) {

                    Debug.WriteLine("client receive waiting");
                    // This will await until a complete packet (length-prefix + payload) arrives
                    Packet packet = await Packet.ReceiveAsync(stream, ct);
                    Debug.WriteLine($"????????????????????????????? {packet.Type}");

                    // Dispatch it to your handler (which already does InvokeRequired check, etc.)
                    PacketHandlerMethod(packet);
                }
            } catch (OperationCanceledException) {
                // The loop was cancelled (ctsClient.Cancel()) — just exit cleanly
            } catch (IOException) {
                // Network error or server disconnected
                // You can set connected = false, show a “disconnected” message, etc.
            } catch (Exception ex) {
                // Unexpected exception – you might log or show MessageBox here
                Debug.WriteLine($"[ClientReceiveLoop] Exception: {ex}");
            } finally {
                // Clean up when exiting
                if (client != null) {
                    client.Close();
                    client = null;
                    connected = false;
                }
            }
        }


        private void tsi_StopHosting_Click(object sender, EventArgs e) {
            if(connected && hosting) {
                server.CancelListen();
            }
        }

        async private void SendToServer(Packet p) {

            if (client != null) {
                try {
                    NetworkStream ns = client.GetStream();

                    await SendPacketAsync(ns, p);
                } catch (Exception ex) {

                }
            }

        }


        /// <summary>
        /// Takes a Packet, serializes it (type+payload), and sends it with a 4-byte length prefix.
        /// </summary>
        public static async Task SendPacketAsync(NetworkStream stream, Packet packet) {
            // 1) Serialize the packet into [ type(1 byte) | payload… ]
            byte[] body = packet.ToBytes();

            // 2) Compute a 4-byte length prefix
            //    (this prefix describes exactly how many bytes follow)
            int length = body.Length;
            byte[] lengthPrefix = BitConverter.GetBytes(length);
            // (By default this is little-endian. 
            //  If you need big-endian, use IPAddress.HostToNetworkOrder.)

            // 3) Write the prefix, then the body
            await stream.WriteAsync(lengthPrefix, 0, lengthPrefix.Length);
            await stream.WriteAsync(body, 0, body.Length);
        }


        private void PacketHandlerMethod(Packet packet) {

            // If called from a background thread, marshal to the UI thread
            if (this.InvokeRequired) {
                this.BeginInvoke(new Action(() => PacketHandlerMethod(packet)));
                return;
            }

            // Handle received data
            Debug.WriteLine($"{packet.Type}");

            switch (packet) {
                case PlaceNotePacket p:
                    Debug.WriteLine($"PlaceNote: X={p.X}, Y={p.Y}, Ins={p.Instrument}, Key={p.Key}");
                    AddBlock(p.X, p.Y, new NoteBlock(p.X, p.Y, (sbyte)p.Instrument, p.Key, 100, 100, 0), true);
                    break;

                case RemoveNotePacket r:
                    Debug.WriteLine($"RemoveNote: X={r.X}, Y={r.Y}");
                    RemoveBlock(r.X, r.Y, true);
                    break;

                case EditNotePacket e:
                    Debug.WriteLine($"EditNote: X={e.X}, Y={e.Y}, Instrument={e.Instrument}, Key={e.Key}, Velocity={e.Velocity}, Panning:{e.Panning}");
                    break;

                case EditLayerPacket l:
                    Debug.WriteLine($"EditLayer: layer={l.Layer}, Velocity={l.Velocity}, Stereo={l.Stereo}");
                    break;

                case ChangeTempoPacket t:
                    Debug.WriteLine($"ChangeTempo: Tempo={t.Tempo}");
                    break;

                default:
                    throw new InvalidDataException($"Unknown packet type: {packet.Type}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
            if (ctsClient != null && !ctsClient.IsCancellationRequested) {
                ctsClient.Cancel();
            }

            // If you’re hosting too, stop the server
            if (hosting && server != null) {
                server.CancelListen();
            }
        }

    }
}