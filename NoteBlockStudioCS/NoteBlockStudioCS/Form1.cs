using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Diagnostics;
using System.Timers;
using Haukcode.HighResolutionTimer;
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;
using System.IO;

namespace NoteBlockStudioCS {
    public partial class Form1: Form {

        public Form1() {
            InitializeComponent();
            _uiContext = SynchronizationContext.Current!;
            _postTick = () => _uiContext.Post(_ => OnTimerTick(), null);
            centered.Alignment = StringAlignment.Center;
            centered.LineAlignment = StringAlignment.Center;
            volume = volumeBar.Value;

            NoteSound.Init();

            StartWorker();
            KeyPreview = true;

            MinuteTimer.Tick += (object? sender, EventArgs e) => { MinutesSpent++; };
            MinuteTimer.Interval = 60000;
            MinuteTimer.Enabled = true;
            MinuteTimer.Start();

            ClearSoundBuffer.Tick += (object? sender, EventArgs e) => { NoteSound.Clear(); tsl_SoundsPlaying.Text = $"Sounds Playing: {NoteSound.GetSoundsPlaying()}"; };
            ClearSoundBuffer.Interval = 250;
            ClearSoundBuffer.Enabled = true;
            ClearSoundBuffer.Start();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

            picBox.MouseWheel += ScrollPlayer;
            pbx_Layers.MouseWheel += ScrollLayer;

            picBox.Invalidate();

            btnHarp.Click += (object? sender, EventArgs e) => { ChangeInstrument("harp"); LeftClicks++; };
            btnDBass.Click += (object? sender, EventArgs e) => { ChangeInstrument("dbass"); LeftClicks++; };
            btnBDrum.Click += (object? sender, EventArgs e) => { ChangeInstrument("bdrum"); LeftClicks++; };
            btnSnare.Click += (object? sender, EventArgs e) => { ChangeInstrument("sdrum"); LeftClicks++; };
            btnClick.Click += (object? sender, EventArgs e) => { ChangeInstrument("click"); LeftClicks++; };
            btnGuitar.Click += (object? sender, EventArgs e) => { ChangeInstrument("guitar"); LeftClicks++; };
            btnFlute.Click += (object? sender, EventArgs e) => { ChangeInstrument("flute"); LeftClicks++; };
            btnBell.Click += (object? sender, EventArgs e) => { ChangeInstrument("bell"); LeftClicks++; };
            btnChime.Click += (object? sender, EventArgs e) => { ChangeInstrument("icechime"); LeftClicks++; };
            btnXylophone.Click += (object? sender, EventArgs e) => { ChangeInstrument("xylobone"); LeftClicks++; };
            btnIronXylophone.Click += (object? sender, EventArgs e) => { ChangeInstrument("iron_xylophone"); LeftClicks++; };
            btnCowbell.Click += (object? sender, EventArgs e) => { ChangeInstrument("cow_bell"); LeftClicks++; };
            btnDidgeridoo.Click += (object? sender, EventArgs e) => { ChangeInstrument("didgeridoo"); LeftClicks++; };
            btnBit.Click += (object? sender, EventArgs e) => { ChangeInstrument("bit"); LeftClicks++; };
            btnBanjo.Click += (object? sender, EventArgs e) => { ChangeInstrument("banjo"); LeftClicks++; };
            btnPling.Click += (object? sender, EventArgs e) => { ChangeInstrument("pling"); LeftClicks++; };

            foreach (Keys key in Enum.GetValues(typeof(Keys))) {
                keyStates[key] = false;
            }

            KeyDown += (object? s, KeyEventArgs e) => { keyStates[e.KeyCode] = true; };
            KeyUp += (object? s, KeyEventArgs e) => { keyStates[e.KeyCode] = false; };

        }

        private void StartWorker() {
            var worker = new Thread(() => {
                try {
                    while (true) {
                        timer.WaitForTrigger();
                        lock (threadlock) {
                            // Marshal the tick call to the main thread
                            _postTick();
                        }
                    }
                } catch (ThreadAbortException) {
                    // clean exit
                }
            }) {
                IsBackground = true,
                Name = "HighResTimerWorker"
            };
            worker.Start();
        }

        private void OnTimerTick() {
            NoteSound.Clear();
            tsl_SoundsPlaying.Text = $"Sounds Playing: {NoteSound.GetSoundsPlaying()}";
            bool hasSolo = false;
            if (layers.Where(x => x.Solo).Count() >= 1) {
                hasSolo = true;
            }
            if (notes.ContainsKey(playbackPosition)) {
                foreach (var note in notes[playbackPosition]) {
                    if (hasSolo) {
                        if (layers[note.Key].Solo) {
                            NoteSound.AddToPlayQueue(note.Value.Instrument, note.Value.Key, (note.Value.Velocity * (layers[note.Value.Y].Volume / 100f)) * (volume / 100f));
                        }
                    } else if (!layers[note.Key].Lock) {
                        NoteSound.AddToPlayQueue(note.Value.Instrument, note.Value.Key, (note.Value.Velocity * (layers[note.Value.Y].Volume / 100f)) * (volume / 100f));
                    }
                }
                NoteSound.Play();
                tsl_SoundsPlaying.Text = $"Sounds Playing: {NoteSound.GetSoundsPlaying()}";
            }
            sw.Stop();
            tsl_LastTickMS.Text = $"Last Tick: {sw.ElapsedMilliseconds.ToString()}ms";
            sw.Restart();
            playbackPosition++;
            if (playbackPosition >= farthestNoteX + displayWidth - 3 && playing) {
                timer.Stop();
                playing = false;
            }
            if (playbackPosition > hScrollBar.Value + displayWidth) {
                hScrollBar.Value = Math.Min(playbackPosition, farthestNoteX);
                picBox.Invalidate();
            } else {
                picBox.Invalidate(new Rectangle((playbackPosition - hScrollBar.Value - 1) * 32, 32, 64, (displayHeight * 32)));
            }
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

    }
}
