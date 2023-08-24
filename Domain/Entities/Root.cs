using Newtonsoft.Json;

namespace Billing.Domain.Entities
{
    public class Root
    {
        [JsonProperty("balance")]
        public ICollection<Balance> Balances { get; set; }
    }
}
