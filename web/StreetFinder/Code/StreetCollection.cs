using System.Collections.Immutable;
using System.Runtime.Serialization;

namespace StreetFinder.Code
{
    public class StreetCollection
    {
        public StreetCollection(DateTime udpdt, IEnumerable<StreetRecord> data) 
        {
            UpdateDate = udpdt;
            StreetData = data.ToImmutableArray();
        }

        public DateTime UpdateDate { get; private set;}

        public ImmutableArray<StreetRecord> StreetData { get; private set; }
    }
}
