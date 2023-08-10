using Billing.Domain.Entities;

namespace Billing.Application.Interfaces
{
    public interface IPayment
    {
        public Task<ICollection<Payment>> GetPayments();
    }
}
