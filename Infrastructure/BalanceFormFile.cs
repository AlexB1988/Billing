using Billing.Application.Interfaces;
using Billing.Domain.Entities;
using Newtonsoft.Json;
using System.Text;

namespace Billing.Infrastructure
{
    public class BalanceFormFile : IBalance
    {
        public async Task<List<Balance>> GetBalances()
        {
            using var file = new FileStream(@"Infrastructure\balance_202105270825.json", FileMode.Open);

            byte[] buffer = new byte[file.Length];

            await file.ReadAsync(buffer, 0, buffer.Length);

            string stringJson = Encoding.Default.GetString(buffer);

            var balances = JsonConvert.DeserializeObject<Root>(stringJson.ToString());

            return balances.Balances;
        }
    }
}
