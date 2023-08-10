using Billing.Application.Interfaces;
using Billing.Domain.Entities;
using Newtonsoft.Json;
using System.Text;

namespace Billing.Infrastructure
{
    public class BalanceFormFile : IBalance
    {
        public async Task<ICollection<Balance>> GetBalances()
        {
            using var file = File.OpenText(@"Infrastructure\balance_202105270825.json");

            var stringJson =await file.ReadToEndAsync();

            var balances = JsonConvert.DeserializeObject<Root>(stringJson);

            return balances.Balances;
        }
    }
}
