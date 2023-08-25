

namespace Billing.Domain.Entities
{
    public class Payment
    {
        public Guid Guid { get; set; }

        public int AccountId { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }
    }
}
