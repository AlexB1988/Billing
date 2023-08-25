using Newtonsoft.Json;

namespace Billing.Application.DTOs
{
    public class BalanceDto
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
