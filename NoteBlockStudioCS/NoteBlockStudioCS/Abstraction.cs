using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlockStudioCS {
    public partial class Form1: Form {

        private enum LayerSetting {
            SetVolume = 0,
            AddVolume = 1,
            SetStereo = 2,
            AddStereo = 3,
            SetMute = 4,
            ToggleMute = 5,
            SetSolo = 6,
            ToggleSolo = 7,
            AddLayer = 8,
            RemoveLayer = 9,
            UpLayer = 10,
            DownLayer = 11
        }

        /// <summary>
        /// Allows modifying layers
        /// ToggleMute
        /// ToggleSolo
        /// AddLayer
        /// RemoveLayer
        /// UpLayer
        /// DownLayer
        /// </summary>
        /// <param name="layer">Layer to modify (index)</param>
        /// <param name="setting">Layer setting to execute</param>
        private void ModifyLayer(int layer, LayerSetting setting) {
            if (layer < 0) {
                return;
            }
            while (layers.Count <= layer) {
                layers.Add(new Layer());
            }
            switch (setting) {
                case LayerSetting.ToggleMute: {
                        layers[layer].Lock = !layers[layer].Lock;
                        break;
                    }
                case LayerSetting.ToggleSolo: {
                        layers[layer].Solo = !layers[layer].Solo;
                        break;
                    }
                case LayerSetting.AddLayer: {
                        break;
                    }
                case LayerSetting.RemoveLayer: {
                        break;
                    }
                case LayerSetting.UpLayer: {
                        break;
                    }
                case LayerSetting.DownLayer: {
                        break;
                    }
            }
            Debug.WriteLine($"{layers[layer].Lock}, {layers[layer].Solo}");
            pbx_Layers.Invalidate();
        }

        /// <summary>
        /// Allows modifying layers
        /// SetVolume
        /// AddVolume
        /// SetStereo
        /// AddStereo
        /// </summary>
        /// <param name="layer">Layer to modify (index)</param>
        /// <param name="setting">Layer setting to execute</param>
        /// <param name="value">Value to set/add by</param>
        private void ModifyLayer(int layer, LayerSetting setting, int value) {
            if(layer < 0) {
                return;
            }
            while (layers.Count <= layer) {
                layers.Add(new Layer());
            }
            switch (setting) {
                case LayerSetting.SetVolume: {
                        layers[layer].Volume = (sbyte)Math.Clamp(value, 0, 100);
                        break;
                    }
                case LayerSetting.AddVolume: {
                        layers[layer].Volume = (sbyte)Math.Clamp(layers[layer].Volume + value, 0, 100);
                        break;
                    }
                case LayerSetting.SetStereo: {
                        layers[layer].Stereo = (byte)Math.Clamp(value, 0, 200);
                        break;
                    }
                case LayerSetting.AddStereo: {
                        layers[layer].Stereo = (byte)Math.Clamp(layers[layer].Stereo + value, 0, 200);
                        break;
                    }
            }
            pbx_Layers.Invalidate();
        }

        /// <summary>
        /// Allows modifying layers
        /// SetMute
        /// SetSolo
        /// </summary>
        /// <param name="layer">Layer to modify (index)</param>
        /// <param name="setting">Layer setting to execute</param>
        /// <param name="option">Value to set to</param>
        private void ModifyLayer(int layer, LayerSetting setting, bool option) {
            if (layer < 0) {
                return;
            }
            while (layers.Count <= layer) {
                layers.Add(new Layer());
            }
            switch (setting) {
                case LayerSetting.SetMute: {
                        layers[layer].Lock = option;
                        break;
                    }
                case LayerSetting.SetSolo: {
                        layers[layer].Solo = option;
                        break;
                    }
            }
            pbx_Layers.Invalidate();
        }

        public void SetLayer(int layer, sbyte volume, byte stereo) {
            if (layer < 0) {
                return;
            }
            while (layers.Count <= layer) {
                layers.Add(new Layer());
            }
            layers[layer].Volume = volume;
            layers[layer].Stereo = stereo;
        }

        private void AddBlock(int x, int y) {
            AddBlock(x, y, new NoteBlock(x, y, (sbyte)instrumentSelected, keySelected, 100, 100, 0));
        }

        private void AddBlock(int x, int y, NoteBlock nb) {

            if (!connected) {
                NoteBlocksAdded++;

                if (!notes.ContainsKey(x))
                    notes[x] = new Dictionary<int, NoteBlock>();
                if (!notes[x].ContainsKey(y)) {
                    totalNotes++;
                }
                notes[x][y] = nb;

                if (y >= layers.Count) {
                    for (int i = layers.Count; i <= y; i++) {
                        layers.Add(new Layer());
                    }
                    pbx_Layers.Invalidate();
                }

                tsl_TotalNotes.Text = $"Total Notes: {totalNotes}";
                if (x >= farthestNoteX) {
                    farthestNoteX = x;
                }

                if (y >= farthestNoteY) {
                    farthestNoteY = y;
                }

                hScrollBar.Maximum = farthestNoteX;
                vScrollBar.Maximum = farthestNoteY;

                picBox.Invalidate(new Rectangle((x - hScrollBar.Value) * 32, ((y - vScrollBar.Value) * 32) + 32, 32, 32));
            } else {

            }
        }

        private void RemoveBlock(int x, int y) {

            NoteBlocksRemoved++;

            if (notes.ContainsKey(x) && notes[x].ContainsKey(y)) {
                notes[x].Remove(y);
                totalNotes--;
                if (notes[x].Count == 0) {
                    notes.Remove(x);
                    if (totalNotes == 0) {
                        farthestNoteX = 0;
                    } else {
                        farthestNoteX = notes.OrderByDescending(i => i.Key).First().Key;
                    }
                }
                if (y == farthestNoteY) {
                    if (totalNotes == 0) {
                        farthestNoteY = 0;
                    } else {
                        farthestNoteY = notes.Max(outer => outer.Value.Keys.Max());
                    }
                    pbx_Layers.Invalidate();
                }
                hScrollBar.Maximum = farthestNoteX;
                vScrollBar.Maximum = farthestNoteY;
                tsl_TotalNotes.Text = $"Total Notes: {totalNotes}";
            }
            picBox.Invalidate(new Rectangle((x - hScrollBar.Value) * 32, ((y - vScrollBar.Value) * 32) + 32, 32, 32));
        }

    }
}
