using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Info.Blockchain.API.Json;
using Newtonsoft.Json;

namespace Info.Blockchain.API.Models
{
	/// <summary>
	/// Represents an address.
	/// </summary>
	public class Address
	{

		[JsonConstructor]
		// ReSharper disable once UnusedMember.Local
		private Address() {}

		/// <summary>
		/// Hash160 representation of the address
		/// </summary>
		[JsonProperty("hash160", Required = Required.Always)]
		public string Hash160 { get; private set; }

		/// <summary>
		/// Base58Check representation of the address
		/// </summary>
		[JsonProperty("address", Required = Required.Always)]
		public string Base58Check { get; private set; }

		/// <summary>
		/// Total amount received
		/// </summary>
		[JsonProperty("total_received", Required = Required.Always)]
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		public BitcoinValue TotalReceived { get; private set; }

		/// <summary>
		/// Total amount sent
		/// </summary>
		[JsonProperty("total_sent", Required = Required.Always)]
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		public BitcoinValue TotalSent { get; private set; }

		/// <summary>
		/// Final balance of the address
		/// </summary>
		[JsonProperty("final_balance", Required = Required.Always)]
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		public BitcoinValue FinalBalance { get; private set; }

		/// <summary>
		/// Total count of all transactions of this address
		/// </summary>
		[JsonProperty("n_tx", Required = Required.Always)]
		public long TransactionCount { get; private set; }

		/// <summary>
		/// List of transactions associated with this address
		/// </summary>
		[JsonProperty("txs", Required = Required.Always)]
		public ReadOnlyCollection<Transaction> Transactions { get; private set; }
	}
}
