using Newtonsoft.Json;

namespace Billing.Domain.Entities
{
    public class Balance
    {
        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("in_balance")]
        public decimal InBalance { get; set; }

        [JsonProperty("calculation")]
        public decimal Calculation { get; set; }
    }
}
