using Billing.Application.DTOs;

namespace Billing.Application.Interfaces
{
    public interface IBalancesPerMonth
    {
        public Task<ICollection<BalancesPerMonthDto>> GetBalancesPerMonth(int accountId);
    }
}