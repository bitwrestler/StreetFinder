using System.Text.Json;
namespace StreetFinder.Code
{
    public class LocalResourceDataAdpater : IDataAdapter
    {
        public LocalResourceDataAdpater()
        {
            StreetData = StreetDataConverter.JsonDocToCollection(JsonDocument.Parse(Resource.streets));
        }

        public StreetCollection StreetData { get; init; }
    }
}
