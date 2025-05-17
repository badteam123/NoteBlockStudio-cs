using CSCore.Streams;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlockStudioCS {
    internal class NoteSound {

        static WaveOutEvent waveOut;
        static MixingSampleProvider mixer;
        static List<CachedSoundSource> soundSources;

        static List<NoteSound> sounds = new List<NoteSound>();
        //CachedSound;

        private NoteSound() {
            
        }

        static void play(string type) {
            //CachedSound = 
        }

    }

    public class CachedSound {
        public float[] AudioData;
        public WaveFormat WaveFormat;

        public CachedSound(string filePath) {
            using var reader = new AudioFileReader(filePath);
            WaveFormat = reader.WaveFormat;

            List<float> samples = new List<float>();
            float[] buffer = new float[reader.WaveFormat.SampleRate * reader.WaveFormat.Channels];
            int samplesRead;
            while ((samplesRead = reader.Read(buffer, 0, buffer.Length)) > 0) {
                for (int i = 0; i < samplesRead; i++)
                    samples.Add(buffer[i]);
            }
            AudioData = samples.ToArray();
        }
    }

    // Creates a clone of the cached sound to play
    public class CachedSoundSampleProvider: ISampleProvider {
        private readonly CachedSound cachedSound;
        private int position;

        public CachedSoundSampleProvider(CachedSound sound) {
            cachedSound = sound;
        }

        public WaveFormat WaveFormat => cachedSound.WaveFormat;

        public int Read(float[] buffer, int offset, int count) {
            int availableSamples = cachedSound.AudioData.Length - position;
            int samplesToCopy = Math.Min(availableSamples, count);
            Array.Copy(cachedSound.AudioData, position, buffer, offset, samplesToCopy);
            position += samplesToCopy;
            return samplesToCopy;
        }
    }
}
