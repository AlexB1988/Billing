using Billing.Application.DTOs;

namespace Billing.Application.Interfaces
{
    public interface IBalancesPerMonth
    {
        public Task<ICollection<BalancesPerMonthModel>> GetBalancesPerMonth(int accountId);
    }
}