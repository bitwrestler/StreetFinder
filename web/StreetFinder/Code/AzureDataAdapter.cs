using Azure.Storage.Blobs;
using System.Text.Json;

namespace StreetFinder.Code
{
    public class AzureDataAdapter : IDataAdapter
    {
        public const string SETTING_KEY = "AZURE_STORAGE_CONNECTION_STRING";
        public const string CONTAINER_NAME = "streetsdata";
        public const string BLOB_NAME = "nova_data.json";

        private readonly JsonDocument jsondata;
        private StreetCollection? streetCollection=null;

        public AzureDataAdapter()
        {
            var svc = new BlobContainerClient(System.Environment.GetEnvironmentVariable(SETTING_KEY), CONTAINER_NAME);
            var blobClient = svc.GetBlobClient(BLOB_NAME);
            string allBlob=string.Empty;
            using (var strm = blobClient.OpenRead(new Azure.Storage.Blobs.Models.BlobOpenReadOptions(false)))
            {
                this.jsondata = JsonDocument.Parse(strm);
            }
        }

        public StreetCollection StreetData { 
            get
            {
                lock (jsondata)
                {
                    if (streetCollection is null)
                        streetCollection = JsonDocToCollection(jsondata);
                }
                return streetCollection;
            } 
        }

        internal static StreetCollection JsonDocToCollection(JsonDocument doc)
        {

            DateTime? update_date=null;
            IEnumerable<StreetRecord> records = Enumerable.Empty<StreetRecord>();

            var root = doc.RootElement;
            foreach(var props in root.EnumerateObject())
            {
                switch(props.Name)
                {
                    case "update_date":
                        update_date = props.Value.GetDateTime();
                        break;
                    case "data":
                        records = JsonDataToStreetRecords(props.Value);
                        break;
                }
            }

            if (!update_date.HasValue)
                throw new InvalidOperationException("JSON data corrupt");

            return new StreetCollection(update_date.Value, records);
        }

        private static IEnumerable<StreetRecord> JsonDataToStreetRecords(JsonElement arrayEle)
        {
            var l = new List<StreetRecord>();

            foreach(var arec in arrayEle.EnumerateArray()) 
            {
                string? name = null;
                int[]? zr = null; 
                foreach(var aprop in arec.EnumerateObject())
                {
                    switch (aprop.Name)
                    {
                        case "name":
                            name = aprop.Value.GetString();
                            break;
                        case "zipcode_range":
                            zr = aprop.Value.EnumerateArray().Select(s => s.GetInt32()).ToArray();
                            break;
                    }
                }
                if(name is null || zr is null)
                    throw new InvalidOperationException("JSON data corrupt");
               yield return new StreetRecord(name, CompressZipCodeRange(zr));
            }
        }

        /// <summary>
        /// compress the zipcode range to a single elelment if theya re both the same
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        private static int[] CompressZipCodeRange(int[] range)
        {
            if(range.Length>0)
            {
                int v = range[0];
                if(range.All(a=>a == v))
                    return new int[] { v };
            }
            return range;
        }
    }
}
