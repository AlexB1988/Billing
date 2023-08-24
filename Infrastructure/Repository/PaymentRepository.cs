using Billing.Application.DTOs;
using Billing.Application.Interfaces;
using Billing.Domain.Entities;
using Newtonsoft.Json;

namespace Billing.Infrastructure.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        public async Task<ICollection<PaymentDto>> GetPayments()
        {
            using var file = File.OpenText(@"Infrastructure\payment_202105270827.json");

            var stringJson = await file.ReadToEndAsync();

            var payments = JsonConvert.DeserializeObject<ICollection<Payment>>(stringJson.ToString());

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
