using StreetFinder.Code;
using System;

namespace StreetsTests
{
    [TestClass]
    public class SoundsLikeHandlerTests
    {
        [TestMethod]
        public void ConvertAllData()
        {
            var handler = new SoundsLikeHandler();
            var l = new List<string[]>();
            var coll = TestUtils.GetTestStreetCollection();
            foreach (var item in coll.StreetData)
            {
                l.Add(handler.PhoneticTokens(item.ShortName).ToArray());
            }
        }
    }
}
