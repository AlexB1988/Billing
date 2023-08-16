using Billing.Application.DTOs;
using Billing.Application.Interfaces;

namespace Billing.Application.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IPayment _payment;

        public PaymentRepository(IPayment payment) =>
            _payment = payment;

        public async Task<ICollection<PaymentDto>> GetPayments()
        {
            var payments = await _payment.GetPayments();

            var paymentsDto = payments.Select(x => new PaymentDto
            {
                Guid = x.Guid,
                AccountId = x.AccountId,
                Sum = x.Sum,
                Date = x.Date
            }).ToList();

            return paymentsDto;
        }
    }
}
