using System;
using System.Threading.Tasks;
using Info.Blockchain.Api.Data;

namespace Info.Blockchain.Api.Client
{
	public interface IHttpClient : IDisposable
	{
		string _apiCode { get; set; }
		Task<T> GetAsync<T>(string route, QueryString queryString = null, Func<string, T> customDeserialization = null);
		Task<TResponse> PostAsync<TPost, TResponse>(string route, TPost postObject, Func<string, TResponse> customDeserialization = null, bool multiPartContent = false);
	}
}
