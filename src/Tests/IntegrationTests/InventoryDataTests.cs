using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Data;
using Xunit;

namespace Info.Blockchain.API.Tests.IntegrationTests
{
	public class InventoryDataTests
	{
		[Fact]
		public async void GetInventoryData_ByHash_Valid()
		{
			using (ApiHelper apiHelper = new ApiHelper())
			{
				// have to get the latest block, hashes only are temporary
				LatestBlock latestBlock = await apiHelper._blockExplorer.GetLatestBlockAsync();
				InventoryData data = await apiHelper._blockExplorer.GetInventoryDataAsync(latestBlock.Hash);
				Assert.NotNull(data);

				Assert.Equal(latestBlock.Hash, data.Hash);
			}
		}
	}
}
