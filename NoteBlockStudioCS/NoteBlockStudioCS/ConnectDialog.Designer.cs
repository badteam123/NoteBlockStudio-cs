namespace NoteBlockStudioCS {
    partial class ConnectDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            groupBox1 = new GroupBox();
            rad_Join = new RadioButton();
            rad_Host = new RadioButton();
            label1 = new Label();
            tbx_Port = new TextBox();
            tbx_Address = new TextBox();
            label2 = new Label();
            btn_Confirm = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rad_Join);
            groupBox1.Controls.Add(rad_Host);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(185, 71);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Connection Mode";
            // 
            // rad_Join
            // 
            rad_Join.AutoSize = true;
            rad_Join.Checked = true;
            rad_Join.Location = new Point(6, 47);
            rad_Join.Name = "rad_Join";
            rad_Join.Size = new Size(46, 19);
            rad_Join.TabIndex = 1;
            rad_Join.TabStop = true;
            rad_Join.Text = "Join";
            rad_Join.UseVisualStyleBackColor = true;
            // 
            // rad_Host
            // 
            rad_Host.AutoSize = true;
            rad_Host.Location = new Point(6, 22);
            rad_Host.Name = "rad_Host";
            rad_Host.Size = new Size(50, 19);
            rad_Host.TabIndex = 0;
            rad_Host.Text = "Host";
            rad_Host.UseVisualStyleBackColor = true;
            rad_Host.CheckedChanged += CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 158);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 1;
            label1.Text = "Port:";
            // 
            // tbx_Port
            // 
            tbx_Port.Location = new Point(12, 176);
            tbx_Port.Name = "tbx_Port";
            tbx_Port.PlaceholderText = "Port";
            tbx_Port.Size = new Size(185, 23);
            tbx_Port.TabIndex = 2;
            tbx_Port.Text = "25565";
            // 
            // tbx_Address
            // 
            tbx_Address.Location = new Point(12, 122);
            tbx_Address.Name = "tbx_Address";
            tbx_Address.PlaceholderText = "Address";
            tbx_Address.Size = new Size(185, 23);
            tbx_Address.TabIndex = 4;
            tbx_Address.Text = "localhost";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 104);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 3;
            label2.Text = "Address:";
            // 
            // btn_Confirm
            // 
            btn_Confirm.Location = new Point(12, 216);
            btn_Confirm.Name = "btn_Confirm";
            btn_Confirm.Size = new Size(75, 23);
            btn_Confirm.TabIndex = 5;
            btn_Confirm.Text = "Confirm";
            btn_Confirm.UseVisualStyleBackColor = true;
            btn_Confirm.Click += btn_Confirm_Click;
            // 
            // ConnectDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(209, 251);
            Controls.Add(btn_Confirm);
            Controls.Add(tbx_Address);
            Controls.Add(label2);
            Controls.Add(tbx_Port);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Name = "ConnectDialog";
            Text = "ConnectDialog";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private RadioButton rad_Host;
        private RadioButton rad_Join;
        private Label label1;
        private TextBox tbx_Port;
        private TextBox tbx_Address;
        private Label label2;
        private Button btn_Confirm;
    }
}