using CSCore.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using Vortice.Multimedia;
using Vortice.XAudio2;
using System.Reflection.Metadata;

namespace NoteBlockStudioCS {
    internal class NoteSound {

        static IXAudio2 xaudio;
        static IXAudio2MasteringVoice master;
        static List<IXAudio2SourceVoice> sources;

        static private Dictionary<string, AudioFile> sounds;
        static private Dictionary<string, AudioBuffer> Buffers;

        static public Dictionary<string, SolidBrush> Brushes;

        static NoteSound() {

            sounds = new Dictionary<string, AudioFile>();
            Buffers = new Dictionary<string, AudioBuffer>();
            sources = new List<IXAudio2SourceVoice>();

            Brushes = new Dictionary<string, SolidBrush>();

            Brushes["harp"] =           new SolidBrush(Color.FromArgb(10, 62, 115));
            Brushes["dbass"] =          new SolidBrush(Color.FromArgb(35, 92, 43));
            Brushes["bdrum"] =          new SolidBrush(Color.FromArgb(127, 67, 69));
            Brushes["sdrum"] =          new SolidBrush(Color.FromArgb(127, 127, 10));
            Brushes["click"] =          new SolidBrush(Color.FromArgb(124, 62, 122));
            Brushes["guitar"] =         new SolidBrush(Color.FromArgb(87, 43, 33));
            Brushes["flute"] =          new SolidBrush(Color.FromArgb(127, 121, 57));
            Brushes["bell"] =           new SolidBrush(Color.FromArgb(127, 10, 127));
            Brushes["icechime"] =       new SolidBrush(Color.FromArgb(50, 93, 103));
            Brushes["xylobone"] =       new SolidBrush(Color.FromArgb(127, 127, 127));
            Brushes["iron_xylophone"] = new SolidBrush(Color.FromArgb(10, 94, 127));
            Brushes["cow_bell"] =       new SolidBrush(Color.FromArgb(127, 17, 20));
            Brushes["didgeridoo"] =     new SolidBrush(Color.FromArgb(127, 54, 21));
            Brushes["bit"] =            new SolidBrush(Color.FromArgb(10, 127, 10));
            Brushes["banjo"] =          new SolidBrush(Color.FromArgb(127, 10, 54));
            Brushes["pling"] =          new SolidBrush(Color.FromArgb(55, 55, 55));

            sounds["harp"] = new AudioFile("stuff/sounds/harp.wav");
            sounds["dbass"] = new AudioFile("stuff/sounds/dbass.wav");
            sounds["bdrum"] = new AudioFile("stuff/sounds/bdrum.wav");
            sounds["sdrum"] = new AudioFile("stuff/sounds/sdrum.wav");
            sounds["click"] = new AudioFile("stuff/sounds/click.wav");
            sounds["guitar"] = new AudioFile("stuff/sounds/guitar.wav");
            sounds["flute"] = new AudioFile("stuff/sounds/flute.wav");
            sounds["bell"] = new AudioFile("stuff/sounds/bell.wav");
            sounds["icechime"] = new AudioFile("stuff/sounds/icechime.wav");
            sounds["xylobone"] = new AudioFile("stuff/sounds/xylobone.wav");
            sounds["iron_xylophone"] = new AudioFile("stuff/sounds/iron_xylophone.wav");
            sounds["cow_bell"] = new AudioFile("stuff/sounds/cow_bell.wav");
            sounds["didgeridoo"] = new AudioFile("stuff/sounds/didgeridoo.wav");
            sounds["bit"] = new AudioFile("stuff/sounds/bit.wav");
            sounds["banjo"] = new AudioFile("stuff/sounds/banjo.wav");
            sounds["pling"] = new AudioFile("stuff/sounds/pling.wav");

            foreach(var sound in sounds) {
                Buffers[sound.Key] = new AudioBuffer(sound.Value.Buffer);
            }

            xaudio = XAudio2.XAudio2Create();
            master = xaudio.CreateMasteringVoice();

        }

        /// <summary>
        /// Initialize the static class
        /// </summary>
        public static void Init() {

        }

        public static void Clear() {
            for (int i = sources.Count - 1; i >= 0; i--) {
                if (sources[i].State.BuffersQueued == 0) {
                    sources[i].Stop();
                    sources[i].DestroyVoice();
                    sources[i].Dispose();
                    sources.RemoveAt(i);
                }
            }
            Debug.WriteLine($"Sounds playing: {sources.Count}");
        }

        public static void Play() {
            xaudio.CommitChanges(1);
        }

        public static void AddToPlayQueue(string type, float speed = 45.0f, float volume = 100.0f) {
            Stopwatch sw = Stopwatch.StartNew();
            float freqRatio = (float)Math.Pow(2, (speed - 45f) / 12);
            sources.Add(xaudio.CreateSourceVoice(sounds[type].WaveFormat, maxFrequencyRatio: 4f));
            int index = sources.Count - 1;
            sources[index].SetVolume(volume / 100f);
            sources[index].SubmitSourceBuffer(Buffers[type]);
            sources[index].SetFrequencyRatio(freqRatio, 0);
            sources[index].Start(1);
            sw.Stop();
        }

    }

}
