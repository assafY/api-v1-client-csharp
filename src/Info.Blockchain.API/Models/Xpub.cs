using Newtonsoft.Json;

namespace Info.Blockchain.API.Models
{
    public class Xpub : Address
    {
        [JsonProperty("change_index")]
        public int ChangeIndex { get; private set; }

        [JsonProperty("account_index")]
        public int AccountIndex { get; private set; }

        [JsonProperty("gap_limit")]
        public int GapLimit { get; private set; }
    }
}