using Newtonsoft.Json;

namespace Info.Blockchain.Api.Data
{
    public class CreateWalletResponse
	{
		/// <summary>
		/// Wallet identifier (GUID)
		/// </summary>
		[JsonProperty("guid", Required = Required.Always)]
		public string Identifier { get; private set; }

		/// <summary>
		/// First address in the wallet
		/// </summary>
		[JsonProperty("address", Required = Required.Always)]
		public string Address { get; private set; }

		/// <summary>
		/// Wallet label
		/// </summary>
		[JsonProperty("label", Required = Required.Always)]
		public string Label { get; private set; }
	}
}