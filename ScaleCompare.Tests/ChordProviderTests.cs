using Moq;
using Xunit;

namespace ScaleCompare.Tests
{
    public class ChordProviderTests
    {
        [Fact]
        public void GetMajorChord()
        {
            var rootNote = 49;
            var expectedNotes = new[] { 49, 53, 56 };
            var temperamentMock = new Mock<ITemperament>(MockBehavior.Strict);
            foreach (var note in expectedNotes)
            {
                temperamentMock.Setup(m => m.GetPitch(note)).Returns(note);
            }
            
            var chordProvider = new ChordProvider(temperamentMock.Object);

            double[] actualFrequencies = chordProvider.GetMajorChord(rootNote);

            Assert.Equal(expectedNotes.Length, actualFrequencies.Length);
            temperamentMock.Verify(m => m.GetPitch(It.IsIn(expectedNotes)), Times.Exactly(expectedNotes.Length));
            foreach (var note in expectedNotes)
            {
                temperamentMock.Verify(m => m.GetPitch(note), Times.Once());
            }
        }
    }
}
