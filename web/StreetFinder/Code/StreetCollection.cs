using System.Collections.Immutable;

namespace StreetFinder.Code
{
    [Serializable]
    public class StreetCollection
    {
        public DateTime UpdateDate { get; set; }
        public ImmutableArray<StreetRecord> StreetRecords { get; set; }
    }
}
