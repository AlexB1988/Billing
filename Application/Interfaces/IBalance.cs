using Billing.Domain.Entities;

namespace Billing.Application.Interfaces
{
    public interface IBalance
    {
        public Task<ICollection<Balance>> GetBalances();
    }
}
