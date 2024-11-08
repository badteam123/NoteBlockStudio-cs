using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

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

        StringFormat centered = new StringFormat();

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

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
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
            // Get the dragged file(s)
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 1) {
                string filePath = files[0];
                // Process the file here
                loader.LoadFile(filePath);
                List<NoteBlock> tempList = new List<NoteBlock>(loader.NoteBlocks);

                for(int b=0; b<tempList.Count; b++) {
                    if (!notes.ContainsKey(tempList[b].X)) {
                        notes[tempList[b].X] = new Dictionary<int, NoteBlock>();
                    }
                    if (!notes[tempList[b].X].ContainsKey(tempList[b].Y)) {
                        notes[tempList[b].X][tempList[b].Y] = new NoteBlock(tempList[b]);
                    }
                }

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
            displayWidth = (this.Width - 786 + 512) / 32;
            displayHeight = (this.Height - 344 + 224) / 32;
            hScrollBar.Location = new Point(235, 61 + (displayHeight * 32));
            hScrollBar.Size = new Size(displayWidth * 32, hScrollBar.Size.Height);
            vScrollBar.Location = new Point(238 + (displayWidth * 32), 58);
            vScrollBar.Size = new Size(vScrollBar.Size.Width, displayHeight * 32);
            picBox.Width = displayWidth*32;
            picBox.Height = displayHeight*32;
        }

        private void volumeBar_Scroll(object sender, EventArgs e) {
            lblVolume.Text = volumeBar.Value.ToString() + "%";
        }

        private void btnPlay_Click(object sender, EventArgs e) {

        }

        private void visualUpdate_Tick(object sender, EventArgs e) {
            picBox.Invalidate();
        }

        private void picBox_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            picBox.Invalidate();

            g.FillRectangle(brushGray, 0, 0, picBox.Width, 32);

            g.DrawLine(penLightGray, 0, 32, picBox.Width, 32);
            g.DrawLine(penLightGray, 0, 16, picBox.Width, 16);
            
            for(int i = 0; i < displayWidth; i++) {
                if(i % 4 == 0) {
                    g.DrawLine(penGray1, i * 32, 32, i * 32, picBox.Height);
                    g.DrawString(i.ToString(), DefaultFont, brushLightGray, i * 32, 24, centered);
                } else {
                    g.DrawLine(penGray2, i * 32, 32, i * 32, picBox.Height);
                }
            }

            SolidBrush temp = new SolidBrush(Color.FromArgb(100, 100, 200));
            SolidBrush white = new SolidBrush(Color.FromArgb(255, 255, 255));
            foreach (int x in notes.Keys) {
                foreach (int y in notes[x].Keys) {
                    g.FillRectangle(temp, (notes[x][y].X) * 32, (notes[x][y].Y+1) * 32, 32, 32);
                    g.DrawString((notes[x][y].Key-33).ToString(), DefaultFont, white, ((notes[x][y].X) * 32)+16, ((notes[x][y].Y + 1) * 32)+16);
                }
            }

        }

        private void picBox_MouseClick(object sender, MouseEventArgs e) {
            int mouseX = e.X/32;
            int mouseY = e.Y/32;
        }
    }
}
