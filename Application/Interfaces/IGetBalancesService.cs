using Billing.Application.Services;
using Billing.Application.ViewModels;

namespace Billing.Application.Interfaces
{
    public interface IGetBalancesService
    {
        public Task<List<GetBalancesViewModel>> GetBalances(int accountId, Period period);
    }
}
