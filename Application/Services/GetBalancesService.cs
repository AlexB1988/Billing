using Billing.Application.DTOs;
using Billing.Application.Interfaces;
using Billing.Application.ViewModels;
using System.Runtime.CompilerServices;

namespace Billing.Application.Services
{
    public class GetBalancesService : IGetBalancesService
    {

        private readonly IBalancesPerMonth _balancesPerMonth; 

        public GetBalancesService(IBalancesPerMonth balancesPerMonth) 
            => _balancesPerMonth = balancesPerMonth;

        public async Task<List<GetBalancesViewModel>> GetBalances(int accountId, Period period)
        {
            var result =await _balancesPerMonth.GetBalancesPerMonth(accountId);

            switch (period)
            {
                case Period.Month:
                    return result.OrderByDescending(x => x.Period)
                        .Select(x => new GetBalancesViewModel
                        {
                            AccountId = x.AccountId,
                            Period = x.Period.ToString("yyyyMM"),
                            InBalance = x.InBalance,
                            Calculate = x.Calculate,
                            Pay = x.Pay,
                            OutBalance = x.OutBalance
                        }).ToList();

                //case Period.Quater:
                //    return result.GroupBy(x => new { Year = x.Period.Year, Quater = x.Period.Month % 4 + 1 })
                //        .Select((x) => new GetBalancesViewModel
                //        {
                //            AccountId = accountId,
                //            Period = x.Key.Year.ToString() +" "+ x.Key.Quater.ToString(),
                //            InBalance = x.OrderBy(y => y.Period.Month).FirstOrDefault().InBalance,
                //            Calculate = x.Sum(y => y.Calculate),
                //            Pay = x.Sum(y => y.Pay),
                //            OutBalance = x.OrderBy(y => y.Period.Month).LastOrDefault().OutBalance
                //        }).OrderByDescending(x => x.Period).ToList();     
                    
                case Period.Year:
                    return result.OrderByDescending(x => x.Period)
                        .GroupBy(x => new { Year = x.Period.Year })
                        .Select((x) => new GetBalancesViewModel
                        {
                            AccountId = accountId,
                            Period = x.Key.ToString(),
                            InBalance = x.OrderBy(y => y.Period.Month).FirstOrDefault().InBalance,
                            Calculate = x.Sum(y => y.Calculate),
                            Pay = x.Sum(y => y.Pay),
                            OutBalance = x.OrderBy(y => y.Period.Month).LastOrDefault().OutBalance
                        }).ToList();
                default:
                    throw new Exception();
            }
        } 
    }

    public enum Period
    {
        Month,
        Quater,
        Year
    }
}
