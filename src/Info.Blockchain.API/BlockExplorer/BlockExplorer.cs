using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Info.Blockchain.API.Client;
using Info.Blockchain.API.Json;
using Info.Blockchain.API.Models;

namespace Info.Blockchain.API.BlockExplorer
{
	/// <summary>
	/// The BlockExplorer class reflects the functionality documented at
	/// https://blockchain.info/api/blockchain_api. It can be used to query the block chain,
	/// fetch block, transaction and address data, get unspent outputs for an address etc.
	/// </summary>
	public class BlockExplorer
	{
		private readonly IHttpClient httpClient;
		public const int MAX_TRANSACTIONS_PER_REQUEST = 50;

		public BlockExplorer()
		{
			httpClient  = new BlockchainHttpClient();
		}

		internal BlockExplorer(IHttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		/// <summary>
		///  Deprecated. Gets a single transaction based on a transaction index.
		/// </summary>
		/// <param name="index">Transaction index</param>
		/// <returns>An instance of the Transaction class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
        [System.Obsolete("Deprecated. Get transaction by hash wherever possible.")]
		public async Task<Transaction> GetTransactionByIndexAsync(long index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(index), "Index must be a positive integer");
			}

			return await GetTransactionAsync(index.ToString());
		}

		/// <summary>
		///  Gets a single transaction based on a transaction hash.
		/// </summary>
		/// <param name="hash">Transaction hash</param>
		/// <returns>An instance of the Transaction class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<Transaction> GetTransactionByHashAsync(string hash)
		{
			if (string.IsNullOrWhiteSpace(hash))
			{
				throw new ArgumentNullException(nameof(hash));
			}

			return await GetTransactionAsync(hash);
		}

        private async Task<Transaction> GetTransactionAsync(string hashOrIndex)
		{
			if (string.IsNullOrWhiteSpace(hashOrIndex))
			{
				throw new ArgumentNullException(nameof(hashOrIndex));
			}

			return await httpClient.GetAsync<Transaction>("rawtx/" + hashOrIndex);
		}

        /*private async Task<List<Transaction>> GetTransactionsAsync(Address address, int? limit)
		{
			if (address == null)
			{
				throw new ArgumentNullException(nameof(address));
			}

			limit = limit ?? (int)address.TransactionCount;
			IList<Task<Address>> tasks = new List<Task<Address>>();

			int offset;
			for (offset = MAX_TRANSACTIONS_PER_REQUEST; offset <= limit - MAX_TRANSACTIONS_PER_REQUEST; offset += MAX_TRANSACTIONS_PER_REQUEST)
			{
				Task<Address> task = this.GetAddressWithOffsetAsync(address.AddressStr, MAX_TRANSACTIONS_PER_REQUEST, offset);
				tasks.Add(task);
			}

			if (offset < maxTransactionCount.Value)
			{
				int remainingTransactions = (int)maxTransactionCount.Value - offset;
				Task<Address> task = this.GetAddressWithOffsetAsync(address.AddressStr, remainingTransactions, offset);
				tasks.Add(task);
			}

			await Task.WhenAll(tasks.ToArray());

			List<Transaction> transactions = address.Transactions.ToList();

			foreach (Task<Address> task in tasks)
			{
				transactions.AddRange(task.Result.Transactions);
			}

			return transactions;
		}*/

