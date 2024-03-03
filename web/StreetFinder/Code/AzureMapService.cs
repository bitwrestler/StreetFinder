using Microsoft.AspNetCore.Http.Extensions;
using System.Collections.Immutable;

namespace StreetFinder.Code
{
    public class AzureMapServiceClient : IMapService
    {
        private readonly HttpClient _httpClient;

        public AzureMapServiceClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        private const string AZURE_MAP_BASEURL = "https://atlas.microsoft.com/search/address/json";
        private static readonly KeyValuePair<string, string>[] qparms = new KeyValuePair<string, string>[]{
                new("api-version", "1.0"),
                new("language", "en-US"),
                new("subscription-key", System.Environment.GetEnvironmentVariable("AZURE_MAP_ACCOUNT_KEY") ?? string.Empty)
            };

        

        public async Task<string[]> GetLatAndLongAsync(StreetRecord record)
        {
            var query = new QueryBuilder(qparms.Concat(
                 [
                     new("query",record.Name + " " + record.ZipCodeRange[0])
                 ]
                ));
            var urimaker = new UriBuilder(AZURE_MAP_BASEURL);
            urimaker.Query = query.ToQueryString().ToUriComponent();
            var addressObj = await _httpClient.GetFromJsonAsync<AzureAddressStruct>(urimaker.Uri);
            if(addressObj is null)
            {
                return new string[0];
            } else
            {
                return addressObj.results.OrderByDescending(o => o.score).Select(sl => sl.position.ToString()).ToArray();
            }
        }
    }
}
