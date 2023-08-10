using Billing.Application.Interfaces;
using Billing.Domain.Entities;
using Newtonsoft.Json;
using System.Text;

namespace Billing.Infrastructure
{
    public class PaymentFromFile : IPayment
    {
        public async Task<ICollection<Payment>> GetPayments()
        {
            using var file = File.OpenText(@"Infrastructure\payment_202105270827.json");

            var stringJson = await file.ReadToEndAsync();

            var payments = JsonConvert.DeserializeObject<List<Payment>>(stringJson.ToString());

            return payments;
        }
    }
}
