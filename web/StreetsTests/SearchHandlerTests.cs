using StreetFinder.Code;
using System;

namespace StreetsTests
{
    [TestClass]
    public class SearchHandlerTests
    {
        private static StreetCollection coll;

        [ClassInitialize]
        public static void onetimesetup(TestContext ctx)
        {
            coll = TestUtils.GetTestStreetCollection();
        }

        [DataTestMethod]
        [DataRow("bradok")]
        public void SoundsLike_ExpectedResults(string street)
        {
            var ph = new SoundsLikeHandler();
            var phonetic = ph.PhoneticTokens(street);

            var ret = coll.Search(street, SearchOptions.Phonetic).Select(s=>coll.FindSearchRecordByID(s.ID));
            Assert.IsTrue(ret.Any());

        }
    }
}
