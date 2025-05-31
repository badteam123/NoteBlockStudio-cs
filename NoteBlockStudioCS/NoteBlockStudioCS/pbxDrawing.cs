using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlockStudioCS {
    public partial class Form1: Form {

        private void picBox_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            //picBox.Invalidate();

            // Top stuff
            g.FillRectangle(brushGray, 0, 0, picBox.Width, 32);

            for (int i = hScrollBar.Value; i < hScrollBar.Value + displayWidth; i++) {
                if (i % 4 == 0) {
                    g.DrawLine(penGray1, (i - hScrollBar.Value) * 32, 32, (i - hScrollBar.Value) * 32, picBox.Height);
                    g.DrawString(i.ToString(), DefaultFont, brushLightGray, (i - hScrollBar.Value) * 32, 24, centered);
                } else {
                    g.DrawLine(penGray2, (i - hScrollBar.Value) * 32, 32, (i - hScrollBar.Value) * 32, picBox.Height);
                }
            }

            SolidBrush white = new SolidBrush(Color.FromArgb(255, 255, 255));
            foreach (int x in notes.Keys) {
                foreach (int y in notes[x].Keys) {
                    if (y - vScrollBar.Value >= 0) {
                        g.FillRectangle(NoteSound.Brushes[notes[x][y].Instrument], (notes[x][y].X - hScrollBar.Value) * 32, (notes[x][y].Y - vScrollBar.Value + 1) * 32, 32, 32);
                        g.DrawString((notes[x][y].Key - 33).ToString(), DefaultFont, white, ((notes[x][y].X - hScrollBar.Value) * 32) + 16, ((notes[x][y].Y - vScrollBar.Value + 1) * 32) + 16, centered);
                    }
                }
            }

            // Playback column highlight
            g.FillRectangle(brushSelected, (playbackPosition - hScrollBar.Value) * 32, 32, 32, (displayHeight * 32));

            // Lines at top
            g.DrawLine(penLightGray, 0, 32, picBox.Width, 32);
            g.DrawLine(penLightGray, 0, 16, picBox.Width, 16);

        }

        private void pbx_Piano_Paint(object sender, PaintEventArgs e) {

            base.OnPaint(e);
            Graphics g = e.Graphics;

            string[] keyNames = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
            //string[] keyNames = { "A", "B", "C", "D", "E", "F", "G" };

            // 1. Configuration
            int centerKey = 45;           // center index
            int keyWidth = 32;            // width of each key in pixels
            int W = pbx_Piano.ClientSize.Width;
            int H = pbx_Piano.ClientSize.Height;

            // 2. How many keys per side?
            //    Always draw an odd count: 2*side + 1
            int sideCount = (W / keyWidth) / 2 + 1;

            // 3. Total number of keys we’ll draw
            int totalKeys = sideCount * 2 + 1;

            // 4. Compute inset so the block of keys is centered
            int usedWidth = totalKeys * keyWidth;
            int insetX = (W - usedWidth) / 2;

            // 5. Loop from leftmost logical key to rightmost
            int leftKey = centerKey - sideCount;
            using var pen = new Pen(Color.Gray);

            int skipped = 0;
            for (int i = 0; i < totalKeys; i++) {
                int keyIndex = leftKey + i;
                int x = insetX + i * keyWidth;

                if (keyIndex % 12 == 1 || keyIndex % 12 == 4 || keyIndex % 12 == 6 || keyIndex % 12 == 9 || keyIndex % 12 == 11) {
                    g.FillRectangle(Brushes.Black, x, 0, keyWidth, H);
                }

                // Highlight the selected key
                if (keyIndex == keySelected)
                    g.FillRectangle(brushKeySelected, x, 0, keyWidth, H);

                // Draw the vertical line (key boundary)
                g.DrawLine(pen, x, 0, x, H);

                // Draw the key number beneath it
                g.DrawString(keyIndex.ToString(), this.Font, Brushes.White, new RectangleF(x, H - 20, keyWidth, 20), centered);
                g.DrawString(keyNames[((keyIndex % 12) + 12) % 12], this.Font, Brushes.White, new RectangleF(x, H - 40, keyWidth, 20), centered);
            }

            // Final right boundary line
            g.DrawLine(pen, insetX + totalKeys * keyWidth, 0, insetX + totalKeys * keyWidth, H);

        }

        private void pbx_Layers_Paint(object sender, PaintEventArgs e) {
            var g = e.Graphics;

            for (int i = 0; i < displayHeight; i++) {
                
                // Display base stuff

                // Volume
                g.FillRectangle(Brushes.WhiteSmoke, 6, 7 + (i * 32), 18, 18);
                if (i + vScrollBar.Value < layers.Count) {
                    int offset = 16 - (layers[i + vScrollBar.Value].Volume * 16) / 100;
                    g.FillRectangle(Brushes.Black, 7, 8 + (i * 32), 16, 16);
                    g.FillRectangle(Brushes.Green, 7, 8 + (i * 32) + offset, 16, 16 - offset);
                } else {
                    g.FillRectangle(Brushes.Green, 7, 8 + (i * 32), 16, 16);
                }

                // Stereo
                g.DrawLine(Pens.WhiteSmoke, 28, 16 + (i * 32), 42, 16 + (i * 32));
                if (i + vScrollBar.Value < layers.Count) {
                    int offset = (layers[i + vScrollBar.Value].Stereo * 14) / 200;
                    g.FillRectangle(Brushes.LightBlue, 27 + offset, 13 + (i * 32), 3, 7);
                } else {
                    g.FillRectangle(Brushes.LightBlue, 34, 13 + (i * 32), 3, 7);
                }

                // Mute
                if (i + vScrollBar.Value < layers.Count) {
                    if(layers[i + vScrollBar.Value].Lock) {
                        g.FillRectangle(Brushes.DarkRed, 46, 7 + (i * 32), 18, 18);
                    } else {
                        g.FillRectangle(Brushes.Green, 46, 7 + (i * 32), 18, 18);
                    }
                } else {
                    g.FillRectangle(Brushes.Green, 46, 7 + (i * 32), 18, 18);
                }
                g.DrawString($"M", Font, Brushes.White, 55, (i * 32) + 16, centered);

                // Solo
                if (i + vScrollBar.Value < layers.Count) {
                    if (layers[i + vScrollBar.Value].Solo) {
                        g.FillRectangle(Brushes.DarkRed, 66, 7 + (i * 32), 18, 18);
                    } else {
                        g.FillRectangle(Brushes.Green, 66, 7 + (i * 32), 18, 18);
                    }
                } else {
                    g.FillRectangle(Brushes.Green, 66, 7 + (i * 32), 18, 18);
                }
                g.DrawString($"S", Font, Brushes.White, 75, (i * 32) + 16, centered);

                // Select all
                g.FillRectangle(Brushes.DimGray, 86, 7 + (i * 32), 18, 18);

                // Add Layer
                g.FillRectangle(Brushes.DimGray, 106, 7 + (i * 32), 18, 18);

                // Delete Layer
                g.FillRectangle(Brushes.DimGray, 126, 7 + (i * 32), 18, 18);

                // Up
                g.FillRectangle(Brushes.DimGray, 146, 7 + (i * 32), 18, 18);

                // Down
                g.FillRectangle(Brushes.DimGray, 166, 7 + (i * 32), 18, 18);



                // Change on mouse hover
                if(i == LayersMouse.Y) {
                    g.FillRectangle(brushDarken, (LayersMouse.X * 20) + 5, (i * 32) + 6, 20, 20);
                    if (LayersMouse.X == 0) {
                        if (i + vScrollBar.Value < layers.Count) {
                            g.DrawString($"{layers[i].Volume}%", Font, Brushes.White, 15, (i * 32) + 16, centered);
                        } else {
                            g.DrawString($"100%", Font, Brushes.White, 15, (i * 32) + 16, centered);
                        }
                    } else if(LayersMouse.X == 1) {
                        if (i + vScrollBar.Value < layers.Count) {
                            g.DrawString($"{layers[i].Stereo-100}", Font, Brushes.White, 35, (i * 32) + 16, centered);
                        } else {
                            g.DrawString($"0", Font, Brushes.White, 35, (i * 32) + 16, centered);
                        }
                    }
                    
                }
                
            }
        }

    }
}
