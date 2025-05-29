using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteBlockStudioCS {
    public partial class ConnectDialog: Form {

        TcpClient tempSocket;
        public TcpClient ConnectionSocket;
        public string ConnectionMode;
        public int PortUsed;

        public ConnectDialog() {
            InitializeComponent();
        }

        private void CheckedChanged(object sender, EventArgs e) {
            tbx_Address.Enabled = rad_Join.Checked;
        }

        async private void btn_Confirm_Click(object sender, EventArgs e) {

            string address = tbx_Address.Text;
            int port;

            if (int.TryParse(tbx_Port.Text, out port)) {
                if (rad_Host.Checked) {
                    ConnectionMode = "host";
                    if (port >= 1) {
                        PortUsed = port;
                    } else {
                        return;
                    }
                } else {
                    ConnectionMode = "join";
                    if (port >= 1 && address.Length >= 1) {

                        try {
                            tempSocket = new TcpClient();
                            btn_Confirm.Enabled = false;
                            //btn_Cancel.Enabled = false;
                            ControlBox = false;
                            await tempSocket.ConnectAsync(address, port);
                            btn_Confirm.Enabled = true;
                            //btn_Cancel.Enabled = true;
                            ControlBox = true;
                            ConnectionSocket = tempSocket;
                            //DialogResult = DialogResult.OK;
                            //Close();

                        } catch (SocketException exc) {
                            Console.WriteLine($"SocketException: {exc.Message}");
                        } catch (Exception exc) {
                            Console.WriteLine($"Exception: {exc.Message}");
                        }

                    } else {
                        return;
                    }
                }

                DialogResult = DialogResult.OK;
            }

        }
    }
}
