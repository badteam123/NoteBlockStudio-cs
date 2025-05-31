using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlockStudioCS {
    public abstract class Packet {

        public enum PacketType : byte {
            PlaceNote = 0,
            RemoveNote = 1,
            EditNote = 2,
            EditLayer = 3,
            ChangeTempo = 4
        }

        public abstract PacketType Type { get; }

        /// <summary>
        /// Write only the type-specific data here.
        /// </summary>
        protected abstract void WritePayload(BinaryWriter writer);

        /// <summary>
        /// Serializes the packet into [ type(1) | payload… ].
        /// </summary>
        public byte[] ToBytes() {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);

            bw.Write((byte)Type);
            WritePayload(bw);

            return ms.ToArray();
        }

        /// <summary>
        /// Reads a length-prefixed packet from the stream, then dispatches by type.
        /// </summary>
        public static async Task<Packet> ReceiveAsync(NetworkStream ns, CancellationToken ct) {
            // 1) read the 4-byte length prefix
            var lenBuf = new byte[4];
            await ReadExactAsync(ns, lenBuf, 0, 4, ct);
            int payloadLen = BitConverter.ToInt32(lenBuf, 0);

            // 2) read the packet data
            var data = new byte[payloadLen];
            await ReadExactAsync(ns, data, 0, payloadLen, ct);

            // 3) dispatch
            using var ms = new MemoryStream(data);
            using var br = new BinaryReader(ms);

            var type = (PacketType)br.ReadByte();
            return type switch {
                PacketType.PlaceNote => PlaceNotePacket.FromReader(br),
                PacketType.RemoveNote => RemoveNotePacket.FromReader(br),
                PacketType.EditNote => EditNotePacket.FromReader(br),
                PacketType.EditLayer => EditLayerPacket.FromReader(br),
                PacketType.ChangeTempo => ChangeTempoPacket.FromReader(br),
                _ => throw new InvalidDataException($"Unknown packet type {type}")
            };
        }

        static async Task ReadExactAsync(Stream s, byte[] buf, int off, int count, CancellationToken ct) {
            int read, tot = 0;
            while (tot < count) {
                read = await s.ReadAsync(buf, off + tot, count - tot, ct);
                if (read == 0) throw new IOException("socket closed");
                tot += read;
            }
        }

    }

    public class PlaceNotePacket: Packet {

        public override PacketType Type => PacketType.PlaceNote;

        public int X, Y;
        public Form1.ins Instrument;
        public sbyte Key;

        public PlaceNotePacket(int x, int y, Form1.ins ins, sbyte key) {
            X = x;
            Y = y;
            Instrument = ins;
            Key = key;
        }

        /// <summary>
        /// This packet type's data to be sent
        /// </summary>
        /// <param name="writer">BinaryWriter used</param>
        protected override void WritePayload(BinaryWriter writer) {
            writer.Write(X);
            writer.Write(Y);
            writer.Write((byte)Instrument);
            writer.Write(Key);
        }

        /// <summary>
        /// Get a PlaceNotePacket from binary data
        /// </summary>
        /// <param name="br">BinaryReader used</param>
        /// <returns>A PlaceNotePacket Packet</returns>
        public static PlaceNotePacket FromReader(BinaryReader br) {
            int x = br.ReadInt32();
            int y = br.ReadInt32();
            Form1.ins ins = (Form1.ins)br.ReadByte();
            sbyte key = br.ReadSByte();
            return new PlaceNotePacket(x, y, ins, key);
        }

    }

    public class RemoveNotePacket: Packet {

        public override PacketType Type => PacketType.RemoveNote;

        public int X, Y;

        public RemoveNotePacket(int x, int y) {
            X = x;
            Y = y;
        }

        /// <summary>
        /// This packet type's data to be sent
        /// </summary>
        /// <param name="writer">BinaryWriter used</param>
        protected override void WritePayload(BinaryWriter writer) {
            writer.Write(X);
            writer.Write(Y);
        }

        /// <summary>
        /// Get a RemoveNotePacket from binary data
        /// </summary>
        /// <param name="br">BinaryReader used</param>
        /// <returns>A RemoveNotePacket Packet</returns>
        public static RemoveNotePacket FromReader(BinaryReader br) {
            int x = br.ReadInt32();
            int y = br.ReadInt32();
            return new RemoveNotePacket(x, y);
        }

    }

    public class EditNotePacket: Packet {

        public override PacketType Type => PacketType.EditNote;

        public int X;
        public int Y;
        public Form1.ins Instrument;
        public sbyte Key;
        public sbyte Velocity;
        public byte Panning;
        public short Pitch;

        public EditNotePacket(int x, int y, Form1.ins ins, sbyte key, sbyte velocity, byte panning, short pitch) {
            X = x;
            Y = y;
            Instrument = ins;
            Key = key;
            Velocity = velocity;
            Panning = panning;
            Pitch = pitch;
        }

        /// <summary>
        /// This packet type's data to be sent
        /// </summary>
        /// <param name="writer">BinaryWriter used</param>
        protected override void WritePayload(BinaryWriter writer) {
            writer.Write(X);
            writer.Write(Y);
            writer.Write((byte)Instrument);
            writer.Write(Key);
            writer.Write(Velocity);
            writer.Write(Panning);
            writer.Write(Pitch);
        }

        /// <summary>
        /// Get a EditNotePacket from binary data
        /// </summary>
        /// <param name="br">BinaryReader used</param>
        /// <returns>A EditNotePacket Packet</returns>
        public static EditNotePacket FromReader(BinaryReader br) {
            int x = br.ReadInt32();
            int y = br.ReadInt32();
            Form1.ins ins = (Form1.ins)br.ReadByte();
            sbyte key = br.ReadSByte();
            sbyte velocity = br.ReadSByte();
            byte panning = br.ReadByte();
            short pitch = br.ReadInt16();
            return new EditNotePacket(x, y, ins, key, velocity, panning, pitch);
        }

    }

    public class EditLayerPacket: Packet {

        public override PacketType Type => PacketType.EditLayer;

        public int Layer;
        public sbyte Velocity;
        public byte Stereo;

        public EditLayerPacket(int layer, sbyte velocity, byte stereo) {
            Layer = layer;
            Velocity = velocity;
            Stereo = stereo;
        }

        /// <summary>
        /// This packet type's data to be sent
        /// </summary>
        /// <param name="writer">BinaryWriter used</param>
        protected override void WritePayload(BinaryWriter writer) {
            writer.Write(Layer);
            writer.Write(Velocity);
            writer.Write(Stereo);
        }

        /// <summary>
        /// Get a EditLayerPacket from binary data
        /// </summary>
        /// <param name="br">BinaryReader used</param>
        /// <returns>A EditLayerPacket Packet</returns>
        public static EditLayerPacket FromReader(BinaryReader br) {
            int layer = br.ReadInt32();
            sbyte velocity = br.ReadSByte();
            byte stereo = br.ReadByte();
            return new EditLayerPacket(layer, velocity, stereo);
        }

    }

    public class ChangeTempoPacket: Packet {

        public override PacketType Type => PacketType.ChangeTempo;

        public short Tempo;

        public ChangeTempoPacket(short tempo) {
            Tempo = tempo;
        }

        /// <summary>
        /// This packet type's data to be sent
        /// </summary>
        /// <param name="writer">BinaryWriter used</param>
        protected override void WritePayload(BinaryWriter writer) {
            writer.Write(Tempo);
        }

        /// <summary>
        /// Get a ChangeTempoPacket from binary data
        /// </summary>
        /// <param name="br">BinaryReader used</param>
        /// <returns>A ChangeTempoPacket Packet</returns>
        public static ChangeTempoPacket FromReader(BinaryReader br) {
            short tempo = br.ReadInt16();
            return new ChangeTempoPacket(tempo);
        }

    }

}
