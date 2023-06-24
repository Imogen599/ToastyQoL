using System;
using System.Collections.Generic;

namespace ToastyQoL.Core.Systems.MNLSystems.Sets
{
    public class MNLSet
    {
        public readonly Dictionary<int, int> AssosiatedIDsAndFightLength;

        public readonly Func<float> Weight;

        public MNLSet(Dictionary<int, int> assosiatedIDsAndFightLength, Func<float> weight)
        {
            AssosiatedIDsAndFightLength = assosiatedIDsAndFightLength;
            Weight = weight;
        }
    }
}
