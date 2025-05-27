using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace NoteBlockStudioCS {
    internal class NBSLoader {
        public sbyte Version { get; private set; }
        public sbyte VanillaInstrumentCount { get; private set; }
        public short SongLength { get; private set; }
        public short LayerCount { get; private set; }
        public string SongName { get; private set; }
        public string SongAuthor { get; private set; }
        public string SongOriginalAuthor { get; private set; }
        public string SongDescription { get; private set; }
        public short SongTempo { get; private set; }
        public sbyte AutoSaving { get; private set; }
        public sbyte AutoSavingDuration { get; private set; }
        public sbyte TimeSignature { get; private set; }
        public int MinutesSpent { get; private set; }
        public int LeftClicks { get; private set; }
        public int RightClicks { get; private set; }
        public int NoteBlocksAdded { get; private set; }
        public int NoteBlocksRemoved { get; private set; }
        public string MidiSchematicFileName { get; private set; }
        public sbyte Looping { get; private set; }
        public sbyte MaxLoopCount { get; private set; }
        public short LoopStartTick { get; private set; }

        public List<NoteBlock> NoteBlocks { get; private set; }

        public Layer[] layers { get; private set; }

        public NBSLoader() {
            NoteBlocks = new List<NoteBlock>();
        }

        public void LoadFile(string filePath) {
            // Open the file with FileStream for byte-by-byte reading
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs)) {

                // READ IN THE HEADER FILE

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

            }

            /*
             * Version: {Version}\nVanillaInstrumentCount: {VanillaInstrumentCount}\nSongLength: {SongLength}\nLayerCount: {LayerCount}\nSongName: {SongName}\nSongAuthor: {SongAuthor}\nSongOriginalAuthor: {SongOriginalAuthor}\nSongDescription: {SongDescription}\nSongTempo: {SongTempo}\nAutoSaving: {AutoSaving}\nAutoSavingDuration: {AutoSavingDuration}\nTimeSignature: {TimeSignature}\nMinutesSpent: {MinutesSpent}\nLeftClicks: {LeftClicks}\nRightClicks: {RightClicks}\nNoteBlocksAdded: {NoteBlocksAdded}\nNoteBlocksRemoved: {NoteBlocksRemoved}\nMidiSchematicFileName: {MidiSchematicFileName}\nLooping: {Looping}\nMaxLoopCount: {MaxLoopCount}\nLoopStartTick: {LoopStartTick}\n
            */
             // MessageBox.Show($"Version: {Version}\nVanillaInstrumentCount: {VanillaInstrumentCount}\nSongLength: {SongLength}\nLayerCount: {LayerCount}\nSongName: {SongName}\nSongAuthor: {SongAuthor}\nSongOriginalAuthor: {SongOriginalAuthor}\nSongDescription: {SongDescription}\nSongTempo: {SongTempo}\nAutoSaving: {AutoSaving}\nAutoSavingDuration: {AutoSavingDuration}\nTimeSignature: {TimeSignature}\nMinutesSpent: {MinutesSpent}\nLeftClicks: {LeftClicks}\nRightClicks: {RightClicks}\nNoteBlocksAdded: {NoteBlocksAdded}\nNoteBlocksRemoved: {NoteBlocksRemoved}\nMidiSchematicFileName: {MidiSchematicFileName}\nLooping: {Looping}\nMaxLoopCount: {MaxLoopCount}\nLoopStartTick: {LoopStartTick}\n");

        }
    }

    internal class NoteBlock {
        public int X;
        public int Y;
        public sbyte InstrumentNum;
        public sbyte Key;
        public sbyte Velocity;
        public byte Panning;
        public short Pitch;

        public string Instrument;

        public NoteBlock(int x, int y, sbyte instrument, sbyte key, sbyte velocity, byte panning, short pitch) {
            X = x;
            Y = y;
            InstrumentNum = instrument;
            Key = key;
            Velocity = velocity;
            Panning = panning;
            Pitch = pitch;
            switch (InstrumentNum) {
                case 0:
                    Instrument = "harp";
                    break;
                case 1:
                    Instrument = "dbass";
                    break;
                case 2:
                    Instrument = "bdrum";
                    break;
                case 3:
                    Instrument = "sdrum";
                    break;
                case 4:
                    Instrument = "click";
                    break;
                case 5:
                    Instrument = "guitar";
                    break;
                case 6:
                    Instrument = "flute";
                    break;
                case 7:
                    Instrument = "bell";
                    break;
                case 8:
                    Instrument = "icechime";
                    break;
                case 9:
                    Instrument = "xylobone";
                    break;
                case 10:
                    Instrument = "iron_xylophone";
                    break;
                case 11:
                    Instrument = "cow_bell";
                    break;
                case 12:
                    Instrument = "didgeridoo";
                    break;
                case 13:
                    Instrument = "bit";
                    break;
                case 14:
                    Instrument = "banjo";
                    break;
                case 15:
                    Instrument = "pling";
                    break;
            }
        }

        public NoteBlock(NoteBlock copy) : this(copy.X, copy.Y, copy.InstrumentNum, copy.Key, copy.Velocity, copy.Panning, copy.Pitch) {

        }
    }

    internal class Layer {
        // Used in save
        public string Name;
        public sbyte Locked;
        public sbyte Volume;
        public byte Stereo;

        // Unused in save
        public bool Solo;
        public bool Lock { get { return Locked == 1; } set { Locked = (sbyte)(value ? 1 : 0); } }

        public Layer() : this("", 0, 100, 100) { }

        public Layer(string name, sbyte locked, sbyte volume, byte stereo) {
            Name = name;
            Locked = locked;
            Volume = volume;
            Stereo = stereo;
            Solo = false;
        }

        public bool IsSafeToDelete(int layerIndex, int highestYNote) {
            if (layerIndex > highestYNote && Volume == 100 && Stereo == 100 && Locked == 0) {
                return true;
            }
            return false;
        }
    }
}
