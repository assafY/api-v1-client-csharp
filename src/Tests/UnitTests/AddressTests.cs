using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Info.Blockchain.API.Client;
using Info.Blockchain.API.Tests.UnitTests;
using Xunit;

namespace Info.Blockchain.API.Tests.UnitTests
{
	public class AddressTests
	{
		[Fact]
		public async void GetAddress_NullHash_ArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper.blockExplorer.GetAddressAsync("", null);
				}
			});
		}

		[Fact]
		public async void GetAddress_NegativeTransactions_ArgumentOutOfRangeException()
		{
			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper.blockExplorer.GetAddressAsync("test", -1);
				}
			});
		}
	}
}
