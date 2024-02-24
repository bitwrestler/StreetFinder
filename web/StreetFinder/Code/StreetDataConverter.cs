using System.Text.Json;

namespace StreetFinder.Code
{
    public class StreetDataConverter
    {
        public static StreetCollection JsonDocToCollection(JsonDocument doc)
        {

            DateTime? update_date = null;
            IEnumerable<StreetRecord> records = Enumerable.Empty<StreetRecord>();

            var root = doc.RootElement;
            foreach (var props in root.EnumerateObject())
            {
                switch (props.Name)
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
            int id = 0;
            foreach (var arec in arrayEle.EnumerateArray())
            {
                id++;
                string? name = null;
                string? short_name = null;
                int[]? zr = null;
                foreach (var aprop in arec.EnumerateObject())
                {
                    switch (aprop.Name)
                    {
                        case "name":
                            name = aprop.Value.GetString();
                            break;
                        case "zipcode_range":
                            zr = aprop.Value.EnumerateArray().Select(s => s.GetInt32()).ToArray();
                            break;
                        case "short_name":
                            short_name = aprop.Value.GetString();
                            break;
                    }
                }
                if (name is null || zr is null || short_name is null)
                    throw new InvalidOperationException("JSON data corrupt");
                yield return new StreetRecord(id, name, CompressZipCodeRange(zr), short_name);
            }
        }

        /// <summary>
        /// compress the zipcode range to a single elelment if theya re both the same
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        private static int[] CompressZipCodeRange(int[] range)
        {
            if (range.Length > 0)
            {
                int v = range[0];
                if (range.All(a => a == v))
                    return new int[] { v };
            }
            return range;
        }
    }
}
