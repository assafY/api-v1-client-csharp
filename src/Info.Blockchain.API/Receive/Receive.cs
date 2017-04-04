using System.Threading.Tasks;
using Info.Blockchain.API.Client;

namespace Info.Blockchain.API.Receive
{
    public class Receive
    {
        private readonly IHttpClient httpClient;

        public Receive(IHttpClient httpClient = null)
        {
            this.httpClient = (httpClient == null)
                ? new BlockchainHttpClient("https://api.blockchain.info/v2")
                : httpClient;
        }

        /// <summary>
        /// Generate a new address for an Xpub
        /// </summary>
        /// <param name="xpub">The Xpub to generate a new address from</param>
        /// <param name="callback">The callback URL to be notified when a payment is received</param>
        /// <param name="key">The blockchain.info receive payments v2 api key</param>
        /// <returns></returns>
        public async Task<ReceivePaymentResponse> GenerateAddressAsync(string xpub, string callback, string key)
        {
            var queryString = new QueryString();
            queryString.Add("xpub", xpub);
            queryString.Add("callback", callback);
            queryString.Add("key", key);

            return await httpClient.GetAsync<ReceivePaymentResponse>("receive", queryString);
        }
    }
}