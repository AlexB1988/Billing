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

            return balances.LastOrDefault().OutBalance;
        }
    }
}
