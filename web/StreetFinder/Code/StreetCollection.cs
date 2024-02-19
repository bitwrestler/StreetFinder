using System.Collections.Immutable;

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

        public IEnumerable<StreetRecord> Search(string pattern)
        {
            string pat = pattern.ToUpper();
            return StreetData.Where(w=>w.Name.Contains(pat));
        }

        public async IAsyncEnumerable<StreetRecord> SearchAsync(string pattern)
        {
            string pat = pattern.ToUpper();
           await foreach (
                var r in  StreetData.ToAsyncEnumerable()
                .WhereAwait( (w) => ValueTask.FromResult(w.Name.Contains(pat)) )
                )
                yield return r;
        }
    }
}
