using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;


namespace StreetFinder.Code
{
    public class StreetCollection
    {
        public class SearchStruct
        {
            public SearchStruct(StreetRecord street, IPhoneticHandler phHandler)
            {
                Street = street;
                PhoneticHandler = phHandler;
            }

            public StreetRecord Street { get; init; }
            public IPhoneticHandler PhoneticHandler { get; init; }
            public string[]? LatLong { get; set; } = null;
        }

        private readonly ImmutableArray<SearchStruct> _structures;

        public StreetCollection(DateTime udpdt, IEnumerable<StreetRecord> data) : this(udpdt, data, new PhoneticHandlerFactory())
        {
        }

        public StreetCollection(DateTime udpdt, IEnumerable<StreetRecord> data, PhoneticHandlerFactory phoneticFactory) 
        {
            UpdateDate = udpdt;
            _structures = data.Select(s=>new SearchStruct(s, phoneticFactory.GetHandlerForPattern(s.ShortName)) { 
            }).ToImmutableArray();
        }

        public DateTime UpdateDate { get; private set;}

        internal IEnumerable<StreetRecord> StreetData => _structures.Select(s => s.Street);
            
        public IEnumerable<StreetRecord> Search(string pattern, SearchOptions options)
        {
            var handler = new SearchHandler(pattern, options);
            return _structures.Where(handler.Search).Select(s=>s.Street);
        }

        public SearchStruct GetByID(int id)
        {
            var ss = _structures.SingleOrDefault(sng => sng.Street.ID == id);
            if(ss is null)
                throw new IndexOutOfRangeException(nameof(id));
            return ss;
        }
            

        //TODO async searching
    }
}
