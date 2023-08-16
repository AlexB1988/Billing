using Billing.Application.DTOs;

namespace Billing.Application.Interfaces
{
    public interface IPaymentRepository
    {
        public Task<ICollection<PaymentDto>> GetPayments();
    }
}
