using System;

namespace ScaleCompare
{
    public class TwelveToneEqualTemperament : ITemperament
    {
        private const double DefaultReferencePitchFrequency = 440;

        private readonly double referencePitchFrequency;

        public TwelveToneEqualTemperament()
            : this(DefaultReferencePitchFrequency)
        {
        }

        public TwelveToneEqualTemperament(double referencePitchFrequency)
        {
            this.referencePitchFrequency = referencePitchFrequency;
        }

        public double GetPitch(int pianoNoteNumber)
        {
            return this.referencePitchFrequency *
                Math.Pow(Math.Pow(2, 1.0/12), pianoNoteNumber - 49);
        }
    }
}
