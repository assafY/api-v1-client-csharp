using System;
using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Tests.UnitTests;
using Xunit;

namespace Info.Blockchain.API.Tests.UnitTests
{
	public class InventoryDataTests
	{
		[Fact]
		public async void GetInventoryData_NullHash_ArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._blockExplorer.GetInventoryDataAsync(null);
				}
			});
		} 
	}
}