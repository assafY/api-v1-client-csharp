using System;
using System.Threading.Tasks;
using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Data;

namespace Info.Blockchain.Api.Tests
{
	public class FakeHttpClient : IHttpClient
	{

		public string _apiCode { get; set; }

		public Task<T> GetAsync<T>(string route, QueryString queryString = null, Func<string, T> customDeserialization = null)
		{
			return Task.FromResult(default(T));
		}

		public Task<TResponse> PostAsync<TPost, TResponse>(string route, TPost postObject, Func<string, TResponse> customDeserialization = null, bool multiPartContent = false)
		{
			return Task.FromResult(default(TResponse));
		}

        void IDisposable.Dispose()
        {
        }
    }
}
