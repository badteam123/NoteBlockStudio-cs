using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlockStudioCS {
    public partial class Form1: Form {
        
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
                farthestNoteX = loader.SongLength;
                Debug.WriteLine(loader.MinutesSpent);
                hScrollBar.Maximum = farthestNoteX;

                SongName = loader.SongName;
                SongAuthor = loader.SongAuthor;
                SongOriginalAuthor = loader.SongOriginalAuthor;
                SongDescription = loader.SongDescription;
                SongTempo = loader.SongTempo;
                LeftClicks = loader.LeftClicks;
                RightClicks = loader.RightClicks;
                MinutesSpent = loader.MinutesSpent;
                NoteBlocksAdded = loader.NoteBlocksAdded;
                NoteBlocksRemoved = loader.NoteBlocksRemoved;
                MidiSchematicFileName = loader.MidiSchematicFileName;
                TimeSignature = loader.TimeSignature;
                Looping = loader.Looping;
                MaxLoopCount = loader.MaxLoopCount;
                LoopStartTick = loader.LoopStartTick;

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

        public void SaveFile() {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = ".nbs";
            if (sfd.ShowDialog() == DialogResult.OK) {
                Debug.WriteLine("Starting save...");
                using (FileStream s = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write)) {

                    // Part 1 - Header
                    s.Write(new byte[] { 0x00, 0x00 });
                    s.WriteByte((byte)(sbyte)5);     // Version
                    s.WriteByte(16);    // VanillaInstrumentCount
                    s.Write(BitConverter.GetBytes((short)farthestNoteX));
                    s.Write(BitConverter.GetBytes((short)layers.Count));
                    s.Write(BitConverter.GetBytes(SongName.Length));
                    s.Write(Encoding.UTF8.GetBytes(SongName));
                    s.Write(BitConverter.GetBytes(SongAuthor.Length));
                    s.Write(Encoding.UTF8.GetBytes(SongAuthor));
                    s.Write(BitConverter.GetBytes(SongOriginalAuthor.Length));
                    s.Write(Encoding.UTF8.GetBytes(SongOriginalAuthor));
                    s.Write(BitConverter.GetBytes(SongDescription.Length));
                    s.Write(Encoding.UTF8.GetBytes(SongDescription));
                    s.Write(BitConverter.GetBytes(SongTempo));
                    s.WriteByte(0x00);  // Auto saving
                    s.WriteByte(10);    // Auto saving duration
                    s.WriteByte(6);     // Time signature
                    s.Write(BitConverter.GetBytes(MinutesSpent));
                    s.Write(BitConverter.GetBytes(LeftClicks));
                    s.Write(BitConverter.GetBytes(RightClicks));
                    s.Write(BitConverter.GetBytes(NoteBlocksAdded));
                    s.Write(BitConverter.GetBytes(NoteBlocksRemoved));
                    s.Write(BitConverter.GetBytes(MidiSchematicFileName.Length));
                    s.Write(Encoding.UTF8.GetBytes(MidiSchematicFileName));
                    s.WriteByte((byte)Looping);
                    s.WriteByte((byte)MaxLoopCount);
                    s.Write(BitConverter.GetBytes(LoopStartTick));

                    // Part 2 - Note Blocks
                    int currentTick = -1;
                    int currentLayer = -1;

                    foreach(var tick in notes) {
                        short XOff = (short)(tick.Key - currentTick);
                        currentTick += XOff;
                        s.Write(BitConverter.GetBytes(XOff));
                        currentLayer = -1;
                        foreach (var layer in tick.Value) {
                            short YOff = (short)(layer.Key - currentLayer);
                            currentLayer += YOff;
                            s.Write(BitConverter.GetBytes(YOff));
                            s.WriteByte((byte)layer.Value.InstrumentNum);
                            s.WriteByte((byte)layer.Value.Key);
                            s.WriteByte((byte)layer.Value.Velocity);
                            s.WriteByte(layer.Value.Panning);
                            s.Write(BitConverter.GetBytes(layer.Value.Pitch));
                        }
                        s.Write(BitConverter.GetBytes((short)0));
                    }
                    s.Write(BitConverter.GetBytes((short)0));

                    // Part 3 - Layers
                    foreach(var layer in layers) {
                        s.Write(BitConverter.GetBytes(layer.Name.Length));
                        s.Write(Encoding.UTF8.GetBytes(layer.Name));
                        s.WriteByte((byte)layer.Locked);
                        s.WriteByte((byte)layer.Volume);
                        s.WriteByte(layer.Stereo);
                    }
                    s.WriteByte(0);

                    // Part 4 - Custom Instruments


                }
            }
            /*
            int length;
                // First 2 bytes are 0x00
                reader.ReadInt16();
                Version = reader.ReadSByte();
                VanillaInstrumentCount = reader.ReadSByte();
                SongLength = reader.ReadInt16();
                LayerCount = reader.ReadInt16();
                length = reader.ReadInt32();
                SongName = "";
                for (int i = 0; i < length; i++) {
                    SongName = SongName + (char)reader.ReadByte();
                }
                length = reader.ReadInt32();
                SongAuthor = "";
                for (int i = 0; i < length; i++) {
                    SongAuthor = SongAuthor + (char)reader.ReadByte();
                }
                length = reader.ReadInt32();
                SongOriginalAuthor = "";
                for (int i = 0; i < length; i++) {
                    SongOriginalAuthor = SongOriginalAuthor + (char)reader.ReadByte();
                }
                length = reader.ReadInt32();
                SongDescription = "";
                for (int i = 0; i < length; i++) {
                    SongDescription = SongDescription + (char)reader.ReadByte();
                }
                SongTempo = reader.ReadInt16();
                AutoSaving = reader.ReadSByte();
                AutoSavingDuration = reader.ReadSByte();
                TimeSignature = reader.ReadSByte();
                MinutesSpent = reader.ReadInt32();
                LeftClicks = reader.ReadInt32();
                RightClicks = reader.ReadInt32();
                NoteBlocksAdded = reader.ReadInt32();
                NoteBlocksRemoved = reader.ReadInt32();
                length = reader.ReadInt32();
                MidiSchematicFileName = "";
                for (int i = 0; i < length; i++) {
                    MidiSchematicFileName = MidiSchematicFileName + (char)reader.ReadByte();
                }
                Looping = reader.ReadSByte();
                MaxLoopCount = reader.ReadSByte();
                LoopStartTick = reader.ReadInt16();

                // READ IN NOTE BLOCK DATA

                int x = -1;
                int y = -1;
                while (true) {
                    short TickJump = reader.ReadInt16();
                    if (TickJump == 0) {
                        break;
                    }
                    x = x + TickJump;
                    while (true) {
                        short LayerJump = reader.ReadInt16();
                        y = y + LayerJump;
                        if (LayerJump == 0) {
                            y = -1;
                            break;
                        }
                        NoteBlocks.Add(new NoteBlock(x, y, reader.ReadSByte(), reader.ReadSByte(), reader.ReadSByte(), reader.ReadByte(), reader.ReadInt16()));
                    }
                }

                layers = new Layer[LayerCount];

                for (int l = 0; l < LayerCount; l++) {
                    length = reader.ReadInt32();
                    string tempName = "";
                    for (int i = 0; i < length; i++) {
                        tempName = tempName + (char)reader.ReadByte();
                    }
                    layers[l] = new Layer(tempName, reader.ReadSByte(), reader.ReadSByte(), reader.ReadByte());
                    Debug.WriteLine(layers[l].Volume);
                }
            */
        }

    }
}
