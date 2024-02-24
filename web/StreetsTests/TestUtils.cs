using StreetFinder.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StreetsTests
{
    public class TestUtils
    {
        public static StreetCollection GetTestStreetCollection()
        {
            return StreetDataConverter.JsonDocToCollection(JsonDocument.Parse(Resource1.streets));          
        }
    }
}
