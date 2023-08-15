namespace Billing.Application.Interfaces
{
    public interface IGetDebtService
    {
        public Task<decimal> GetDebt(int accountId);
    }
}
