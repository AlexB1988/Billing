using Billing.Application.Interfaces;
using Billing.Domain.Entities;
using Newtonsoft.Json;
using System.Text;

namespace Billing.Infrastructure
{
    public class PaymentFromFile : IPayment
    {
        public async Task<List<Payment>> GetPayments()
        {
            using var file = new FileStream(@"Infrastructure\payment_202105270827.json", FileMode.Open);

            byte[] buffer = new byte[file.Length];

            await file.ReadAsync(buffer, 0, buffer.Length);

            string stringJson = Encoding.Default.GetString(buffer);

            var payments = JsonConvert.DeserializeObject<List<Payment>>(stringJson.ToString());

            return payments;
        }
    }
}
