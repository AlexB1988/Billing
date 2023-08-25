using Billing.Domain.Entities;

namespace Billing.Application.Interfaces
{
    public interface IPaymentRepository
    {
        public Task<ICollection<Payment>> GetPayments();
    }
}
