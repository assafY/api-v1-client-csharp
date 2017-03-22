using System;
using Info.Blockchain.Api.ExchangeRates;
using Info.Blockchain.Api.PushTx;
using Info.Blockchain.Api.Statistics;
using Info.Blockchain.Api.Wallet;

namespace Info.Blockchain.Api.Client
{
	public class ApiHelper : IDisposable
	{
        private readonly IHttpClient _baseHttpClient;
        private readonly IHttpClient _serviceHttpClient;
        public readonly BlockExplorer.BlockExplorer _blockExplorer;
		public readonly WalletCreator _walletCreator;
        public readonly TransactionBroadcaster _transactionBroadcaster;
    	public readonly ExchangeRateExplorer _exchangeRateExplorer;
		public readonly StatisticsExplorer _statisticsExplorer;


		public ApiHelper(string apiCode = null, IHttpClient baseHttpClient = null, string serviceUrl = null, IHttpClient serviceHttpClient = null)
		{

			if (baseHttpClient == null)
			{
				_baseHttpClient = new BlockchainHttpClient(apiCode);
			}
            else
            {
				_baseHttpClient = baseHttpClient;

				if (apiCode != null)
				{
					_baseHttpClient._apiCode = apiCode;
                }
			}

            if (serviceHttpClient == null && serviceUrl != null)
            {
                _serviceHttpClient = new BlockchainHttpClient(apiCode, serviceUrl);
            }
            else if (serviceHttpClient != null)
            {
                _serviceHttpClient = serviceHttpClient;

                if (apiCode != null)
                {
                    _serviceHttpClient._apiCode = apiCode;
                }
            }
            else
            {
                _serviceHttpClient = null;
            }

            _blockExplorer = new BlockExplorer.BlockExplorer(_baseHttpClient);
            _transactionBroadcaster = new TransactionBroadcaster(_baseHttpClient);
			_exchangeRateExplorer = new ExchangeRateExplorer(_baseHttpClient);
            _statisticsExplorer = new StatisticsExplorer(_baseHttpClient);

            if (_serviceHttpClient != null)
            {
                _walletCreator = new WalletCreator(_serviceHttpClient);
            }
            else
            {
                _walletCreator = null;
            }

        }

        /// <summary>
        /// Creates an instance of 'WalletHelper' based on the identifier allowing the use
        /// of that wallet
        /// </summary>
        /// <param name="identifier">Wallet identifier (GUID)</param>
        /// <param name="password">Decryption password</param>
        /// <param name="secondPassword">Second password</param>
        public Wallet.Wallet CreateWallet(string identifier, string password, string secondPassword = null)
		{
            if (_serviceHttpClient == null)
            {
                throw new ClientApiException("In order to create wallets, you must provide a valid service_url to BlockchainApiHelper");
            }

            return new Wallet.Wallet(_serviceHttpClient, identifier, password, secondPassword);
		}

        public WalletCreator CreateWalletCreator()
        {
            return new WalletCreator(_serviceHttpClient);
        }

		public void Dispose()
		{
            _baseHttpClient.Dispose();

            if (_serviceHttpClient != null)
            {
                _serviceHttpClient.Dispose();
            }
		}
	}
}