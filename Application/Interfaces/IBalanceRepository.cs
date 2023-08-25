using Billing.Domain.Entities;

namespace Billing.Application.Interfaces
{
    public interface IBalanceRepository
    {
        public Task<ICollection<Balance>> GetBalances();
    }
}
