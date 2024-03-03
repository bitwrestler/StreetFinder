﻿using Microsoft.AspNetCore.Http.Extensions;
using System.Collections.Immutable;

namespace StreetFinder.Code
{
    public interface IAzureMapSericeClient
    {
        Task<string> GetLatAndLongAsync(StreetRecord record);
    }
    public class AzureMapServiceClient : IAzureMapSericeClient
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

        

        public async Task<string> GetLatAndLongAsync(StreetRecord record)
        {
            var query = new QueryBuilder(qparms.Concat(
                 [
                     new("query",record.Name + " " + record.ZipCodeRange[0])
                 ]
                ));
            var urimaker = new UriBuilder(AZURE_MAP_BASEURL);
            urimaker.Query = query.ToQueryString().ToUriComponent();
            var addressObj = await _httpClient.GetFromJsonAsync<AzureAddressStruct>(urimaker.Uri);
            //TODO handle multi or no results better
            if(addressObj is not null && addressObj.summary.totalResults > 0)
            {
                var uniquePositions = addressObj.results.Select(sl => sl.position).ToImmutableHashSet();
                if(uniquePositions.Count == 1)
                {
                    return addressObj.results[0].position.ToString();
                } else
                {
                    return string.Empty;
                }

            } else
            {
                return string.Empty;
            }
        }
    }
}
