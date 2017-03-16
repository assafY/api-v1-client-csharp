using Newtonsoft.Json;

namespace Info.Blockchain.Api.Data
{
    public class CreateWalletRequest
	{
		[JsonProperty("email")]
		public string Email { get; set; }
		[JsonProperty("label")]
		public string Label { get; set; }
		[JsonProperty("password")]
		public string Password { get; set; }
		[JsonProperty("priv")]
		public string PrivateKey { get; set; }
	}
}