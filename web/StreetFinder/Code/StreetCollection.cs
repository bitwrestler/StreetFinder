using System.Collections.Generic;
using System.Collections.Immutable;


namespace StreetFinder.Code
{
    public class StreetCollection
    {
        public struct SearchStruct
        {
            public readonly StreetRecord Street { get; init; }
            public readonly HashSet<string> PhoneticValue { get; init; }
        }
        
        private readonly ImmutableArray<SearchStruct> _structures;

        public StreetCollection(DateTime udpdt, IEnumerable<StreetRecord> data) 
        {
            UpdateDate = udpdt;
            var phoneticHandler = new SoundsLikeHandler();
            _structures = data.Select(s=>new SearchStruct { 
                Street = s, 
                PhoneticValue = phoneticHandler.PhoneticTokens(s.ShortName).ToHashSet() 
            }).ToImmutableArray();
        }

        public DateTime UpdateDate { get; private set;}

        internal IEnumerable<StreetRecord> StreetData => _structures.Select(s => s.Street);

        internal SearchStruct FindSearchRecordByID(int id)
        {
            return this._structures.Single(sl => sl.Street.ID == id);
        }
            

        public IEnumerable<StreetRecord> Search(string pattern, SearchOptions options)
        {
            var handler = new SearchHandler(pattern, options);
            return _structures.Where(handler.Search).Select(s=>s.Street);
        }

        //TODO async searching
    }
}
