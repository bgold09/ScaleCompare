using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace ScaleCompare
{
    public class AudioPlaybackEngine : IDisposable
    {
        private readonly IWavePlayer outputDevice;
        private readonly MixingSampleProvider mixer;

        public static readonly AudioPlaybackEngine Instance = new AudioPlaybackEngine();

        public AudioPlaybackEngine(int sampleRate = 44100, int channelCount = 2)
        {
            this.outputDevice = new WaveOutEvent();
            this.mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount))
            {
                ReadFully = true
            };

            this.outputDevice.Init(mixer);
            this.outputDevice.Play();
        }

        public void PlayChord(IChordProvider chordProvider, TimeSpan duration)
        {
            IEnumerable<double> chordFrequencies = chordProvider.GetChord();

            foreach (var frequency in chordFrequencies)
            {
                var sine = new SignalGenerator()
                {
                    Gain = 0.2,
                    Frequency = frequency,
                    Type = SignalGeneratorType.Sin
                }.Take(duration);

                this.AddMixerInput(sine);
            }
        }

        public void PlayChord(IChordProvider chordProvider)
        {
            this.PlayChord(chordProvider, TimeSpan.FromSeconds(3));
        }

        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == mixer.WaveFormat.Channels)
            {
                return input;
            }

            if (input.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
            {
                return new MonoToStereoSampleProvider(input);
            }

            throw new NotImplementedException();
        }

        private void AddMixerInput(ISampleProvider input)
        {
            this.mixer.AddMixerInput(ConvertToRightChannelCount(input));
        }

        public void Dispose()
        {
            this.outputDevice.Dispose();
        }
    }
}
