using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
        public string Name;
        public sbyte Locked;
        public sbyte Volume;
        public byte Stereo;

        public Layer(string name, sbyte locked, sbyte volume, byte stereo) {
            Name = name;
            Locked = locked;
            Volume = volume;
            Stereo = stereo;
        }
    }
}
