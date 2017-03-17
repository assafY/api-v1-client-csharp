using System;
using System.Threading.Tasks;
using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Data;

namespace Info.Blockchain.Api.Wallet
{   
    /// <summary>
    /// This class reflects the functionality documented at https://blockchain.info/api/create_wallet.
    /// <summary>
    public class WalletCreator
    {       
        private readonly IHttpClient _httpClient;
        
        public WalletCreator(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
		/// Creates a new Blockchain.info wallet if the user's API code has the 'generate wallet' permission.
		/// It can be created containing a pre-generated private key or will otherwise generate a new private key.
		/// </summary>
		/// <param name="password">Password for the new wallet. At least 10 characters.</param>
		/// <param name="privateKey">Private key to add to the wallet</param>
		/// <param name="label">Label for the first address in the wallet</param>
		/// <param name="email">Email to associate with the new wallet</param>
		/// <returns>An instance of the CreateWalletResponse class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<CreateWalletResponse> Create(string password, string privateKey = null, string label = null, string email = null)
		{
			if (string.IsNullOrWhiteSpace(password))
			{
			throw new ArgumentNullException(nameof(password));
			}
			if (string.IsNullOrWhiteSpace(_httpClient._apiCode))
			{
                throw new ArgumentNullException("Api code must be specified", innerException: null);
			}
			
			var request = new CreateWalletRequest {
				Password = password,
				PrivateKey = privateKey,
				Label = label,
				Email = email
			};

			return await _httpClient.PostAsync<CreateWalletRequest, CreateWalletResponse>("api/v2/create_wallet", request);
		}
    }
}