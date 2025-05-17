namespace NoteBlockStudioCS {
    partial class Form1 {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newSongToolStripMenuItem = new ToolStripMenuItem();
            openSongToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            btnSave = new Button();
            btnPlay = new Button();
            btnStop = new Button();
            btnReverse = new Button();
            btnForward = new Button();
            label1 = new Label();
            label2 = new Label();
            num_TPS = new NumericUpDown();
            label3 = new Label();
            textBox1 = new TextBox();
            picBox = new PictureBox();
            vScrollBar = new VScrollBar();
            hScrollBar = new HScrollBar();
            btnHarp = new Button();
            btnDBass = new Button();
            btnBDrum = new Button();
            btnSnare = new Button();
            btnBell = new Button();
            btnFlute = new Button();
            btnGuitar = new Button();
            btnClick = new Button();
            btnCowbell = new Button();
            btnIronXylophone = new Button();
            btnXylophone = new Button();
            btnChime = new Button();
            btnPling = new Button();
            btnBanjo = new Button();
            btnBit = new Button();
            btnDidgeridoo = new Button();
            volumeBar = new TrackBar();
            label4 = new Label();
            lblVolume = new Label();
            openFileDialog1 = new OpenFileDialog();
            menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_TPS).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)volumeBar).BeginInit();
            SuspendLayout();
            // 
            // menu
            // 
            menu.BackgroundImage = (Image)resources.GetObject("menu.BackgroundImage");
            menu.BackgroundImageLayout = ImageLayout.None;
            menu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, settingsToolStripMenuItem });
            menu.Location = new Point(0, 0);
            menu.Name = "menu";
            menu.Padding = new Padding(7, 2, 0, 2);
            menu.Size = new Size(782, 24);
            menu.TabIndex = 0;
            menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newSongToolStripMenuItem, openSongToolStripMenuItem });
            fileToolStripMenuItem.ForeColor = Color.DimGray;
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newSongToolStripMenuItem
            // 
            newSongToolStripMenuItem.Name = "newSongToolStripMenuItem";
            newSongToolStripMenuItem.Size = new Size(133, 22);
            newSongToolStripMenuItem.Text = "New Song";
            // 
            // openSongToolStripMenuItem
            // 
            openSongToolStripMenuItem.Name = "openSongToolStripMenuItem";
            openSongToolStripMenuItem.Size = new Size(133, 22);
            openSongToolStripMenuItem.Text = "Open Song";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.ForeColor = Color.DimGray;
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.ForeColor = Color.DimGray;
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // btnSave
            // 
            btnSave.BackgroundImage = (Image)resources.GetObject("btnSave.BackgroundImage");
            btnSave.BackgroundImageLayout = ImageLayout.Stretch;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(12, 36);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(18, 17);
            btnSave.TabIndex = 0;
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            btnPlay.BackgroundImage = (Image)resources.GetObject("btnPlay.BackgroundImage");
            btnPlay.BackgroundImageLayout = ImageLayout.Stretch;
            btnPlay.FlatAppearance.BorderSize = 0;
            btnPlay.FlatStyle = FlatStyle.Flat;
            btnPlay.Location = new Point(36, 36);
            btnPlay.Margin = new Padding(4, 3, 4, 3);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(18, 17);
            btnPlay.TabIndex = 1;
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // btnStop
            // 
            btnStop.BackgroundImage = (Image)resources.GetObject("btnStop.BackgroundImage");
            btnStop.BackgroundImageLayout = ImageLayout.Stretch;
            btnStop.FlatAppearance.BorderSize = 0;
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Location = new Point(61, 36);
            btnStop.Margin = new Padding(4, 3, 4, 3);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(18, 17);
            btnStop.TabIndex = 2;
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnReverse
            // 
            btnReverse.BackgroundImage = (Image)resources.GetObject("btnReverse.BackgroundImage");
            btnReverse.BackgroundImageLayout = ImageLayout.Stretch;
            btnReverse.FlatAppearance.BorderSize = 0;
            btnReverse.FlatStyle = FlatStyle.Flat;
            btnReverse.Location = new Point(85, 36);
            btnReverse.Margin = new Padding(4, 3, 4, 3);
            btnReverse.Name = "btnReverse";
            btnReverse.Size = new Size(18, 17);
            btnReverse.TabIndex = 3;
            btnReverse.UseVisualStyleBackColor = true;
            // 
            // btnForward
            // 
            btnForward.BackgroundImage = (Image)resources.GetObject("btnForward.BackgroundImage");
            btnForward.BackgroundImageLayout = ImageLayout.Stretch;
            btnForward.FlatAppearance.BorderSize = 0;
            btnForward.FlatStyle = FlatStyle.Flat;
            btnForward.Location = new Point(110, 36);
            btnForward.Margin = new Padding(4, 3, 4, 3);
            btnForward.Name = "btnForward";
            btnForward.Size = new Size(18, 17);
            btnForward.TabIndex = 4;
            btnForward.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(7, 61);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(113, 23);
            label1.TabIndex = 5;
            label1.Text = "00:00:00.000";
            // 
            // label2
            // 
            label2.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(24, 82);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 6;
            label2.Text = "/ 00:00:00.000";
            // 
            // num_TPS
            // 
            num_TPS.BackColor = Color.FromArgb(20, 20, 20);
            num_TPS.BorderStyle = BorderStyle.FixedSingle;
            num_TPS.DecimalPlaces = 2;
            num_TPS.ForeColor = Color.White;
            num_TPS.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            num_TPS.Location = new Point(127, 67);
            num_TPS.Margin = new Padding(4, 3, 4, 3);
            num_TPS.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            num_TPS.Minimum = new decimal(new int[] { 25, 0, 0, 131072 });
            num_TPS.Name = "num_TPS";
            num_TPS.Size = new Size(59, 23);
            num_TPS.TabIndex = 7;
            num_TPS.TextAlign = HorizontalAlignment.Center;
            num_TPS.UpDownAlign = LeftRightAlignment.Left;
            num_TPS.Value = new decimal(new int[] { 10, 0, 0, 0 });
            num_TPS.ValueChanged += num_TPS_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(189, 69);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(21, 15);
            label3.TabIndex = 8;
            label3.Text = "t/s";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(20, 20, 20);
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(212, 67);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(54, 23);
            textBox1.TabIndex = 9;
            textBox1.Text = "1, 1, 1";
            textBox1.TextAlign = HorizontalAlignment.Right;
            // 
            // picBox
            // 
            picBox.BackColor = Color.FromArgb(40, 40, 40);
            picBox.BackgroundImageLayout = ImageLayout.None;
            picBox.BorderStyle = BorderStyle.FixedSingle;
            picBox.Location = new Point(274, 67);
            picBox.Margin = new Padding(4, 3, 4, 3);
            picBox.Name = "picBox";
            picBox.Size = new Size(480, 256);
            picBox.TabIndex = 10;
            picBox.TabStop = false;
            picBox.LoadCompleted += picBox_LoadCompleted;
            picBox.Paint += picBox_Paint;
            picBox.MouseClick += picBox_MouseClick;
            // 
            // vScrollBar
            // 
            vScrollBar.Location = new Point(758, 67);
            vScrollBar.Name = "vScrollBar";
            vScrollBar.Size = new Size(17, 256);
            vScrollBar.TabIndex = 13;
            // 
            // hScrollBar
            // 
            hScrollBar.LargeChange = 1;
            hScrollBar.Location = new Point(274, 326);
            hScrollBar.Maximum = 0;
            hScrollBar.Name = "hScrollBar";
            hScrollBar.Size = new Size(480, 14);
            hScrollBar.TabIndex = 14;
            hScrollBar.Scroll += hScrollBar_Scroll;
            // 
            // btnHarp
            // 
            btnHarp.BackgroundImage = (Image)resources.GetObject("btnHarp.BackgroundImage");
            btnHarp.BackgroundImageLayout = ImageLayout.Stretch;
            btnHarp.FlatAppearance.BorderSize = 0;
            btnHarp.FlatStyle = FlatStyle.Flat;
            btnHarp.Location = new Point(168, 35);
            btnHarp.Margin = new Padding(4, 3, 4, 3);
            btnHarp.Name = "btnHarp";
            btnHarp.Size = new Size(19, 18);
            btnHarp.TabIndex = 15;
            btnHarp.UseVisualStyleBackColor = true;
            // 
            // btnDBass
            // 
            btnDBass.BackgroundImage = (Image)resources.GetObject("btnDBass.BackgroundImage");
            btnDBass.BackgroundImageLayout = ImageLayout.Stretch;
            btnDBass.FlatAppearance.BorderSize = 0;
            btnDBass.FlatStyle = FlatStyle.Flat;
            btnDBass.Location = new Point(194, 35);
            btnDBass.Margin = new Padding(4, 3, 4, 3);
            btnDBass.Name = "btnDBass";
            btnDBass.Size = new Size(19, 18);
            btnDBass.TabIndex = 16;
            btnDBass.UseVisualStyleBackColor = true;
            // 
            // btnBDrum
            // 
            btnBDrum.BackgroundImage = (Image)resources.GetObject("btnBDrum.BackgroundImage");
            btnBDrum.BackgroundImageLayout = ImageLayout.Stretch;
            btnBDrum.FlatAppearance.BorderSize = 0;
            btnBDrum.FlatStyle = FlatStyle.Flat;
            btnBDrum.Location = new Point(219, 35);
            btnBDrum.Margin = new Padding(4, 3, 4, 3);
            btnBDrum.Name = "btnBDrum";
            btnBDrum.Size = new Size(19, 18);
            btnBDrum.TabIndex = 17;
            btnBDrum.UseVisualStyleBackColor = true;
            // 
            // btnSnare
            // 
            btnSnare.BackgroundImage = (Image)resources.GetObject("btnSnare.BackgroundImage");
            btnSnare.BackgroundImageLayout = ImageLayout.Stretch;
            btnSnare.FlatAppearance.BorderSize = 0;
            btnSnare.FlatStyle = FlatStyle.Flat;
            btnSnare.Location = new Point(245, 35);
            btnSnare.Margin = new Padding(4, 3, 4, 3);
            btnSnare.Name = "btnSnare";
            btnSnare.Size = new Size(19, 18);
            btnSnare.TabIndex = 18;
            btnSnare.UseVisualStyleBackColor = true;
            // 
            // btnBell
            // 
            btnBell.BackgroundImage = (Image)resources.GetObject("btnBell.BackgroundImage");
            btnBell.BackgroundImageLayout = ImageLayout.Stretch;
            btnBell.FlatAppearance.BorderSize = 0;
            btnBell.FlatStyle = FlatStyle.Flat;
            btnBell.Location = new Point(348, 35);
            btnBell.Margin = new Padding(4, 3, 4, 3);
            btnBell.Name = "btnBell";
            btnBell.Size = new Size(19, 18);
            btnBell.TabIndex = 22;
            btnBell.UseVisualStyleBackColor = true;
            // 
            // btnFlute
            // 
            btnFlute.BackgroundImage = (Image)resources.GetObject("btnFlute.BackgroundImage");
            btnFlute.BackgroundImageLayout = ImageLayout.Stretch;
            btnFlute.FlatAppearance.BorderSize = 0;
            btnFlute.FlatStyle = FlatStyle.Flat;
            btnFlute.Location = new Point(322, 35);
            btnFlute.Margin = new Padding(4, 3, 4, 3);
            btnFlute.Name = "btnFlute";
            btnFlute.Size = new Size(19, 18);
            btnFlute.TabIndex = 21;
            btnFlute.UseVisualStyleBackColor = true;
            // 
            // btnGuitar
            // 
            btnGuitar.BackgroundImage = (Image)resources.GetObject("btnGuitar.BackgroundImage");
            btnGuitar.BackgroundImageLayout = ImageLayout.Stretch;
            btnGuitar.FlatAppearance.BorderSize = 0;
            btnGuitar.FlatStyle = FlatStyle.Flat;
            btnGuitar.Location = new Point(296, 35);
            btnGuitar.Margin = new Padding(4, 3, 4, 3);
            btnGuitar.Name = "btnGuitar";
            btnGuitar.Size = new Size(19, 18);
            btnGuitar.TabIndex = 20;
            btnGuitar.UseVisualStyleBackColor = true;
            // 
            // btnClick
            // 
            btnClick.BackgroundImage = (Image)resources.GetObject("btnClick.BackgroundImage");
            btnClick.BackgroundImageLayout = ImageLayout.Stretch;
            btnClick.FlatAppearance.BorderSize = 0;
            btnClick.FlatStyle = FlatStyle.Flat;
            btnClick.Location = new Point(271, 35);
            btnClick.Margin = new Padding(4, 3, 4, 3);
            btnClick.Name = "btnClick";
            btnClick.Size = new Size(19, 18);
            btnClick.TabIndex = 19;
            btnClick.UseVisualStyleBackColor = true;
            // 
            // btnCowbell
            // 
            btnCowbell.BackgroundImage = (Image)resources.GetObject("btnCowbell.BackgroundImage");
            btnCowbell.BackgroundImageLayout = ImageLayout.Stretch;
            btnCowbell.FlatAppearance.BorderSize = 0;
            btnCowbell.FlatStyle = FlatStyle.Flat;
            btnCowbell.Location = new Point(450, 35);
            btnCowbell.Margin = new Padding(4, 3, 4, 3);
            btnCowbell.Name = "btnCowbell";
            btnCowbell.Size = new Size(19, 18);
            btnCowbell.TabIndex = 26;
            btnCowbell.UseVisualStyleBackColor = true;
            // 
            // btnIronXylophone
            // 
            btnIronXylophone.BackgroundImage = (Image)resources.GetObject("btnIronXylophone.BackgroundImage");
            btnIronXylophone.BackgroundImageLayout = ImageLayout.Stretch;
            btnIronXylophone.FlatAppearance.BorderSize = 0;
            btnIronXylophone.FlatStyle = FlatStyle.Flat;
            btnIronXylophone.Location = new Point(425, 35);
            btnIronXylophone.Margin = new Padding(4, 3, 4, 3);
            btnIronXylophone.Name = "btnIronXylophone";
            btnIronXylophone.Size = new Size(19, 18);
            btnIronXylophone.TabIndex = 25;
            btnIronXylophone.UseVisualStyleBackColor = true;
            // 
            // btnXylophone
            // 
            btnXylophone.BackgroundImage = (Image)resources.GetObject("btnXylophone.BackgroundImage");
            btnXylophone.BackgroundImageLayout = ImageLayout.Stretch;
            btnXylophone.FlatAppearance.BorderSize = 0;
            btnXylophone.FlatStyle = FlatStyle.Flat;
            btnXylophone.Location = new Point(399, 35);
            btnXylophone.Margin = new Padding(4, 3, 4, 3);
            btnXylophone.Name = "btnXylophone";
            btnXylophone.Size = new Size(19, 18);
            btnXylophone.TabIndex = 24;
            btnXylophone.UseVisualStyleBackColor = true;
            // 
            // btnChime
            // 
            btnChime.BackgroundImage = (Image)resources.GetObject("btnChime.BackgroundImage");
            btnChime.BackgroundImageLayout = ImageLayout.Stretch;
            btnChime.FlatAppearance.BorderSize = 0;
            btnChime.FlatStyle = FlatStyle.Flat;
            btnChime.Location = new Point(373, 35);
            btnChime.Margin = new Padding(4, 3, 4, 3);
            btnChime.Name = "btnChime";
            btnChime.Size = new Size(19, 18);
            btnChime.TabIndex = 23;
            btnChime.UseVisualStyleBackColor = true;
            // 
            // btnPling
            // 
            btnPling.BackgroundImage = (Image)resources.GetObject("btnPling.BackgroundImage");
            btnPling.BackgroundImageLayout = ImageLayout.Stretch;
            btnPling.FlatAppearance.BorderSize = 0;
            btnPling.FlatStyle = FlatStyle.Flat;
            btnPling.Location = new Point(553, 35);
            btnPling.Margin = new Padding(4, 3, 4, 3);
            btnPling.Name = "btnPling";
            btnPling.Size = new Size(19, 18);
            btnPling.TabIndex = 30;
            btnPling.UseVisualStyleBackColor = true;
            // 
            // btnBanjo
            // 
            btnBanjo.BackgroundImage = (Image)resources.GetObject("btnBanjo.BackgroundImage");
            btnBanjo.BackgroundImageLayout = ImageLayout.Stretch;
            btnBanjo.FlatAppearance.BorderSize = 0;
            btnBanjo.FlatStyle = FlatStyle.Flat;
            btnBanjo.Location = new Point(527, 35);
            btnBanjo.Margin = new Padding(4, 3, 4, 3);
            btnBanjo.Name = "btnBanjo";
            btnBanjo.Size = new Size(19, 18);
            btnBanjo.TabIndex = 29;
            btnBanjo.UseVisualStyleBackColor = true;
            // 
            // btnBit
            // 
            btnBit.BackgroundImage = (Image)resources.GetObject("btnBit.BackgroundImage");
            btnBit.BackgroundImageLayout = ImageLayout.Stretch;
            btnBit.FlatAppearance.BorderSize = 0;
            btnBit.FlatStyle = FlatStyle.Flat;
            btnBit.Location = new Point(502, 35);
            btnBit.Margin = new Padding(4, 3, 4, 3);
            btnBit.Name = "btnBit";
            btnBit.Size = new Size(19, 18);
            btnBit.TabIndex = 28;
            btnBit.UseVisualStyleBackColor = true;
            // 
            // btnDidgeridoo
            // 
            btnDidgeridoo.BackgroundImage = (Image)resources.GetObject("btnDidgeridoo.BackgroundImage");
            btnDidgeridoo.BackgroundImageLayout = ImageLayout.Stretch;
            btnDidgeridoo.FlatAppearance.BorderSize = 0;
            btnDidgeridoo.FlatStyle = FlatStyle.Flat;
            btnDidgeridoo.Location = new Point(476, 35);
            btnDidgeridoo.Margin = new Padding(4, 3, 4, 3);
            btnDidgeridoo.Name = "btnDidgeridoo";
            btnDidgeridoo.Size = new Size(19, 18);
            btnDidgeridoo.TabIndex = 27;
            btnDidgeridoo.UseVisualStyleBackColor = true;
            // 
            // volumeBar
            // 
            volumeBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            volumeBar.Location = new Point(624, 8);
            volumeBar.Margin = new Padding(4, 3, 4, 3);
            volumeBar.Maximum = 100;
            volumeBar.Name = "volumeBar";
            volumeBar.Size = new Size(150, 45);
            volumeBar.TabIndex = 31;
            volumeBar.TickFrequency = 10;
            volumeBar.TickStyle = TickStyle.Both;
            volumeBar.Value = 10;
            volumeBar.Scroll += volumeBar_Scroll;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.ForeColor = Color.White;
            label4.Location = new Point(578, 16);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 32;
            label4.Text = "Volume";
            // 
            // lblVolume
            // 
            lblVolume.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblVolume.ForeColor = Color.White;
            lblVolume.Location = new Point(578, 33);
            lblVolume.Margin = new Padding(4, 0, 4, 0);
            lblVolume.Name = "lblVolume";
            lblVolume.Size = new Size(49, 15);
            lblVolume.TabIndex = 33;
            lblVolume.Text = "10%";
            lblVolume.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 20, 20);
            ClientSize = new Size(782, 349);
            Controls.Add(lblVolume);
            Controls.Add(label4);
            Controls.Add(volumeBar);
            Controls.Add(btnPling);
            Controls.Add(btnBanjo);
            Controls.Add(btnBit);
            Controls.Add(btnDidgeridoo);
            Controls.Add(btnCowbell);
            Controls.Add(btnIronXylophone);
            Controls.Add(btnXylophone);
            Controls.Add(btnChime);
            Controls.Add(btnBell);
            Controls.Add(btnFlute);
            Controls.Add(btnGuitar);
            Controls.Add(btnClick);
            Controls.Add(btnSnare);
            Controls.Add(btnBDrum);
            Controls.Add(btnDBass);
            Controls.Add(btnHarp);
            Controls.Add(hScrollBar);
            Controls.Add(vScrollBar);
            Controls.Add(picBox);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(num_TPS);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnForward);
            Controls.Add(btnReverse);
            Controls.Add(btnStop);
            Controls.Add(btnPlay);
            Controls.Add(btnSave);
            Controls.Add(menu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menu;
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(798, 388);
            Name = "Form1";
            Text = "Note Block Studio Remake";
            Resize += Form1_Resize;
            menu.ResumeLayout(false);
            menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_TPS).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)volumeBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newSongToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSongToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.Button btnHarp;
        private System.Windows.Forms.Button btnDBass;
        private System.Windows.Forms.Button btnBDrum;
        private System.Windows.Forms.Button btnSnare;
        private System.Windows.Forms.Button btnBell;
        private System.Windows.Forms.Button btnFlute;
        private System.Windows.Forms.Button btnGuitar;
        private System.Windows.Forms.Button btnClick;
        private System.Windows.Forms.Button btnCowbell;
        private System.Windows.Forms.Button btnIronXylophone;
        private System.Windows.Forms.Button btnXylophone;
        private System.Windows.Forms.Button btnChime;
        private System.Windows.Forms.Button btnPling;
        private System.Windows.Forms.Button btnBanjo;
        private System.Windows.Forms.Button btnBit;
        private System.Windows.Forms.Button btnDidgeridoo;
        private System.Windows.Forms.TrackBar volumeBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private NumericUpDown num_TPS;
    }
}

