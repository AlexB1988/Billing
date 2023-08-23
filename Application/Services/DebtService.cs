using Billing.Application.Exceptions;
using Billing.Application.Interfaces;

namespace Billing.Application.Services
{
    public class DebtService : IDebtService
    {
        private readonly IBalancesPerMonth _balancesPerMonth;

        public DebtService(IBalancesPerMonth balancesPerMonth)
            => _balancesPerMonth = balancesPerMonth;

        public async Task<decimal> GetDebt(int accountId)
        {
            var balances = await _balancesPerMonth.GetBalancesPerMonth(accountId);

            if (balances.Count == 0)
            {
                throw new NotFoundException($"Данных по этому лицевому счету нет");
            }

            var currentDate = DateTime.UtcNow;


            var lastYearBilling = balances.LastOrDefault().Period.Year;

            var lastMonthBilling = balances.LastOrDefault().Period.Month;

            if (lastYearBilling == currentDate.Year && lastMonthBilling == currentDate.Month)
            {
                return balances.Select(x => x.InBalance - x.Pay).LastOrDefault();
            };

            return balances.LastOrDefault().OutBalance;
        }
    }
}
