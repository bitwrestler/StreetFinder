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

        [HttpGet(nameof(Search))]
        public IEnumerable<StreetRecord> Search(string pattern, string searchType)
        {
            if (!Enum.TryParse<SearchOptions>(searchType, false, out var searchOption))
                searchOption = SearchOptions.Contains;

            if(pattern.Length < MIN_SEARCH_PATTERN)
                return Enumerable.Empty<StreetRecord>();

            return _adapter.StreetData.Search(pattern, searchOption).Take(MAX_SEARCH_RESULTS);
        }

        [HttpGet(nameof(Coordinates))]
        public async Task<string[]> Coordinates([FromServices] IMapService restClient, [FromQuery] int id)
        {
            var ss = _adapter.StreetData.GetByID(id);
            if(ss.LatLong is null)
            {
                var latlon =  await restClient.GetLatAndLongAsync(ss.Street);
                if(latlon.Any())
                    ss.LatLong = latlon;
                return latlon;
            } else
            {
                return await Task.FromResult(ss.LatLong);
            }
        }
    }
}
