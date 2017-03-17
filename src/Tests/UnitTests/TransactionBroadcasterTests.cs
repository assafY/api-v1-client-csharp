using System;
using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Tests.UnitTests;
using Xunit;

namespace Info.Blockchain.API.Tests.UnitTests
{
	public class TransactionBroadcasterTests
	{
		[Fact]
		public async void GetTransaction_BadIds_ArgumentExecption()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._transactionBroadcaster.PushTransactionAsync(null);
				}
			});
		}
	}
}
