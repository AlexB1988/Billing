using Billing.Application.DTOs;

namespace Billing.Application.Interfaces
{
    public interface IBalanceRepository
    {
        public Task<ICollection<BalanceDto>> GetBalances();
    }
}
