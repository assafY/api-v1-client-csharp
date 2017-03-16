using Info.Blockchain.Api.Client;

namespace Info.Blockchain.Api.Tests.UnitTests
{
	internal static class UnitTestUtil
	{
		internal static ApiHelper GetFakeHelper(string apiCode = null)
		{
			return new ApiHelper(apiCode, new FakeHttpClient());
		}
	}
}