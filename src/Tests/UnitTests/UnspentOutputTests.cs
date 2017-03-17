using System;
using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Tests.UnitTests;
using Xunit;

namespace Info.Blockchain.API.Tests.UnitTests
{
	public class UnspentOutputTests
	{
		[Fact]
		public async void GetUnspentOutputs_NullAddress_ArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (ApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper._blockExpolorer.GetUnspentOutputsAsync(null);
				}
			});
		}
	}
}