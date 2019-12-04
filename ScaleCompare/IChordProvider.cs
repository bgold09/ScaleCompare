using System.Collections.Generic;

namespace ScaleCompare
{
    public interface IChordProvider
    {
        double[] GetMajorChord(int rootNote);
    }
}
