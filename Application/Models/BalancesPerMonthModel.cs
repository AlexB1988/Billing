namespace Billing.Application.DTOs
{
    public class BalancesPerMonthModel
    {
        public DateTime Period { get; set; }

        public int AccountId { get; set; }

        public decimal InBalance { get; set; }

        public decimal Calculate { get; set; }

        public decimal Pay { get; set; }

        public decimal OutBalance { get; set; }
    }
}
