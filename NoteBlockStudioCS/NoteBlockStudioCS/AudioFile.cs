using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;
using Vortice.Multimedia;

namespace NoteBlockStudioCS {
    internal class AudioFile {

        public string Name { get; private set; }
        public TimeSpan Length { get; private set; }
        public string FilePath { get; private set; }
        public int SampleRate { get; private set; }
        public int Channels { get; private set; }
        public int Bits { get; private set; }
        public int KBitsPerSec { get; private set; }
        public byte[] Buffer { get; private set; }
        public Vortice.Multimedia.WaveFormat WaveFormat { get; private set; }

        public AudioFile(string filePath) {
            using var audioFileReader = new AudioFileReader(filePath);
            Name = Path.GetFileName(filePath);
            Length = audioFileReader.TotalTime;
            FilePath = filePath;
            SampleRate = audioFileReader.WaveFormat.SampleRate;
            Channels = audioFileReader.WaveFormat.Channels;
            Bits = audioFileReader.WaveFormat.BitsPerSample;
            KBitsPerSec = audioFileReader.WaveFormat.AverageBytesPerSecond / 1000;
            using var ms = new MemoryStream();
            audioFileReader.CopyTo(ms);
            Buffer = ms.ToArray();
            WaveFormat = new Vortice.Multimedia.WaveFormat(SampleRate, Bits, Channels);
        }
    }
}
