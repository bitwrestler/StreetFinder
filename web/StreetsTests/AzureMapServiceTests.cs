using ers_config;
using StreetFinder.Code;
using System;

namespace StreetsTests
{
    [TestClass]
    public class AzureMapServiceTests
    {
        [TestInitialize]
        public void setup()
        {
            DebugConfig.ProcessDotEnvFile(DebugConfig.FindAzureConfigInParents(".env"));
        }

        [TestMethod]
        public async Task GetLatAndLongAsync_ExpectStruct()
        {
            var srecord = new StreetRecord(1, "FLOYD AVE", [22150], "FLOYD");

            using (var client = new HttpClient())
            {
                var svc = new AzureMapServiceClient(client);
                string[] latlon = await svc.GetLatAndLongAsync(srecord);
                Assert.IsNotNull(latlon);
                Assert.IsTrue(latlon.Length > 0);
                Assert.IsTrue(latlon[0].Contains("+"));
            }
        }
    }
}
