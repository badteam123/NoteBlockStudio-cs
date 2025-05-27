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
            lbl_SongCurrentTime = new Label();
            lbl_SongTotalTime = new Label();
            num_TPS = new NumericUpDown();
            label3 = new Label();
            tbx_Position = new TextBox();
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
            pbx_Piano = new PictureBox();
            sts_Status = new StatusStrip();
            tsl_Instrument = new ToolStripStatusLabel();
            tsl_SoundsPlaying = new ToolStripStatusLabel();
            tsl_TotalNotes = new ToolStripStatusLabel();
            tsl_LastTickMS = new ToolStripStatusLabel();
            pbx_Layers = new PictureBox();
            menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_TPS).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)volumeBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbx_Piano).BeginInit();
            sts_Status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbx_Layers).BeginInit();
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
            btnSave.Size = new Size(20, 20);
            btnSave.TabIndex = 0;
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
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
            btnPlay.Size = new Size(20, 20);
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
            btnStop.Size = new Size(20, 20);
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
            btnReverse.Size = new Size(20, 20);
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
            btnForward.Size = new Size(20, 20);
            btnForward.TabIndex = 4;
            btnForward.UseVisualStyleBackColor = true;
            // 
            // lbl_SongCurrentTime
            // 
            lbl_SongCurrentTime.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_SongCurrentTime.ForeColor = Color.White;
            lbl_SongCurrentTime.Location = new Point(7, 61);
            lbl_SongCurrentTime.Margin = new Padding(4, 0, 4, 0);
            lbl_SongCurrentTime.Name = "lbl_SongCurrentTime";
            lbl_SongCurrentTime.Size = new Size(113, 23);
            lbl_SongCurrentTime.TabIndex = 5;
            lbl_SongCurrentTime.Text = "00:00:00.000";
            // 
            // lbl_SongTotalTime
            // 
            lbl_SongTotalTime.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_SongTotalTime.ForeColor = Color.White;
            lbl_SongTotalTime.Location = new Point(24, 82);
            lbl_SongTotalTime.Margin = new Padding(4, 0, 4, 0);
            lbl_SongTotalTime.Name = "lbl_SongTotalTime";
            lbl_SongTotalTime.Size = new Size(89, 15);
            lbl_SongTotalTime.TabIndex = 6;
            lbl_SongTotalTime.Text = "/ 00:00:00.000";
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
            // tbx_Position
            // 
            tbx_Position.BackColor = Color.FromArgb(20, 20, 20);
            tbx_Position.BorderStyle = BorderStyle.FixedSingle;
            tbx_Position.ForeColor = Color.White;
            tbx_Position.Location = new Point(212, 67);
            tbx_Position.Margin = new Padding(4, 3, 4, 3);
            tbx_Position.Name = "tbx_Position";
            tbx_Position.ReadOnly = true;
            tbx_Position.Size = new Size(54, 23);
            tbx_Position.TabIndex = 9;
            tbx_Position.Text = "1, 1, 1";
            tbx_Position.TextAlign = HorizontalAlignment.Right;
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
            picBox.Paint += picBox_Paint;
            picBox.MouseDown += picBox_MouseDown;
            // 
            // vScrollBar
            // 
            vScrollBar.LargeChange = 1;
            vScrollBar.Location = new Point(758, 67);
            vScrollBar.Maximum = 0;
            vScrollBar.Name = "vScrollBar";
            vScrollBar.Size = new Size(17, 256);
            vScrollBar.TabIndex = 13;
            vScrollBar.Scroll += ScrollBar_Scroll;
            // 
            // hScrollBar
            // 
            hScrollBar.LargeChange = 1;
            hScrollBar.Location = new Point(274, 326);
            hScrollBar.Maximum = 0;
            hScrollBar.Name = "hScrollBar";
            hScrollBar.Size = new Size(480, 14);
            hScrollBar.TabIndex = 14;
            hScrollBar.Scroll += ScrollBar_Scroll;
            // 
            // btnHarp
            // 
            btnHarp.BackgroundImage = (Image)resources.GetObject("btnHarp.BackgroundImage");
            btnHarp.BackgroundImageLayout = ImageLayout.Stretch;
            btnHarp.FlatAppearance.BorderSize = 0;
            btnHarp.FlatStyle = FlatStyle.Flat;
            btnHarp.Location = new Point(213, 35);
            btnHarp.Margin = new Padding(4, 3, 4, 3);
            btnHarp.Name = "btnHarp";
            btnHarp.Size = new Size(20, 20);
            btnHarp.TabIndex = 15;
            btnHarp.UseVisualStyleBackColor = true;
            // 
            // btnDBass
            // 
            btnDBass.BackgroundImage = (Image)resources.GetObject("btnDBass.BackgroundImage");
            btnDBass.BackgroundImageLayout = ImageLayout.Stretch;
            btnDBass.FlatAppearance.BorderSize = 0;
            btnDBass.FlatStyle = FlatStyle.Flat;
            btnDBass.Location = new Point(236, 35);
            btnDBass.Margin = new Padding(4, 3, 4, 3);
            btnDBass.Name = "btnDBass";
            btnDBass.Size = new Size(20, 20);
            btnDBass.TabIndex = 16;
            btnDBass.UseVisualStyleBackColor = true;
            // 
            // btnBDrum
            // 
            btnBDrum.BackgroundImage = (Image)resources.GetObject("btnBDrum.BackgroundImage");
            btnBDrum.BackgroundImageLayout = ImageLayout.Stretch;
            btnBDrum.FlatAppearance.BorderSize = 0;
            btnBDrum.FlatStyle = FlatStyle.Flat;
            btnBDrum.Location = new Point(258, 35);
            btnBDrum.Margin = new Padding(4, 3, 4, 3);
            btnBDrum.Name = "btnBDrum";
            btnBDrum.Size = new Size(20, 20);
            btnBDrum.TabIndex = 17;
            btnBDrum.UseVisualStyleBackColor = true;
            // 
            // btnSnare
            // 
            btnSnare.BackgroundImage = (Image)resources.GetObject("btnSnare.BackgroundImage");
            btnSnare.BackgroundImageLayout = ImageLayout.Stretch;
            btnSnare.FlatAppearance.BorderSize = 0;
            btnSnare.FlatStyle = FlatStyle.Flat;
            btnSnare.Location = new Point(281, 35);
            btnSnare.Margin = new Padding(4, 3, 4, 3);
            btnSnare.Name = "btnSnare";
            btnSnare.Size = new Size(20, 20);
            btnSnare.TabIndex = 18;
            btnSnare.UseVisualStyleBackColor = true;
            // 
            // btnBell
            // 
            btnBell.BackgroundImage = (Image)resources.GetObject("btnBell.BackgroundImage");
            btnBell.BackgroundImageLayout = ImageLayout.Stretch;
            btnBell.FlatAppearance.BorderSize = 0;
            btnBell.FlatStyle = FlatStyle.Flat;
            btnBell.Location = new Point(372, 35);
            btnBell.Margin = new Padding(4, 3, 4, 3);
            btnBell.Name = "btnBell";
            btnBell.Size = new Size(20, 20);
            btnBell.TabIndex = 22;
            btnBell.UseVisualStyleBackColor = true;
            // 
            // btnFlute
            // 
            btnFlute.BackgroundImage = (Image)resources.GetObject("btnFlute.BackgroundImage");
            btnFlute.BackgroundImageLayout = ImageLayout.Stretch;
            btnFlute.FlatAppearance.BorderSize = 0;
            btnFlute.FlatStyle = FlatStyle.Flat;
            btnFlute.Location = new Point(349, 35);
            btnFlute.Margin = new Padding(4, 3, 4, 3);
            btnFlute.Name = "btnFlute";
            btnFlute.Size = new Size(20, 20);
            btnFlute.TabIndex = 21;
            btnFlute.UseVisualStyleBackColor = true;
            // 
            // btnGuitar
            // 
            btnGuitar.BackgroundImage = (Image)resources.GetObject("btnGuitar.BackgroundImage");
            btnGuitar.BackgroundImageLayout = ImageLayout.Stretch;
            btnGuitar.FlatAppearance.BorderSize = 0;
            btnGuitar.FlatStyle = FlatStyle.Flat;
            btnGuitar.Location = new Point(326, 35);
            btnGuitar.Margin = new Padding(4, 3, 4, 3);
            btnGuitar.Name = "btnGuitar";
            btnGuitar.Size = new Size(20, 20);
            btnGuitar.TabIndex = 20;
            btnGuitar.UseVisualStyleBackColor = true;
            // 
            // btnClick
            // 
            btnClick.BackgroundImage = (Image)resources.GetObject("btnClick.BackgroundImage");
            btnClick.BackgroundImageLayout = ImageLayout.Stretch;
            btnClick.FlatAppearance.BorderSize = 0;
            btnClick.FlatStyle = FlatStyle.Flat;
            btnClick.Location = new Point(304, 35);
            btnClick.Margin = new Padding(4, 3, 4, 3);
            btnClick.Name = "btnClick";
            btnClick.Size = new Size(20, 20);
            btnClick.TabIndex = 19;
            btnClick.UseVisualStyleBackColor = true;
            // 
            // btnCowbell
            // 
            btnCowbell.BackgroundImage = (Image)resources.GetObject("btnCowbell.BackgroundImage");
            btnCowbell.BackgroundImageLayout = ImageLayout.Stretch;
            btnCowbell.FlatAppearance.BorderSize = 0;
            btnCowbell.FlatStyle = FlatStyle.Flat;
            btnCowbell.Location = new Point(462, 35);
            btnCowbell.Margin = new Padding(4, 3, 4, 3);
            btnCowbell.Name = "btnCowbell";
            btnCowbell.Size = new Size(20, 20);
            btnCowbell.TabIndex = 26;
            btnCowbell.UseVisualStyleBackColor = true;
            // 
            // btnIronXylophone
            // 
            btnIronXylophone.BackgroundImage = (Image)resources.GetObject("btnIronXylophone.BackgroundImage");
            btnIronXylophone.BackgroundImageLayout = ImageLayout.Stretch;
            btnIronXylophone.FlatAppearance.BorderSize = 0;
            btnIronXylophone.FlatStyle = FlatStyle.Flat;
            btnIronXylophone.Location = new Point(440, 35);
            btnIronXylophone.Margin = new Padding(4, 3, 4, 3);
            btnIronXylophone.Name = "btnIronXylophone";
            btnIronXylophone.Size = new Size(20, 20);
            btnIronXylophone.TabIndex = 25;
            btnIronXylophone.UseVisualStyleBackColor = true;
            // 
            // btnXylophone
            // 
            btnXylophone.BackgroundImage = (Image)resources.GetObject("btnXylophone.BackgroundImage");
            btnXylophone.BackgroundImageLayout = ImageLayout.Stretch;
            btnXylophone.FlatAppearance.BorderSize = 0;
            btnXylophone.FlatStyle = FlatStyle.Flat;
            btnXylophone.Location = new Point(417, 35);
            btnXylophone.Margin = new Padding(4, 3, 4, 3);
            btnXylophone.Name = "btnXylophone";
            btnXylophone.Size = new Size(20, 20);
            btnXylophone.TabIndex = 24;
            btnXylophone.UseVisualStyleBackColor = true;
            // 
            // btnChime
            // 
            btnChime.BackgroundImage = (Image)resources.GetObject("btnChime.BackgroundImage");
            btnChime.BackgroundImageLayout = ImageLayout.Stretch;
            btnChime.FlatAppearance.BorderSize = 0;
            btnChime.FlatStyle = FlatStyle.Flat;
            btnChime.Location = new Point(394, 35);
            btnChime.Margin = new Padding(4, 3, 4, 3);
            btnChime.Name = "btnChime";
            btnChime.Size = new Size(20, 20);
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
            btnPling.Size = new Size(20, 20);
            btnPling.TabIndex = 30;
            btnPling.UseVisualStyleBackColor = true;
            // 
            // btnBanjo
            // 
            btnBanjo.BackgroundImage = (Image)resources.GetObject("btnBanjo.BackgroundImage");
            btnBanjo.BackgroundImageLayout = ImageLayout.Stretch;
            btnBanjo.FlatAppearance.BorderSize = 0;
            btnBanjo.FlatStyle = FlatStyle.Flat;
            btnBanjo.Location = new Point(530, 35);
            btnBanjo.Margin = new Padding(4, 3, 4, 3);
            btnBanjo.Name = "btnBanjo";
            btnBanjo.Size = new Size(20, 20);
            btnBanjo.TabIndex = 29;
            btnBanjo.UseVisualStyleBackColor = true;
            // 
            // btnBit
            // 
            btnBit.BackgroundImage = (Image)resources.GetObject("btnBit.BackgroundImage");
            btnBit.BackgroundImageLayout = ImageLayout.Stretch;
            btnBit.FlatAppearance.BorderSize = 0;
            btnBit.FlatStyle = FlatStyle.Flat;
            btnBit.Location = new Point(508, 35);
            btnBit.Margin = new Padding(4, 3, 4, 3);
            btnBit.Name = "btnBit";
            btnBit.Size = new Size(20, 20);
            btnBit.TabIndex = 28;
            btnBit.UseVisualStyleBackColor = true;
            // 
            // btnDidgeridoo
            // 
            btnDidgeridoo.BackgroundImage = (Image)resources.GetObject("btnDidgeridoo.BackgroundImage");
            btnDidgeridoo.BackgroundImageLayout = ImageLayout.Stretch;
            btnDidgeridoo.FlatAppearance.BorderSize = 0;
            btnDidgeridoo.FlatStyle = FlatStyle.Flat;
            btnDidgeridoo.Location = new Point(485, 35);
            btnDidgeridoo.Margin = new Padding(4, 3, 4, 3);
            btnDidgeridoo.Name = "btnDidgeridoo";
            btnDidgeridoo.Size = new Size(20, 20);
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
            volumeBar.Value = 100;
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
            lblVolume.Text = "100%";
            lblVolume.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // pbx_Piano
            // 
            pbx_Piano.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbx_Piano.BackColor = Color.DimGray;
            pbx_Piano.BackgroundImageLayout = ImageLayout.None;
            pbx_Piano.BorderStyle = BorderStyle.FixedSingle;
            pbx_Piano.Location = new Point(13, 343);
            pbx_Piano.Margin = new Padding(4, 3, 4, 3);
            pbx_Piano.Name = "pbx_Piano";
            pbx_Piano.Size = new Size(756, 122);
            pbx_Piano.TabIndex = 34;
            pbx_Piano.TabStop = false;
            pbx_Piano.Paint += pbx_Piano_Paint;
            pbx_Piano.MouseDown += pbx_Piano_MouseDown;
            // 
            // sts_Status
            // 
            sts_Status.BackColor = Color.FromArgb(20, 20, 20);
            sts_Status.Items.AddRange(new ToolStripItem[] { tsl_Instrument, tsl_SoundsPlaying, tsl_TotalNotes, tsl_LastTickMS });
            sts_Status.Location = new Point(0, 466);
            sts_Status.Name = "sts_Status";
            sts_Status.Size = new Size(782, 22);
            sts_Status.TabIndex = 35;
            sts_Status.Text = "statusStrip1";
            // 
            // tsl_Instrument
            // 
            tsl_Instrument.ForeColor = Color.White;
            tsl_Instrument.Name = "tsl_Instrument";
            tsl_Instrument.Size = new Size(191, 17);
            tsl_Instrument.Spring = true;
            tsl_Instrument.Text = "Instrument: Harp";
            // 
            // tsl_SoundsPlaying
            // 
            tsl_SoundsPlaying.ForeColor = Color.White;
            tsl_SoundsPlaying.Name = "tsl_SoundsPlaying";
            tsl_SoundsPlaying.Size = new Size(191, 17);
            tsl_SoundsPlaying.Spring = true;
            tsl_SoundsPlaying.Text = "Sounds Playing: 0";
            // 
            // tsl_TotalNotes
            // 
            tsl_TotalNotes.ForeColor = Color.White;
            tsl_TotalNotes.Name = "tsl_TotalNotes";
            tsl_TotalNotes.Size = new Size(191, 17);
            tsl_TotalNotes.Spring = true;
            tsl_TotalNotes.Text = "Total Notes: 0";
            // 
            // tsl_LastTickMS
            // 
            tsl_LastTickMS.ForeColor = Color.White;
            tsl_LastTickMS.Name = "tsl_LastTickMS";
            tsl_LastTickMS.Size = new Size(191, 17);
            tsl_LastTickMS.Spring = true;
            tsl_LastTickMS.Text = "Last Tick: 0ms";
            // 
            // pbx_Layers
            // 
            pbx_Layers.BackColor = Color.FromArgb(20, 20, 20);
            pbx_Layers.BackgroundImageLayout = ImageLayout.None;
            pbx_Layers.Location = new Point(82, 99);
            pbx_Layers.Margin = new Padding(4, 3, 4, 3);
            pbx_Layers.Name = "pbx_Layers";
            pbx_Layers.Size = new Size(190, 224);
            pbx_Layers.TabIndex = 36;
            pbx_Layers.TabStop = false;
            pbx_Layers.Paint += pbx_Layers_Paint;
            pbx_Layers.MouseDown += pbx_Layers_MouseDown;
            pbx_Layers.MouseLeave += pbx_Layers_MouseLeave;
            pbx_Layers.MouseMove += pbx_Layers_MouseMove;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 20, 20);
            ClientSize = new Size(782, 488);
            Controls.Add(pbx_Layers);
            Controls.Add(sts_Status);
            Controls.Add(pbx_Piano);
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
            Controls.Add(tbx_Position);
            Controls.Add(label3);
            Controls.Add(num_TPS);
            Controls.Add(lbl_SongTotalTime);
            Controls.Add(lbl_SongCurrentTime);
            Controls.Add(btnForward);
            Controls.Add(btnReverse);
            Controls.Add(btnStop);
            Controls.Add(btnPlay);
            Controls.Add(btnSave);
            Controls.Add(menu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menu;
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(798, 527);
            Name = "Form1";
            Text = "Note Block Studio Remake";
            Resize += Form1_Resize;
            menu.ResumeLayout(false);
            menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_TPS).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)volumeBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbx_Piano).EndInit();
            sts_Status.ResumeLayout(false);
            sts_Status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbx_Layers).EndInit();
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
        private System.Windows.Forms.Label lbl_SongCurrentTime;
        private System.Windows.Forms.Label lbl_SongTotalTime;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbx_Position;
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
        private PictureBox pbx_Piano;
        private StatusStrip sts_Status;
        private ToolStripStatusLabel tsl_Instrument;
        private ToolStripStatusLabel tsl_SoundsPlaying;
        private ToolStripStatusLabel tsl_TotalNotes;
        private PictureBox pbx_Layers;
        private ToolStripStatusLabel tsl_LastTickMS;
    }
}

