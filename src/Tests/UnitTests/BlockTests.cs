using System;
using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Tests.UnitTests;
using Xunit;

namespace Info.Blockchain.API.Tests.UnitTests
{
	public class BlockTests
	{
		[Fact]
		public async void GetBlock_BadIds_ArgumentException()
		{
			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._blockExplorer.GetBlockAsync(-1);
				}
			});


			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._blockExplorer.GetBlockAsync(null);
				}
			});
		}

		[Fact]
		public async void GetBlocks_BadParameters_ArgumentException()
		{
			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._blockExplorer.GetBlocksAsync(-1);
				}
			});


			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._blockExplorer.GetBlocksAsync(1000);
				}
			});

			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._blockExplorer.GetBlocksAsync(int.MaxValue);
				}
			});


			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._blockExplorer.GetBlocksAsync(DateTime.MinValue);
				}	
			});

			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._blockExplorer.GetBlocksAsync(DateTime.MaxValue);
				}
			});
		}

		[Fact]
		public async void GetBlocksByHeight_BadParameters_ArgumentException()
		{
			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._blockExplorer.GetBlocksAtHeightAsync(-1);
				}
			});
		}
	}
}
