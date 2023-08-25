using Billing.Application.Enums;
using Billing.Application.ViewModels;

namespace Billing.Application.Interfaces
{
    public interface IBalancesService
    {
        public Task<ICollection<BalancesViewModel>> GetBalances(int accountId, Period period);
    }
}
