using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Data;
using Xunit;

namespace Info.Blockchain.API.Tests.IntegrationTests
{
	public class AddressTests
	{
		[Fact]
		public async void GetAddress_ByAddress_Valid()
		{
			using (ApiHelper apiHelper = new ApiHelper())
			{
				const string addressString = "13k5KUK2vswXRdjgjxgCorGoY2EFGMFTnu";
				Address address = await apiHelper._blockExplorer.GetAddressAsync(addressString, null);
				Assert.NotNull(address);
				Assert.Equal(address.AddressStr, addressString);
			}
		}

		[Fact]
		public async void GetAddress_ByHash_Valid()
		{
			using (ApiHelper apiHelper = new ApiHelper())
			{
				const string hash = "1e15be27e4763513af36364674eebdba5a047323";
				Address address = await apiHelper._blockExplorer.GetAddressAsync(hash, null);
				Assert.NotNull(address);
				Assert.Equal(address.Hash160, hash);
			}
		}

		[Theory]
		[InlineData(100)]
		[InlineData(101)]
		[InlineData(76)]
		[InlineData(0)]
		[InlineData(3)]
		public async void GetAddress_LimitTransactions_Valid(int transactionCount)
		{
			using (ApiHelper apiHelper = new ApiHelper())
			{
				const string hash = "1e15be27e4763513af36364674eebdba5a047323";
				Address address = await apiHelper._blockExplorer.GetAddressAsync(hash, transactionCount);
				Assert.NotNull(address);
				Assert.Equal(address.Transactions.Count, transactionCount);
			}
		}
	}
}
