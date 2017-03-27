using System;
using Xunit;
using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Tests.UnitTests;

namespace Info.Blockchain.API.Tests.UnitTests
{
	public class TransactionTests
	{
		[Fact]
		public async void GetTransaction_BadIds_ArgumentExecption()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._blockExplorer.GetTransactionAsync(null);
				}
			});

			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._blockExplorer.GetTransactionByIndexAsync(-1);
				}
			});
		}
	}
}
