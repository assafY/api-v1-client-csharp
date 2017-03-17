using System;
using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Tests.UnitTests;
using Xunit;

namespace Info.Blockchain.API.Tests.UnitTests
{
	public class CurrencyTests
	{
		[Fact]
		public async void ToBtc_NullCurrency_ArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._exchangeRateExplorer.ToBtcAsync(null, 1);
				}
			});
		}

		[Fact]
		public async void ToBtc_NegativeValue_ArgumentOutOfRangeException()
		{
			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._exchangeRateExplorer.ToBtcAsync("USD", -1);
				}
			});
		}
	}
}
