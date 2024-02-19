using Microsoft.AspNetCore.Mvc;
using StreetFinder.Code;

namespace StreetFinder.Controllers
{
    [Produces("application/json")]
    [Route("api/StreetRecord")]
    public class SearchController : Controller
    {
        public const int MIN_SEARCH_PATTERN = 3;

        [HttpGet("{pattern}", Name = "Search")]
        public IEnumerable<StreetRecord> Search(string pattern)
        {
            if(pattern.Length < MIN_SEARCH_PATTERN)
                return Enumerable.Empty<StreetRecord>();

            return AzureDataAdapter.Instance.StreetData.Search(pattern);
        }
    }
}
