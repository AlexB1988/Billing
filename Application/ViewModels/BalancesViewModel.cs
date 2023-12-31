﻿namespace Billing.Application.ViewModels
{
    public class BalancesViewModel
    {
        public string Period { get; set; }

        public int AccountId { get; set; }

        public decimal InBalance { get; set; }

        public decimal Calculate { get; set; }

        public decimal Pay { get; set; }

        public decimal OutBalance { get; set; }
    }
}
