using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Diagnostics;
using System.Timers;
using Haukcode.HighResolutionTimer;
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;

namespace NoteBlockStudioCS {
    public partial class Form1: Form {

        Dictionary<int, Dictionary<int, NoteBlock>> notes = new Dictionary<int, Dictionary<int, NoteBlock>>();
        List<Layer> layers = new List<Layer>();

        List<int> maxIndex = new List<int>();

        NBSLoader loader = new NBSLoader();

        Pen penLightGray = new Pen(Color.FromArgb(200, 200, 200));
        Pen penGray1 = new Pen(Color.FromArgb(100, 100, 100));
        Pen penGray2 = new Pen(Color.FromArgb(70, 70, 70));
        SolidBrush brushLightGray = new SolidBrush(Color.FromArgb(200, 200, 200));
        SolidBrush brushGray = new SolidBrush(Color.FromArgb(60, 60, 60));
        SolidBrush brushSelected = new SolidBrush(Color.FromArgb(50, 200, 200, 200));

        StringFormat centered = new StringFormat();

        WaveOutEvent waveOut = new WaveOutEvent();
        MixingSampleProvider mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 1)) {
            ReadFully = true
        };

        List<string> files = new List<string> { "stuff/sounds/harp.wav" };
        List<ISampleProvider> sources = new List<ISampleProvider>();

        int offset = 0;
        int farthestNote = 0;
        int totalNotes = 0;

        int playbackPosition = 0;
        int volume;
        object threadlock = new object();
        short SongTempo = 1000;

        bool playing = false;

        Stopwatch sw = Stopwatch.StartNew();

        Random rnd = new Random();

        HighResolutionTimer timer = new HighResolutionTimer();

        public enum ins {
            harp = 0,
            dbass = 1,
            bdrum = 2,
            snare = 3,
            click = 4,
            guitar = 5,
            flute = 6,
            bell = 7,
            chime = 8,
            xyl = 9,
            ironxyl = 10,
            cowbell = 11,
            didgeridoo = 12,
            bit = 13,
            banjo = 14,
            pling = 15
        }

        ins instrumentSelected = ins.harp;
        sbyte keySelected = 45;

        int displayWidth = 16;
        int displayHeight = 7;

        System.Windows.Forms.Timer ClearSoundBuffer = new System.Windows.Forms.Timer();

        public Form1() {
            InitializeComponent();
            centered.Alignment = StringAlignment.Center;
            centered.LineAlignment = StringAlignment.Center;

            foreach (string file in files) {
                AudioFileReader reader = new AudioFileReader(file);
                sources.Add(reader.ToSampleProvider());
            }
            volume = volumeBar.Value;

            var worker = new Thread(() => {
                try {
                    while (true) {
                        // blocks until the timer interval has elapsed, then resets the event
                        timer.WaitForTrigger();
                        lock (threadlock) {
                            Timer_Tick(volume);
                        }
                    }
                } catch (ThreadAbortException) {
                    // Thread was aborted—exit cleanly
                }
            }) {
                IsBackground = true,
                Name = "HighResTimerWorker"
            };
            worker.Start();

            NoteSound.Init();

            ClearSoundBuffer.Tick += (object? sender, EventArgs e) => { NoteSound.Clear(); tsl_SoundsPlaying.Text = $"Sounds Playing: {NoteSound.GetSoundsPlaying()}"; };
            ClearSoundBuffer.Interval = 250;
            ClearSoundBuffer.Enabled = true;
            ClearSoundBuffer.Start();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

            picBox.MouseWheel += ScrollPlayer;

            picBox.Invalidate();

            btnHarp.Click += (object? sender, EventArgs e) => { ChangeInstrument("harp"); };
            btnDBass.Click += (object? sender, EventArgs e) => { ChangeInstrument("dbass"); };
            btnBDrum.Click += (object? sender, EventArgs e) => { ChangeInstrument("bdrum"); };
            btnSnare.Click += (object? sender, EventArgs e) => { ChangeInstrument("sdrum"); };
            btnClick.Click += (object? sender, EventArgs e) => { ChangeInstrument("click"); };
            btnGuitar.Click += (object? sender, EventArgs e) => { ChangeInstrument("guitar"); };
            btnFlute.Click += (object? sender, EventArgs e) => { ChangeInstrument("flute"); };
            btnBell.Click += (object? sender, EventArgs e) => { ChangeInstrument("bell"); };
            btnChime.Click += (object? sender, EventArgs e) => { ChangeInstrument("icechime"); };
            btnXylophone.Click += (object? sender, EventArgs e) => { ChangeInstrument("xylobone"); };
            btnIronXylophone.Click += (object? sender, EventArgs e) => { ChangeInstrument("iron_xylophone"); };
            btnCowbell.Click += (object? sender, EventArgs e) => { ChangeInstrument("cow_bell"); };
            btnDidgeridoo.Click += (object? sender, EventArgs e) => { ChangeInstrument("didgeridoo"); };
            btnBit.Click += (object? sender, EventArgs e) => { ChangeInstrument("bit"); };
            btnBanjo.Click += (object? sender, EventArgs e) => { ChangeInstrument("banjo"); };
            btnPling.Click += (object? sender, EventArgs e) => { ChangeInstrument("pling"); };
        }

        private void ChangeInstrument(string type) {
            NoteSound.PlaySingle(type, volume: volume);
            switch (type) {
                case "harp": instrumentSelected = ins.harp; break;
                case "dbass": instrumentSelected = ins.dbass; break;
                case "bdrum": instrumentSelected = ins.bdrum; break;
                case "sdrum": instrumentSelected = ins.snare; break;
                case "click": instrumentSelected = ins.click; break;
                case "guitar": instrumentSelected = ins.guitar; break;
                case "flute": instrumentSelected = ins.flute; break;
                case "bell": instrumentSelected = ins.bell; break;
                case "icechime": instrumentSelected = ins.chime; break;
                case "xylobone": instrumentSelected = ins.xyl; break;
                case "iron_xylophone": instrumentSelected = ins.ironxyl; break;
                case "cow_bell": instrumentSelected = ins.cowbell; break;
                case "didgeridoo": instrumentSelected = ins.didgeridoo; break;
                case "bit": instrumentSelected = ins.bit; break;
                case "banjo": instrumentSelected = ins.banjo; break;
                case "pling": instrumentSelected = ins.pling; break;
            }
        }

        private void ScrollPlayer(object sender, MouseEventArgs e) {
            //offset = Math.Max(offset - Math.Sign(e.Delta), 0);
            //NoteSound.Play("harp", ((float)volumeBar.Value) / 100f);
            playbackPosition += 20 * Math.Sign(e.Delta);
            playbackPosition = Math.Max(playbackPosition, 0);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                // Get the dragged file(s)
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                // Check if the file has the ".nbs" extension
                if (files.Length == 1 && Path.GetExtension(files[0]).Equals(".nbs", StringComparison.OrdinalIgnoreCase)) {
                    e.Effect = DragDropEffects.Copy; // Allow drop
                } else {
                    e.Effect = DragDropEffects.None; // Disallow drop
                }
            } else {
                e.Effect = DragDropEffects.None; // Disallow drop
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            //timer.Stop();
            // Get the dragged file(s)
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 1) {
                string filePath = files[0];
                // Process the file here
                loader.LoadFile(filePath);
                List<NoteBlock> tempList = new List<NoteBlock>(loader.NoteBlocks);
                totalNotes = tempList.Count;
                tsl_TotalNotes.Text = $"Total Notes: {totalNotes}";
                farthestNote = loader.SongLength;
                Debug.WriteLine(loader.MinutesSpent);
                hScrollBar.Maximum = farthestNote;
                SongTempo = loader.SongTempo;
                num_TPS.Value = (loader.SongTempo / 100);

                notes = new Dictionary<int, Dictionary<int, NoteBlock>>();

                for (int b = 0; b < tempList.Count; b++) {
                    if (!notes.ContainsKey(tempList[b].X)) {
                        notes[tempList[b].X] = new Dictionary<int, NoteBlock>();
                    }
                    if (!notes[tempList[b].X].ContainsKey(tempList[b].Y)) {
                        notes[tempList[b].X][tempList[b].Y] = new NoteBlock(tempList[b]);
                    }
                }

                layers = new List<Layer>(loader.layers);

                picBox.Invalidate();
                //MessageBox.Show("File loaded\n\nName: " + loader.SongName + "\nAuthor: " + loader.SongAuthor + "\nOriginal Author: " + loader.SongOriginalAuthor + "\nDescription: " + loader.SongDescription);
            }
        }

        /// <summary>
        /// Returns a Note object if it exists, otherwise returns null
        /// </summary>
        /// <param name="layer">The layer (or Y)</param>
        /// <param name="x">The X value</param>
        /// <returns>Note object</returns>
        private NoteBlock GetNote(int layer, int x) {
            if (layer < notes.Count) {
                if (notes[layer].ContainsKey(x)) {
                    return notes[layer][x];
                } else {
                    return null;
                }
            } else {
                return null;
            }
        }

        private void Form1_Resize(object sender, EventArgs e) {
            int w = Width - 6;
            int h = Height - 151;
            displayWidth = (w - 786 + 512) / 32;
            displayHeight = (h - 344 + 224) / 32;
            hScrollBar.Location = new Point(274, 70 + (displayHeight * 32));
            hScrollBar.Size = new Size((displayWidth * 32) - 32, hScrollBar.Size.Height);
            vScrollBar.Location = new Point(246 + (displayWidth * 32), 67);
            vScrollBar.Size = new Size(vScrollBar.Size.Width, displayHeight * 32);
            picBox.Width = (displayWidth * 32) - 32;
            picBox.Height = (displayHeight * 32);
        }

        private void volumeBar_Scroll(object sender, EventArgs e) {
            lblVolume.Text = volumeBar.Value.ToString() + "%";
            lock (threadlock) {
                volume = volumeBar.Value;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e) {
            if (playing)
                timer.Stop();
            //if (notes.Count >= 1) {
                timer.SetPeriod((int)Math.Round(1000f / ((double)num_TPS.Value)));
                timer.Start();
                playing = true;
            //}
        }

        private void Timer_Tick(int volume) {
            NoteSound.Clear();
            tsl_SoundsPlaying.Text = $"Sounds Playing: {NoteSound.GetSoundsPlaying()}";
            if (notes.ContainsKey(playbackPosition)) {
                foreach (var note in notes[playbackPosition]) {
                    NoteSound.AddToPlayQueue(note.Value.Instrument, note.Value.Key, (note.Value.Velocity * (layers[note.Value.Y].Volume / 100f)) * (volume / 100f));
                }
                NoteSound.Play();
                tsl_SoundsPlaying.Text = $"Sounds Playing: {NoteSound.GetSoundsPlaying()}";
            }
            sw.Restart();
            playbackPosition++;
            if (playbackPosition > offset + displayWidth) {
                offset = playbackPosition;
                picBox.Invalidate();
            } else {
                picBox.Invalidate(new Rectangle((playbackPosition - offset - 1) * 32, 32, 64, (displayHeight * 32)));
            }
        }

        private void picBox_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            //picBox.Invalidate();

            g.FillRectangle(brushGray, 0, 0, picBox.Width, 32);

            g.DrawLine(penLightGray, 0, 32, picBox.Width, 32);
            g.DrawLine(penLightGray, 0, 16, picBox.Width, 16);

            for (int i = 0; i < displayWidth; i++) {
                if (i % 4 == 0) {
                    g.DrawLine(penGray1, i * 32, 32, i * 32, picBox.Height);
                    g.DrawString(i.ToString(), DefaultFont, brushLightGray, i * 32, 24, centered);
                } else {
                    g.DrawLine(penGray2, i * 32, 32, i * 32, picBox.Height);
                }
            }

            SolidBrush white = new SolidBrush(Color.FromArgb(255, 255, 255));
            foreach (int x in notes.Keys) {
                foreach (int y in notes[x].Keys) {
                    g.FillRectangle(NoteSound.Brushes[notes[x][y].Instrument], (notes[x][y].X - offset) * 32, (notes[x][y].Y + 1) * 32, 32, 32);
                    g.DrawString((notes[x][y].Key - 33).ToString(), DefaultFont, white, ((notes[x][y].X - offset) * 32) + 16, ((notes[x][y].Y + 1) * 32) + 16);
                }
            }

            g.FillRectangle(brushSelected, (playbackPosition - offset) * 32, 32, 32, (displayHeight * 32));

        }

        private void picBox_MouseClick(object sender, MouseEventArgs e) {
            int mouseX = e.X / 32;
            int mouseY = (e.Y / 32) - 1;
            if(mouseY >= 0) {
                if (e.Button == MouseButtons.Left) {
                    AddBlock(mouseX, mouseY);
                    NoteSound.PlaySingle(InsToString(instrumentSelected), keySelected, volume);
                } else if (e.Button == MouseButtons.Right) {
                    RemoveBlock(mouseX, mouseY);
                } else {

                }
            }
        }

        private string InsToString(ins i) {
            switch (i) {
                case ins.harp: return "harp";
                case ins.dbass: return "dbass";
                case ins.bdrum: return "bdrum";
                case ins.snare: return "sdrum";
                case ins.click: return "click";
                case ins.guitar: return "guitar";
                case ins.flute: return "flute";
                case ins.bell: return "bell";
                case ins.chime: return "icechime";
                case ins.xyl: return "xylobone";
                case ins.ironxyl: return "iron_xylophone";
                case ins.cowbell: return "cow_bell";
                case ins.didgeridoo: return "didgeridoo";
                case ins.bit: return "bit";
                case ins.banjo: return "banjo";
                case ins.pling: return "pling";
            }
            return "";
        }

        private void AddBlock(int x, int y) {
            AddBlock(x, y, new NoteBlock(x, y, (sbyte)instrumentSelected, keySelected, 100, 0, 0));
        }

        private void AddBlock(int x, int y, NoteBlock nb) {
            if (!notes.ContainsKey(x))
                notes[x] = new Dictionary<int, NoteBlock>();
            if (!notes[x].ContainsKey(y)) {
                totalNotes++;
            }
            notes[x][y] = nb;

            if(y >= layers.Count) {
                for (int i = layers.Count; i <= y; i++) {
                    layers.Add(new Layer());
                }
            }

            tsl_TotalNotes.Text = $"Total Notes: {totalNotes}";
            if(x >= farthestNote) {
                farthestNote = x;
            }

            Debug.WriteLine($"{y}, {layers.Count}");

            picBox.Invalidate(new Rectangle((x - offset) * 32, (y * 32) + 32, 32, 32));
        }

        private void RemoveBlock(int x, int y) {
            if (notes.ContainsKey(x) && notes[x].ContainsKey(y)) {
                notes[x].Remove(y);
                totalNotes--;
                if (notes[x].Count == 0) {
                    notes.Remove(x);
                    if(totalNotes == 0) {
                        farthestNote = 0;
                    } else {
                        farthestNote = notes.OrderByDescending(x => x.Key).First().Key;
                    }
                }
                tsl_TotalNotes.Text = $"Total Notes: {totalNotes}";
            }
            picBox.Invalidate(new Rectangle((x - offset) * 32, (y * 32) + 32, 32, 32));
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e) {
            offset = hScrollBar.Value;
        }

        private void picBox_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {
            picBox.Invalidate();
        }

        private void btnStop_Click(object sender, EventArgs e) {
            if (playing) {
                timer.Stop();
                playing = false;
            } else {
                playbackPosition = 0;
                offset = 0;
                picBox.Invalidate();
            }
            tsl_SoundsPlaying.Text = $"Sounds Playing: {NoteSound.GetSoundsPlaying()}";
        }

        private void num_TPS_ValueChanged(object sender, EventArgs e) {
            timer.SetPeriod((int)Math.Round(1000f / (SongTempo / 100f)));
        }
    }
}
