using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Tests;
using Xunit;

namespace Info.Blockchain.API.Tests.UnitTests
{
	public class BlockchainApiHelperTests
	{
		[Fact]
		public void CreateHelper_Valid()
		{
			const string apiCode = "5";
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper(apiCode, new FakeHttpClient()))
			{
				Assert.NotNull(apiHelper);
				Assert.NotNull(apiHelper._statisticsExplorer);
				Assert.NotNull(apiHelper._blockExplorer);
				Assert.NotNull(apiHelper._exchangeRateExplorer);
				Assert.NotNull(apiHelper._transactionBroadcaster);
				Assert.Null(apiHelper._walletCreator);
			}
		}

        [Fact]
        public void CreateHelperWithService_Valid()
        {
            const string apiCode = "5";
            using (BlockchainApiHelper apiHelper = new BlockchainApiHelper(apiCode, new FakeHttpClient(), "http://localhost:3000"))
            {
                Assert.NotNull(apiHelper);
                Assert.NotNull(apiHelper._statisticsExplorer);
                Assert.NotNull(apiHelper._blockExplorer);
                Assert.NotNull(apiHelper._exchangeRateExplorer);
                Assert.NotNull(apiHelper._transactionBroadcaster);
                Assert.NotNull(apiHelper._walletCreator);
            }
        }
    }
}
