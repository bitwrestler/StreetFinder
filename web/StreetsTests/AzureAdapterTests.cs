using ers_config;
using StreetFinder.Code;

namespace StreetsTests
{
    [TestClass]
    public class AzureAdapterTests
    {
        [TestInitialize]
        public void setup()
        {
            DebugConfig.ProcessDotEnvFile(DebugConfig.FindAzureConfigInParents(".env"));
        }


        [TestMethod]
        public void Init_HappyPath()
        {
            var adapter = new AzureDataAdapter();
            var data = adapter.StreetData;
            Assert.IsNotNull(data);
            Assert.IsTrue(data.StreetData.Count() > 1);
            Assert.IsTrue(data.UpdateDate > DateTime.MinValue);
        }
    }
}