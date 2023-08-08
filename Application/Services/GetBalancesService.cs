using Billing.Application.Interfaces;
using Billing.Application.ViewModels;
using System.Globalization;

namespace Billing.Application.Services
{
    public class GetBalancesService : IGetBalancesService
    {
        private IPayment _payment { get; set; }

        private IBalance _balance { get; set; }

        public GetBalancesService(IPayment payment, IBalance balance) 
            => (_payment, _balance) = (payment, balance);

        public async Task<List<GetBalancesViewModel>> GetBalances(int accountId, Period period)
        {
            var balancesTemp = await _balance.GetBalances();
            var payments = await _payment.GetPayments();

            var balances = balancesTemp
                .Where(account => account.AccountId == accountId)
                .OrderBy(period => period.Period).OrderBy(period => period.Period);


            switch (period)
            {
                case Period.Month:
                    var result = balances.GroupJoin(
                    inner: payments,
                    outerKeySelector: balance => balance.AccountId,
                    innerKeySelector: payment => payment.AccountId,
                    resultSelector: (b, p) => new GetBalancesViewModel
                    {
                        Period = b.Period,
                        AccountId = b.AccountId,
                        InBalance = b.InBalance,
                        Calculate = b.Calculation,
                        Pay = p.Where(p => p.Date.Year == b.Period.Year
                                    && p.Date.Month == b.Period.Month)
                               .Sum(pay => pay.Sum)
                    }).ToList();

                    for(int i = 0; i < result.Count(); i++)
                    {
                        if (i != 0)
                        {
                            result[i].InBalance = result[i - 1].OutBalance;
                        }
                        result[i].OutBalance = result[i].InBalance + result[i].Calculate - result[i].Pay;
                    }

                    return result.OrderByDescending(period => period.Period).ToList();

                case Period.Year:

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
