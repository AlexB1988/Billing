﻿using Billing.Application.Enums;
using Billing.Application.Interfaces;
using Billing.Application.ViewModels;

namespace Billing.Application.Services
{
    public class BalancesService : IBalancesService
    {

        private readonly IBalancesPerMonth _balancesPerMonth; 

        public BalancesService(IBalancesPerMonth balancesPerMonth) 
            => _balancesPerMonth = balancesPerMonth;

        public async Task<ICollection<GetBalancesViewModel>> GetBalances(int accountId, Period period)
        {
            var result =await _balancesPerMonth.GetBalancesPerMonth(accountId);

            switch (period)
            {
                case Period.Month:
                    return result.Select(x => new GetBalancesViewModel
                        {
                            AccountId = accountId,
                            Period = x.Period.ToString("yyyyMM"),
                            InBalance = x.InBalance,
                            Calculate = x.Calculate,
                            Pay = x.Pay,
                            OutBalance = x.OutBalance
                        }).OrderByDescending(x => x.Period).ToList();

                case Period.Quater:
                    return result.GroupBy(x => new { Year = x.Period.Year, Quater = (x.Period.Month - 1) / 3 + 1 })
                        .Select((x) => new GetBalancesViewModel
                        {
                            AccountId = accountId,
                            Period = x.Key.Year.ToString() + " " + x.Key.Quater.ToString(),
                            InBalance = x.FirstOrDefault().InBalance,
                            Calculate = x.Sum(y => y.Calculate),
                            Pay = x.Sum(y => y.Pay),
                            OutBalance = x.LastOrDefault().OutBalance
                        }).OrderByDescending(x => x.Period).ToList();

                case Period.Year:
                    return result.GroupBy(x => new { Year = x.Period.Year })
                        .Select((x) => new GetBalancesViewModel
                        {
                            AccountId = accountId,
                            Period = x.Key.Year.ToString(),
                            InBalance = x.FirstOrDefault().InBalance,
                            Calculate = x.Sum(y => y.Calculate),
                            Pay = x.Sum(y => y.Pay),
                            OutBalance = x.LastOrDefault().OutBalance
                        }).OrderByDescending(x => x.Period).ToList();
                default:
                    throw new Exception();
            }
        } 
    }
}