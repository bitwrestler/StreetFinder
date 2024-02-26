using StreetFinder.Code;
using StreetFinder.Code.PhoneticAlgorithms;
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
        public enum PhoneticType
        {
            CodeProjectSoundex,
            DoubleMatphone
        }


        public static StreetCollection GetTestStreetCollection(PhoneticType phtype = PhoneticType.DoubleMatphone)
        {
            PhoneticHandlerFactory? phFactory = null;
            switch(phtype)
            {
                case PhoneticType.CodeProjectSoundex:
                    phFactory = new PhoneticHandlerFactory( (str) => new CodeProjectSoundexSearcher(str));
                    break;
                case PhoneticType.DoubleMatphone:
                    phFactory = new PhoneticHandlerFactory( (str) => new DoubleMetaphoneSearcher(str));
                    break;
            }

            return StreetDataConverter.JsonDocToCollection(JsonDocument.Parse(Resource1.streets), phFactory);          
        }
    }
}
