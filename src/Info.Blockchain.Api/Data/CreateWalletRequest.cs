using Newtonsoft.Json;

namespace Info.Blockchain.Api.Data
{
    public class CreateWalletRequest
	{
		[JsonConstructor]
		public CreateWalletRequest() {}

		[JsonProperty("api_code")]
		public string ApiCode { get; set; }
		[JsonProperty("email")]
		public string Email { get; set; }
		[JsonProperty("label")]
		public string Label { get; set; }
		[JsonProperty("password")]
		public string Password { get; set; }
		// not providing a private key throws a 
		// create wallet error, therefore it is
		//disabled for the time being
		/*[JsonProperty("priv")]
		public string PrivateKey { get; set; }*/
	}
}