using Microsoft.AspNetCore.Mvc;
using StreetFinder.Code;

namespace StreetFinder.Controllers
{
    [Produces("application/json")]
    [Route("api/StreetRecord")]
    public class SearchController : Controller
    {
        public const int MAX_SEARCH_RESULTS = 50;
        public const int MIN_SEARCH_PATTERN = 3;

        private readonly IDataAdapter _adapter;

        public SearchController(IDataAdapter dataAdapter)
        { 
            _adapter = dataAdapter;
        }

        [HttpGet("Search", Name = "Search")]
        public IEnumerable<StreetRecord> Search(string pattern, string searchType)
        {
            if (!Enum.TryParse<SearchOptions>(searchType, false, out var searchOption))
                searchOption = SearchOptions.Contains;

            if(pattern.Length < MIN_SEARCH_PATTERN)
                return Enumerable.Empty<StreetRecord>();

            return _adapter.StreetData.Search(pattern, searchOption).Take(MAX_SEARCH_RESULTS);
        }
    }
}
