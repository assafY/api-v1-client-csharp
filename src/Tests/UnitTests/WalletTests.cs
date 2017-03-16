using System;
using System.Collections.Generic;
using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Data;
using Xunit;

namespace Info.Blockchain.Api.Tests.UnitTests
{
	public class WalletTests
	{

		private Wallet.Wallet GetWallet(ApiHelper apiHelper)
		{
			return apiHelper.CreateWallet("Test", "Test");
		}

        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void ArchiveAddress_NullAddress_ArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					Wallet.Wallet wallet = GetWallet(apiHelper);
					await wallet.ArchiveAddress(null);
				}
			});
		}

        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void GetAddress_BadParameters_ArgumentExceptions()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					Wallet.Wallet wallet = this.GetWallet(apiHelper);
					await wallet.GetAddressAsync(null);
				}
			});

			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					Wallet.Wallet wallet = this.GetWallet(apiHelper);
					await wallet.GetAddressAsync("Test", -1);
				}
			});
		}

        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void ListAddresses_NegativeConfirmations_ArgumentOutOfRangeException()
		{
			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					Wallet.Wallet wallet = this.GetWallet(apiHelper);
					await wallet.ListAddressesAsync(-1);
				}
			});
		}

        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void Send_BadParameters_ArgumentExceptions()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					Wallet.Wallet wallet = this.GetWallet(apiHelper);
					await wallet.SendAsync(null, BitcoinValue.Zero);
				}
			});

			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					Wallet.Wallet wallet = this.GetWallet(apiHelper);
					await wallet.SendAsync("Test", BitcoinValue.FromBtc(-1));
				}
			});
		}

        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void SendMany_NullReeipients_ArgumentNUllException()
		{
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					Wallet.Wallet wallet = this.GetWallet(apiHelper);
					await wallet.SendManyAsync(null);
				}
			});
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					Wallet.Wallet wallet = this.GetWallet(apiHelper);
					await wallet.SendManyAsync(new Dictionary<string, BitcoinValue>());
				}
			});
		}

        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void Unarchive_NullAddress_ArgumentNulException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					Wallet.Wallet wallet = this.GetWallet(apiHelper);
					await wallet.UnarchiveAddress(null);
				}
			});
		}


	}
}