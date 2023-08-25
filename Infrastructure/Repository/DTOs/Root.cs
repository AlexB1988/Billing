using Billing.Application.DTOs;
using Newtonsoft.Json;

namespace Billing.Domain.Entities
{
    public class Root
    {
        [JsonProperty("balance")]
        public ICollection<BalanceDto> Balances { get; set; }
    }
}
