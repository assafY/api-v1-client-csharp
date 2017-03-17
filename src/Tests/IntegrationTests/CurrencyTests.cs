using System.Collections.Generic;
using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Data;
using Xunit;

namespace Info.Blockchain.API.Tests.IntegrationTests
{
	public class CurrencyTests
	{
		[Fact]
		public async void GetTicker_Valid()
		{
			using (ApiHelper apiHelper = new ApiHelper())
			{
				Dictionary<string, Currency> currencies = await apiHelper._exchangeRateExplorer.GetTickerAsync();
				Assert.NotNull(currencies);
				Assert.True(currencies.Count > 0);
			}
		}

		[Fact]
		public async void ToBtc_FromUs_HasValue()
		{
			using (ApiHelper apiHelper = new ApiHelper())
			{
				double btcValue = await apiHelper._exchangeRateExplorer.ToBtcAsync("USD", 1000);
				Assert.True(btcValue > 0);
			}
		}
	}
}
