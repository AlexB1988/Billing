using Newtonsoft.Json;

namespace Billing.Domain.Entities
{
    public class Payment
    {
        [JsonProperty("payment_guid")]
        public Guid Guid { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("sum")]
        public decimal Sum { get; set; }
    }
}
