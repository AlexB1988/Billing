using Billing.Application.DTOs;
using Billing.Application.Enums;
using Billing.Application.Exceptions;
using Billing.Application.Interfaces;
using Billing.Application.ViewModels;

namespace Billing.Application.Services
{
    public class BalancesService : IBalancesService
    {

        private readonly IBalancesPerMonth _balancesPerMonth; 

        public BalancesService(IBalancesPerMonth balancesPerMonth) 
            => _balancesPerMonth = balancesPerMonth;

        public async Task<ICollection<BalancesViewModel>> GetBalances(int accountId, Period period)
        {
            var balance =await _balancesPerMonth.GetBalancesPerMonth(accountId);

            var resultPerPeriod = period switch
            {
                Period.Month => balance.GroupBy(x => new { x.Period },
                                                (x, y) => new { PeriodName = x.Period.ToString("yyyyMM"), Balances = y }),

                Period.Quater => balance.GroupBy(x => new { Year = x.Period.Year, Quater = (x.Period.Month - 1) / 3 + 1 },
                                                (x, y) => new { PeriodName =  $"{x.Year} {x.Quater}", Balances = y }),

                Period.Year => balance.GroupBy(x => new { x.Period.Year },
                                                (x, y) => new {PeriodName = $"{x.Year}", Balances = y}),

                _ => throw new NotFoundException("Некорректно задан период")
            };


            var result = resultPerPeriod.Select((x) => new BalancesViewModel
                                        {
                                            AccountId = accountId,
                                            Period = x.PeriodName,
                                            InBalance = x.Balances.FirstOrDefault().InBalance,
                                            Calculate = x.Balances.Sum(y => y.Calculate),
                                            Pay = x.Balances.Sum(y => y.Pay),
                                            OutBalance = x.Balances.LastOrDefault().OutBalance
                                        }).OrderByDescending(x => x.Period).ToList();

            return result;
        } 
    }
}
