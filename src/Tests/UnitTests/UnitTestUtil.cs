using Info.Blockchain.Api.Client;

namespace Info.Blockchain.Api.Tests.UnitTests
{
	internal static class UnitTestUtil
	{
		internal static BlockchainApiHelper GetFakeHelper(string apiCode = null)
		{
			return new BlockchainApiHelper(apiCode, new FakeHttpClient(), apiCode, new FakeHttpClient());
		}
	}
}