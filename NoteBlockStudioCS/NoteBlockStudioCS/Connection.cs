using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NoteBlockStudioCS {
    public partial class Form1: Form {

        private ConnectionHost server;
        private TcpClient client;

        private bool connected = false;

        private void tsi_HostJoin_Click(object sender, EventArgs e) {
            ConnectDialog dlg = new ConnectDialog();

            if(dlg.ShowDialog() == DialogResult.OK) {
                connected = true;
                if (dlg.ConnectionMode == "host") {
                    server = new ConnectionHost(dlg.PortUsed);
                } else if (dlg.ConnectionMode == "join") {
                    client = dlg.ConnectionSocket;
                }
            }
        }

        private void SendToServer() {

        }

    }
}