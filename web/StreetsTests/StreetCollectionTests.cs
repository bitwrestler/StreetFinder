using System;
using System.Collections.Generic;

namespace StreetsTests
{
    [TestClass]
    public class StreetCollectionTests
    {
        [TestMethod]
        public void GetTestStreetCollection_HappyPath()
        {
            var coll = TestUtils.GetTestStreetCollection();
            Assert.IsTrue(coll.StreetData.Any());
        }

        [DataTestMethod]
        [DataRow("avon", 5)]
        [DataRow("bogus", 0)]
        [DataRow("BRUNSWICK FOREST PASS", 1)]
        public void Search_ExpectedResults(string pattern, int expectedCount)
        {
            var coll = TestUtils.GetTestStreetCollection();
            var ret = coll.Search(pattern);
            Assert.AreEqual(expectedCount, ret.Count());
        }
    }
}
