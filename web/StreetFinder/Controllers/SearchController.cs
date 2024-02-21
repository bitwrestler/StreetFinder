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

        private readonly AzureDataAdapter _adapter;

        public SearchController(AzureDataAdapter dataAdapter)
        { 
            _adapter = dataAdapter;
        }

        [HttpGet("Search", Name = "Search")]
        public IEnumerable<StreetRecord> Search(string pattern)
        {
            if(pattern.Length < MIN_SEARCH_PATTERN)
                return Enumerable.Empty<StreetRecord>();

            return _adapter.StreetData.Search(pattern, SearchOptions.Contains).Take(MAX_SEARCH_RESULTS);
        }
    }
}
