using Info.Blockchain.Api.Data;
using Info.Blockchain.Api.Client;
using System;
using System.Threading.Tasks;

namespace Info.Blockchain.Api.Tests
{
    public class FakeWalletHttpClient : IHttpClient
    {
        public void Dispose()
        {
        }

        public Task<T> GetAsync<T>(string route, QueryString queryString = null, Func<string, T> customDeserialization = null)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> PostAsync<TPost, TResponse>(string route, TPost postObject,
            Func<string, TResponse> customDeserialization = null,
            bool multiPartContent = false)
        {
            CreateWalletResponse walletResponse = ReflectionUtil.DeserializeFile<CreateWalletResponse>("create_wallet_mock");
            if (walletResponse is TResponse)
            {
                return Task.FromResult((TResponse) (object) walletResponse);
            }
            return Task.FromResult(default(TResponse));
        }

        public string GetApiCode()
        {
            return "Test";
        }
    }
}