using System.Collections.ObjectModel;
using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Data;
using Xunit;

namespace Info.Blockchain.API.Tests.IntegrationTests
{
	public class UnspentOutputTests
	{
		[Fact]
		public async void GetUnspent_ByAdress_Valid()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				const string address = "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa";
				ReadOnlyCollection<UnspentOutput> outputs = await apiHelper._blockExplorer.GetUnspentOutputsAsync(address);
				Assert.NotNull(outputs);
			}
		}
	}
}
