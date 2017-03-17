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

		[Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void CreateWallet_NullPassword_ArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper("APICODE"))
				{
					await apiHelper._walletCreator.Create(null);
				}
			});
		}

        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void CreateWallet_NullApiCode_ArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._walletCreator.Create("password");
				}
			});
		}


        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void CreateWallet_MockRequest_Valid()
		{
			using (ApiHelper apiHelper = new ApiHelper(baseHttpClient: new FakeWalletHttpClient()))
			{
				CreateWalletResponse walletResponse = await apiHelper._walletCreator.Create("Password");
				Assert.NotNull(walletResponse);

				Assert.Equal(walletResponse.Address, "12AaMuRnzw6vW6s2KPRAGeX53meTf8JbZS");
				Assert.Equal(walletResponse.Identifier, "4b8cd8e9-9480-44cc-b7f2-527e98ee3287");
				Assert.Equal(walletResponse.Label, "My Blockchain Wallet");
			}
		}
	}
}