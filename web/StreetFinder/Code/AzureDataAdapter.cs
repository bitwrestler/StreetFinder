using Azure.Storage.Blobs;

namespace StreetFinder.Code
{
    public class AzureDataAdapter
    {
        public const string SETTING_KEY = "AZURE_STORAGE_CONNECTION_STRING";
        public const string CONTAINER_NAME = "streetsdata";
        public const string BLOB_NAME = "nove_data.json";

        public StreetCollection StreetData { get; private set;  }
        
        public void Init()
        {
            var svc = new BlobContainerClient(System.Environment.GetEnvironmentVariable(SETTING_KEY), CONTAINER_NAME);
            var blobClient = svc.GetBlobClient(BLOB_NAME);
            string allBlob=string.Empty;

            using (var strm = blobClient.OpenRead(new Azure.Storage.Blobs.Models.BlobOpenReadOptions(false)))
            {
                var data = System.Text.Json.JsonSerializer.Deserialize<StreetCollection>(strm);

                if (data is null)
                    throw new Exception("Can not read blob");

                this.StreetData = data;
            }
        }
    }
}
