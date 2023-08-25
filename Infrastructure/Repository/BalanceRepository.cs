using Billing.Application.DTOs;
using Billing.Application.Interfaces;
using Billing.Domain.Entities;
using Newtonsoft.Json;
using System.Text;

namespace Billing.Infrastructure
{
    public class BalanceRepository : IBalanceRepository
    {
        public async Task<ICollection<Balance>> GetBalances()
        {
            using var file = File.OpenText(@"Infrastructure\balance_202105270825.json");

            var stringJson =await file.ReadToEndAsync();

            var balances = JsonConvert.DeserializeObject<Root>(stringJson);

            var balancesDto = balances.Balances.Select(x => new Balance
            {
                AccountId = x.AccountId,
                Period = DateTime.ParseExact(x.Period, "yyyyMM", null),
                InBalance = x.InBalance,
                Calculation = x.Calculation
            }).ToList();

            return balancesDto;
        }
    }
}
