using System;

namespace ScaleCompare
{
    public class ChordProvider : IChordProvider
    {
        private readonly ITemperament temperament;

        public ChordProvider()
            : this(new TwelveToneEqualTemperament())
        {
        }

        public ChordProvider(ITemperament temperament)
        {
            this.temperament = temperament ?? throw new ArgumentNullException(nameof(temperament));
        }

        public double[] GetMajorChord(int rootNote)
        {
            return new[]
            {
                this.temperament.GetPitch(rootNote),
                this.temperament.GetPitch(rootNote + 4),
                this.temperament.GetPitch(rootNote + 7),
            };
        }
    }
}
