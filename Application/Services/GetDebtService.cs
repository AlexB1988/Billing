using Billing.Application.Interfaces;

namespace Billing.Application.Services
{
    public class GetDebtService : IGetDebtService
    {
        private readonly IBalancesPerMonth _balancesPerMonth;

        public GetDebtService(IBalancesPerMonth balancesPerMonth)
            => _balancesPerMonth = balancesPerMonth;

        public async Task<decimal> GetDebt(int accountId)
        {
            var balances = await _balancesPerMonth.GetBalancesPerMonth(accountId);

            if (balances.Count > 0)
            {
                return balances.LastOrDefault().OutBalance;
            }

            return 0;
        }
    }
}
