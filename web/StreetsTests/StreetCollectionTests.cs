using StreetFinder.Code;
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
            var ret = coll.Search(pattern,SearchOptions.Contains);
            Assert.AreEqual(expectedCount, ret.Count());
        }

        [DataTestMethod]
        [DataRow("BRUNSWICK FOR*PASS", 1)]
        [DataRow("BRUNSWICK FOR*", 1)]
        [DataRow("*SWICK FOREST PASS", 1)]
        public void Search_WildCard_ExpectedResults(string pattern, int expectedCount)
        {
            var coll = TestUtils.GetTestStreetCollection();
            var ret = coll.Search(pattern, SearchOptions.Contains);
            Assert.AreEqual(expectedCount, ret.Count());
        }

        [DataTestMethod]
        [DataRow("BRUNSWICK F", 1)]
        [DataRow("BRUNSWICK", 2)]
        [DataRow("BRUNSWIC*", 2)]
        public void Search_StartsWith_ExpectedResults(string pattern, int expectedCount)
        {
            var coll = TestUtils.GetTestStreetCollection();
            var ret = coll.Search(pattern, SearchOptions.StartsWith);
            Assert.AreEqual(expectedCount, ret.Count());
        }

        [DataTestMethod]
        [DataRow("FOREST PATH", 1)]
        [DataRow("FORE* PATH", 1)]
        public void Search_EndsWith_ExpectedResults(string pattern, int expectedCount)
        {
            var coll = TestUtils.GetTestStreetCollection();
            var ret = coll.Search(pattern, SearchOptions.EndsWith);
            Assert.AreEqual(expectedCount, ret.Count());
        }
    }
}
