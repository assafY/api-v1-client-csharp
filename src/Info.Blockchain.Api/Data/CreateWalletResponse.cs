using Newtonsoft.Json;

namespace Info.Blockchain.Api.Data
{
    public class CreateWalletResponse
	{
		[JsonConstructor]
		public CreateWalletResponse() {}

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
	}
}