		/// <summary>
		/// Deprecated. Gets a single block based on a block index.
		/// </summary>
		/// <param name="index">Block index</param>
		/// <returns>An instance of the Block class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
        [System.Obsolete("Deprecated. Get block by hash wherever possible.")]
		public async Task<Block> GetBlockByIndexAsync(long index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(index), "Index must be greater than zero");
			}
			return await GetBlockAsync(index.ToString());
		}

		/// <summary>
		/// Gets a single block based on a block hash.
		/// </summary>
		/// <param name="hash">Block hash</param>
		/// <returns>An instance of the Block class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<Block> GetBlockByHashAsync(string hash)
		{
			if (string.IsNullOrWhiteSpace(hash))
			{
				throw new ArgumentNullException(nameof(hash));
			}
			return await GetBlockAsync(hash);
		}

        private async Task<Block> GetBlockAsync(string hashOrIndex)
		{
			if (string.IsNullOrWhiteSpace(hashOrIndex))
			{
				throw new ArgumentNullException(nameof(hashOrIndex));
			}
			return await httpClient.GetAsync<Block>("rawblock/" + hashOrIndex, customDeserialization: Block.Deserialize);
		}

        /// <summary>
		/// Gets data for a single Base58Check address asynchronously.
		/// </summary>
		/// <param name="address">Base58Check address string</param>
		/// <param name="limit">Max amount of transactions to retrieve (Max 50)</param>
        /// <param name="offset">Number of transactions to skip</param>
		/// <returns>An instance of the Address class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
        public async Task<Address> GetBase58AddressAsync(string address, int? limit, int? offset)
        {
            return await GetAddressAsync(address, limit, offset);
        }

        /// <summary>
		/// Gets data for a single Hash160 address asynchronously.
		/// </summary>
		/// <param name="address">Hash160 address string</param>
		/// <param name="limit">Max amount of transactions to retrieve (Max 50)</param>
        /// <param name="offset">Number of transactions to skip</param>
		/// <returns>An instance of the Address class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
        public async Task<Address> GetHash160AddressAsync(string address, int? limit, int? offset)
        {
            return await GetAddressAsync(address, limit, offset);
        }

        /// <summary>
		/// Gets data for a single Xpub address asynchronously.
		/// </summary>
		/// <param name="address">Xpub address string</param>
		/// <param name="limit">Max amount of transactions to retrieve (Max 50)</param>
        /// <param name="offset">Number of transactions to skip</param>
		/// <returns>An instance of the Address class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
        public async Task<Address> GetXpubAddressAsync(string address, int? limit, int? offset)
        {
            return await GetAddressAsync(address, limit, offset);
        }

		private async Task<Address> GetAddressAsync(string address, int? limit, int? offset)
		{
            QueryString queryString = new QueryString();

			if (string.IsNullOrWhiteSpace(address))
			{
				throw new ArgumentNullException(address);
			}
			if (limit != null)
			{
                if (limit < 1 || limit > MAX_TRANSACTIONS_PER_REQUEST)
                {
    				throw new ArgumentOutOfRangeException(nameof(limit), "transaction limit must be greater than 0 and smaller than " + MAX_TRANSACTIONS_PER_REQUEST);
                }
                queryString.Add("limit", limit.ToString());
			}
            if (offset != null)
            {
                if (offset < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(offset), "offset must be greater than 0");
                }
                queryString.Add("offset", offset.ToString());
            }

			queryString.Add("format", "json");

            return await httpClient.GetAsync<Address>("address/" + address, queryString);
			// Address addressObj = await httpClient.GetAsync<Address>("address/" + address, queryString);
			// List<Transaction> transactionList = await GetTransactionsAsync(addressObj, limit);
			// return new Address(addressObj, transactionList);
		}

		/// <summary>
		/// Gets a list of blocks at the specified height. Normally, only one block will be returned,
		/// but in case of a chain fork, multiple blocks may be present.
		/// </summary>
		/// <param name="height">Block height</param>
		/// <returns>A list of blocks at the specified height</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<ReadOnlyCollection<Block>> GetBlocksAtHeightAsync(long height)
		{
			if (height < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(height), "Block height must be greater than or equal to zero");
			}
			QueryString queryString = new QueryString();
			queryString.Add("format", "json");

			return await httpClient.GetAsync("block-height/" + height, queryString, Block.DeserializeMultiple);
		}

		/// <summary>
		/// Gets unspent outputs for a single Base58Check address.
		/// </summary>
		/// <param name="address">Base58check address string</param>
		/// <returns>A list of unspent outputs for the specified address </returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<ReadOnlyCollection<UnspentOutput>> GetUnspentOutputsAsync(string address)
		{
			if (string.IsNullOrWhiteSpace(address))
			{
				throw new ArgumentNullException(nameof(address));
			}
			QueryString queryString = new QueryString();
			queryString.Add("active", address);
			try
			{
				return await httpClient.GetAsync("unspent", queryString, UnspentOutput.DeserializeMultiple);
			}
			catch (Exception ex)
			{
				// Currently the API throws an internal error if there are no free outputs to spend. No free outputs is
				// a legitimate response. Therefore, until we fix this issue, we are circumventing this by returning an empty list
				if (ex.Message.Contains("outputs to spend"))
				{
					return new ReadOnlyCollection<UnspentOutput>(new List<UnspentOutput>());
				}
				throw;
			}
		}

		/// <summary>
		/// Gets the latest block on the main chain (simplified representation).
		/// </summary>
		/// <returns>An instance of the LatestBlock class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<LatestBlock> GetLatestBlockAsync()
		{
			return await httpClient.GetAsync<LatestBlock>("latestblock");
		}

		/// <summary>
		/// Gets a list of currently unconfirmed transactions.
		/// </summary>
		/// <returns>A list of unconfirmed Transaction objects</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<ReadOnlyCollection<Transaction>> GetUnconfirmedTransactionsAsync()
		{
			QueryString queryString = new QueryString();
			queryString.Add("format", "json");

			return await httpClient.GetAsync("unconfirmed-transactions", queryString, Transaction.DeserializeMultiple);
		}

		/// <summary>
		/// Gets a list of blocks mined on a specific day.
		/// </summary>
		/// <param name="dateTime">DateTime that falls
		/// between 00:00 UTC and 23:59 UTC of the desired day.</param>
		/// <returns>A list of SimpleBlock objects</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<ReadOnlyCollection<SimpleBlock>> GetBlocksByDateTimeAsync(DateTime dateTime)
		{
			DateTimeOffset utcDate = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
			var unixMillis = (long)(utcDate.ToUnixTimeMilliseconds());

			if (unixMillis < UnixDateTimeJsonConverter.GenesisBlockUnixMillis)
			{
				throw new ArgumentOutOfRangeException(nameof(dateTime), "Date must be greater than or equal to the genesis block creation date (2009-01-03T18:15:05+00:00)");
			}
			if (dateTime.ToUniversalTime() > DateTime.UtcNow)
			{
				throw new ArgumentOutOfRangeException(nameof(dateTime), "Date must be in the past");
			}
			return await GetBlocksAsync(unixMillis.ToString());
		}

		/// <summary>
		/// Gets a list of blocks mined on a specific day.
		/// </summary>
		/// <param name="unixMillis">Unix timestamp in milliseconds that falls
		/// between 00:00 UTC and 23:59 UTC of the desired day.</param>
		/// <returns>A list of SimpleBlock objects</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<ReadOnlyCollection<SimpleBlock>> GetBlocksByTimestampAsync(long unixMillis)
		{
			if (unixMillis < UnixDateTimeJsonConverter.GenesisBlockUnixMillis)
			{
				throw new ArgumentOutOfRangeException(nameof(unixMillis), "Date must be greater than or equal to the genesis block creation date (2009-01-03T18:15:05+00:00)");
			}
			return await GetBlocksAsync(unixMillis.ToString());
		}

        /// <summary>
		/// Gets a list of recent blocks by a specific mining pool (Max 101).
        /// No input returns all blocks mined today since midnight.
        /// </summary>
        /// <param name="poolName">Name of mining pool</param>
        /// <returns>A list of SimpleBlock objects</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
        public async Task<ReadOnlyCollection<SimpleBlock>> GetBlocksByPoolNameAsync(string poolName = "")
        {
            return await GetBlocksAsync(poolName);
        }

		/// <summary>
		/// Gets a list of recent blocks by a specific mining pool or up to a specified timestamp.
        /// If neither is provided returns all blocks mined since midnight.
		/// </summary>
		/// <param name="poolNameOrTimestamp">Name of the mining pool or Unix timestamp in milliseconds that falls
		/// between 00:00 UTC and 23:59 UTC of the desired day.</param>
		/// <returns>A list of SimpleBlock objects</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		private async Task<ReadOnlyCollection<SimpleBlock>> GetBlocksAsync(string poolNameOrTimestamp)
		{
			QueryString queryString = new QueryString();
			queryString.Add("format", "json");

			ReadOnlyCollection<SimpleBlock> simpleBlocks = await httpClient.GetAsync("blocks/" + poolNameOrTimestamp, queryString, SimpleBlock.DeserializeMultiple);

			return simpleBlocks;
		}
	}
}