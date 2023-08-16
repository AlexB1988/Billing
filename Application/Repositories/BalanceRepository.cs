using Billing.Application.DTOs;
using Billing.Application.Interfaces;

namespace Billing.Application.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly IBalance _balance;

        public BalanceRepository(IBalance balance) => _balance = balance;

        public async Task<ICollection<BalanceDto>> GetBalances()
        {
            var balances = await _balance.GetBalances();

            var balancesDto = balances.OrderBy(x => x.Period).Select(x => new BalanceDto
            {
                AccountId = x.AccountId,
                Period = DateTime.ParseExact(x.Period, "yyyyMM", null),
                InBalance = x.InBalance,
                Calculation = x.Calculation
            });

            return balancesDto.ToList();
        }

    }
}
