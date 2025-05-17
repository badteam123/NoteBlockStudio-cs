using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Diagnostics;
using System.Timers;
using Haukcode.HighResolutionTimer;
using Microsoft.VisualBasic;

namespace NoteBlockStudioCS {
    public partial class Form1: Form {

        Dictionary<int, Dictionary<int, NoteBlock>> notes = new Dictionary<int, Dictionary<int, NoteBlock>>();

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

        int playbackPosition = 0;
        int volume;
        object threadlock = new object();

        bool playing = false;

        Stopwatch sw = Stopwatch.StartNew();

        Random rnd = new Random();

        HighResolutionTimer timer = new HighResolutionTimer();

        public enum ins {
            none = 0,
            harp = 1,
            dbass = 2,
            bdrum = 3,
            snare = 4,
            click = 5,
            guitar = 6,
            flute = 7,
            bell = 8,
            chime = 9,
            xyl = 10,
            ironxyl = 11,
            cowbell = 12,
            didgeridoo = 13,
            bit = 14,
            banjo = 15,
            pling = 16
        }

        ins instrumentSelected = ins.harp;

        int displayWidth = 16;
        int displayHeight = 7;

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

                        // your high-precision callback here:
                        lock (threadlock) {
                            Timer_Tick(volume);
                        }
                    }
                } catch (ThreadAbortException) {
                    // Thread was aborted—exit cleanly
                }
            }) {
                IsBackground = true,    // so it won't keep your app alive if main thread exits
                Name = "HighResTimerWorker"
            };
            worker.Start();

            NoteSound.Init();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

            picBox.MouseWheel += ScrollPlayer;

            picBox.Invalidate();
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
                farthestNote = loader.SongLength;
                Debug.WriteLine(loader.MinutesSpent);
                hScrollBar.Maximum = farthestNote;
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
            int h = Height - 6;
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
            if (notes.Count >= 1) {
                timer.SetPeriod((int)Math.Round(1000f / ((double)num_TPS.Value)));
                timer.Start();
                playing = true;
            }
        }

        private void Timer_Tick(int volume) {
            NoteSound.Clear();
            if (notes.ContainsKey(playbackPosition)) {
                foreach (var note in notes[playbackPosition]) {
                    NoteSound.AddToPlayQueue(note.Value.Instrument, note.Value.Key, (note.Value.Velocity * (loader.layers[note.Value.Y].Volume / 100f)) * (volume / 100f));
                }
                NoteSound.Play();
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
            int mouseY = e.Y / 32;
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e) {
            offset = hScrollBar.Value;
        }

        private void picBox_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {
            picBox.Invalidate();
        }

        private void btnStop_Click(object sender, EventArgs e) {
            if (playing)
                timer.Stop();
            playing = false;
        }

        private void num_TPS_ValueChanged(object sender, EventArgs e) {
            timer.SetPeriod((int)Math.Round(1000f / (loader.SongTempo / 100f)));
        }
    }
}
