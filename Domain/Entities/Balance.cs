

namespace Billing.Domain.Entities
{
    public class Balance
    {
        public int AccountId { get; set; }

        public DateTime Period { get; set; }

        public decimal InBalance { get; set; }

        public decimal Calculation { get; set; }
    }
}
