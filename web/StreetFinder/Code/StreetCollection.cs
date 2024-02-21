﻿using System.Collections.Immutable;

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

        public IEnumerable<StreetRecord> Search(string pattern, SearchOptions options)
        {
            var handler = new SearchHandler(pattern, options);
            return StreetData.Where(handler.Search);
        }

        //TODO async searching
    }
}
