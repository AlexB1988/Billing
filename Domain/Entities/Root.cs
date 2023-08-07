using Newtonsoft.Json;

namespace Billing.Domain.Entities
{
    public class Root
    {
        [JsonProperty("balance")]
        public List<Balance> Balances { get; set; }
    }
}
