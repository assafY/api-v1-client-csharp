using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Data;
using Xunit;

namespace Info.Blockchain.API.Tests.IntegrationTests
{
	public class StatisticsTests
	{
		[Fact]
		public async void GetStatistics_Valid()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				StatisticsResponse statisticsResponse = await apiHelper._statisticsExplorer.GetAsync();
				Assert.NotNull(statisticsResponse);
			}
		}
	}
}
