using Azure.Storage.Blobs;
using System.Text.Json;

namespace StreetFinder.Code
{
    public class AzureDataAdapter : IDataAdapter
    {
        public const string SETTING_KEY = "AZURE_STORAGE_CONNECTION_STRING";
        public const string CONTAINER_NAME = "streetsdata";
        public const string BLOB_NAME = "nova_data.json";

        private readonly StreetCollection streetCollection;

        public AzureDataAdapter()
        {
            var svc = new BlobContainerClient(System.Environment.GetEnvironmentVariable(SETTING_KEY), CONTAINER_NAME);
            var blobClient = svc.GetBlobClient(BLOB_NAME);
            string allBlob=string.Empty;
            JsonDocument jsondata;
            using (var strm = blobClient.OpenRead(new Azure.Storage.Blobs.Models.BlobOpenReadOptions(false)))
            {
                jsondata = JsonDocument.Parse(strm);
            }
            streetCollection = StreetDataConverter.JsonDocToCollection(jsondata);
        }

        public StreetCollection StreetData { 
            get
            {
                return streetCollection;
            } 
        }
    }
}
