using System;
using Xunit;

namespace ScaleCompare.Tests
{
    public class TwelveToneEqualTemperamentTests
    {
        [Theory]
        [InlineData(40, 261.626)]
        [InlineData(46, 369.994)]
        [InlineData(49, 440)]
        public void GetsCorrectPitchForNote(int pianoNoteNumber, double expectedFrequency)
        {
            var temperament = new TwelveToneEqualTemperament();

            double actualFrequency = temperament.GetPitch(pianoNoteNumber);

            Assert.Equal(expectedFrequency, actualFrequency, 3);
        }
    }
}
