using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlockStudioCS {
    public partial class Form1: Form {

        private void ScrollPlayer(object sender, MouseEventArgs e) {
            //hScrollBar.Value = Math.Max(hScrollBar.Value - Math.Sign(e.Delta), 0);
            //NoteSound.Play("harp", ((float)volumeBar.Value) / 100f);
            hScrollBar.Value = Math.Clamp(hScrollBar.Value - Math.Sign(e.Delta), 0, hScrollBar.Maximum);
            picBox.Invalidate();
        }

        private void ScrollLayer(object sender, MouseEventArgs e) {
            int posX = (e.X - 5) / 20;
            int posY = e.Y / 32;
            int idx = posY + vScrollBar.Value;

            if (posX < 0 || posX > 1) return;

            while (layers.Count <= idx) {
                layers.Add(new Layer());
            }

            Debug.WriteLine($"count: {layers.Count}");

            switch (posX) {
                case 0: {
                        int delta = (int)Math.Sign(e.Delta) * (keyStates[Keys.ShiftKey] ? 1 : 10);
                        var L = layers[idx];
                        ModifyLayer(idx, LayerSetting.AddLayer, delta);
                        //L.Volume = (sbyte)Math.Clamp(L.Volume + delta, 0, 100);
                        break;
                    }
                case 1: {
                        int delta = (int)Math.Sign(e.Delta) * (keyStates[Keys.ShiftKey] ? 1 : 10);
                        var L = layers[idx];
                        ModifyLayer(idx, LayerSetting.AddStereo, delta);
                        //L.Stereo = (byte)Math.Clamp(L.Stereo + delta, 0, 200);
                        break;
                    }
            }

            pbx_Layers.Invalidate(new Rectangle((posX * 20) - 5, posY * 32, 40, 32));
        }

        private void volumeBar_Scroll(object sender, EventArgs e) {
            lblVolume.Text = volumeBar.Value.ToString() + "%";
            volume = volumeBar.Value;
        }

        private void ScrollBar_Scroll(object sender, ScrollEventArgs e) {
            picBox.Invalidate();
            pbx_Layers.Invalidate();
        }

        private void btnPlay_Click(object sender, EventArgs e) {
            LeftClicks++;
            if (playing)
                timer.Stop();
            //if (notes.Count >= 1) {
            timer.SetPeriod((int)Math.Round(1000f / ((double)num_TPS.Value)));
            timer.Start();
            playing = true;
            //}
        }

        private void btnStop_Click(object sender, EventArgs e) {
            LeftClicks++;
            if (playing) {
                timer.Stop();
                playing = false;
            } else {
                playbackPosition = 0;
                hScrollBar.Value = 0;
                picBox.Invalidate();
            }
            tsl_SoundsPlaying.Text = $"Sounds Playing: {NoteSound.GetSoundsPlaying()}";
        }

        private void btnSave_Click(object sender, EventArgs e) {
            LeftClicks++;
            SaveFile();
        }

        private void num_TPS_ValueChanged(object sender, EventArgs e) {
            SongTempo = (short)num_TPS.Value;
            SongTempo *= 100;
            timer.SetPeriod((int)Math.Round(1000f / (SongTempo / 100f)));
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

            if(pbx_Layers.Height != (displayHeight - 1) * 32) {
                pbx_Layers.Height = (displayHeight - 1) * 32;
                pbx_Layers.Invalidate();
            }

            // Piano
            pbx_Piano.Invalidate();
        }

        private void picBox_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                LeftClicks++;
            } else if (e.Button == MouseButtons.Right) {
                RightClicks++;
            }
            int mouseX = (e.X / 32) + hScrollBar.Value;
            int mouseY = ((e.Y / 32) - 1) + vScrollBar.Value;
            if (mouseY >= 0) {
                if (e.Button == MouseButtons.Left) {
                    AddBlock(mouseX, mouseY);
                    NoteSound.PlaySingle(InsToString(instrumentSelected), keySelected, volume);
                } else if (e.Button == MouseButtons.Right) {
                    RemoveBlock(mouseX, mouseY);
                } else {

                }
            } else {
                if (!playing) {
                    playbackPosition = mouseX;
                    picBox.Invalidate();
                    if (notes.ContainsKey(mouseX)) {
                        foreach (var i in notes[mouseX]) {
                            NoteSound.AddToPlayQueue(i.Value.Instrument, i.Value.Key, (i.Value.Velocity * (layers[i.Key].Volume / 100f)) * (volume / 100f));
                        }
                        NoteSound.Play();
                    }
                }
            }
        }

        private void pbx_Piano_MouseDown(object sender, MouseEventArgs e) {

            if (e.Button == MouseButtons.Left) {
                LeftClicks++;
            } else if (e.Button == MouseButtons.Right) {
                RightClicks++;
            }

            int centerKey = 45;          // your “always centered” key
            int keyWidth = 32;          // width of each key in pixels
            int W = pbx_Piano.ClientSize.Width;

            // how many on each side
            int sideCount = (W / keyWidth) / 2 + 1;
            int totalKeys = sideCount * 2 + 1;
            int usedWidth = totalKeys * keyWidth;
            int insetX = (W - usedWidth) / 2;

            // logical index of leftmost drawn key
            int leftKey = centerKey - sideCount;

            // mouse X relative to the keyed area
            int localX = e.X - insetX;

            // if you clicked outside the drawn keys, ignore
            if (localX < 0 || localX >= usedWidth)
                return;  // clicked in the margin

            // integer‐divide to find which key slot
            int slot = localX / keyWidth;       // 0..totalKeys-1
            int clickedKey = leftKey + slot;    // the actual key number

            if (clickedKey != keySelected) {
                keySelected = (sbyte)clickedKey;
                pbx_Piano.Invalidate();
            }
            NoteSound.PlaySingle(InsToString(instrumentSelected), keySelected, volume);

        }

        private void pbx_Layers_MouseDown(object sender, MouseEventArgs e) {

            if (e.Button == MouseButtons.Left) {
                LeftClicks++;
            } else if (e.Button == MouseButtons.Right) {
                RightClicks++;
            }
            LayersMouse = new Point((e.X - 5) / 20, e.Y / 32);

            switch (LayersMouse.X) {
                case 2: // Mute
                    ModifyLayer(LayersMouse.Y + vScrollBar.Value, LayerSetting.ToggleMute);
                    break;
                case 3: // Solo
                    ModifyLayer(LayersMouse.Y + vScrollBar.Value, LayerSetting.ToggleSolo);
                    break;
                case 4: // Select all in layer
                    //ModifyLayer(LayersMouse.Y, LayerSetting.ToggleMute);
                    break;
                case 5: // Add Layer
                    break;
                case 6: // Delete Layer
                    break;
                case 7: // Up Layer
                    break;
                case 8: // Down Layer
                    break;
            }
        }

        private void pbx_Layers_MouseMove(object sender, MouseEventArgs e) {
            Point temp = new Point((e.X - 5) / 20, e.Y / 32);

            if(temp != LayersMouse) {
                pbx_Layers.Invalidate(new Rectangle((LayersMouse.X * 20)-5, LayersMouse.Y * 32, 40, 32));
                LayersMouse = temp;
                pbx_Layers.Invalidate(new Rectangle((LayersMouse.X * 20)-5, LayersMouse.Y * 32, 40, 32));
            }
            
        }

        private void pbx_Layers_MouseLeave(object sender, EventArgs e) {
            LayersMouse = new Point(-1, -1);
            pbx_Layers.Invalidate();
        }

    }
}
