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
                        streetCollection = StreetDataConverter.JsonDocToCollection(jsondata);
                }
                return streetCollection;
            } 
        }
    }
}
