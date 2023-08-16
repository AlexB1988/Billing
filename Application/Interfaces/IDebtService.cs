namespace Billing.Application.Interfaces
{
    public interface IDebtService
    {
        public Task<decimal> GetDebt(int accountId);
    }
}
