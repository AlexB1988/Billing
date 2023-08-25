using Newtonsoft.Json;

namespace Billing.Application.DTOs
{
    public class PaymentDto
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
