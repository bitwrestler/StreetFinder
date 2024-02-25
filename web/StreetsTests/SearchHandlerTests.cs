﻿using StreetFinder.Code;
using StreetFinder.Code.PhoneticAlgorithms;
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
        [DataRow("braddock", "bradok")]
        public void Search_Phonetic_ExpectedResults(string realdata, string searchterm)
        {
            var h = PhoneticHandlerFactory.GetHandlerForPattern(realdata);
            Assert.IsTrue(h.CompareTo(searchterm));
        }

        [DataTestMethod]
        [DataRow("braddock", "bradok")]
        public void Search_DoubleMetaphonePhonetic_ExpectedResults(string realdata, string searchterm)
        {
            var searcher = new DoubleMetaphoneSearcher(realdata);
            Assert.IsTrue(searcher.CompareTo(searchterm));
        }
    }
}
