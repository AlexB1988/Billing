using Billing.Application.Interfaces;
using Billing.Application.ViewModels;
using Billing.Domain.Entities;
using Microsoft.VisualBasic;
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

            //var result = new List<GetBalancesViewModel>();

            var balances = balancesTemp
                .Where(account => account.AccountId == accountId)
                .OrderBy(period => period.Period).OrderBy(period => period.Period);

            //var payments = paymentsTemp
            //    .Where(account => account.AccountId == accountId);

            //decimal paymentPeriodSum=0;
            //int balancesCounter=0;


            //foreach(var balance in balances)
            //{
            //    result.Add(new GetBalancesViewModel 
            //            { 
            //                Period = DateTime.ParseExact(balance.Period, "yyyyMM", CultureInfo.InvariantCulture),
            //                AccountId = balance.AccountId,
            //                InBalance = (balancesCounter !=0) ?(result[balancesCounter - 1].OutBalance):(balance.InBalance),
            //                Calculate = balance.Calculation
            //            });

            //    foreach (var payment in payments)
            //    {

            //        if (result[balancesCounter].Period.Month == payment.Date.Month 
            //                    && result[balancesCounter].Period.Year == payment.Date.Year)
            //        {
            //            paymentPeriodSum += payment.Sum;
            //        }
            //    }

            //    result[balancesCounter].Pay = paymentPeriodSum;
            //    result[balancesCounter].OutBalance = result[balancesCounter].InBalance 
            //        + result[balancesCounter].Calculate 
            //        - result[balancesCounter].Pay;

            //    balancesCounter++;
            //    paymentPeriodSum = 0;
            //}
            //var resultYear = result
            //    .GroupBy(period => period.Period.Year)
            //    .Select(p =>
            //        new {
            //            Period = p.Key,
            //            p.FirstOrDefault().InBalance,
            //            Calculate = p.Sum(s => s.Calculate),
            //            AccountId = p.Min(m => m.AccountId),
            //            Pay = p.Sum(s => s.Pay),
            //            p.LastOrDefault().OutBalance
            //        });

            switch (period)
            {
                case Period.Month:
                    var result = balances.GroupJoin(
                    inner: payments,
                    outerKeySelector: balance => balance.AccountId,
                    innerKeySelector: payment => payment.AccountId,
                    resultSelector: (b, p) => new GetBalancesViewModel
                    {
                        Period = DateTime.ParseExact(b.Period, "yyyyMM", CultureInfo.InvariantCulture),
                        AccountId = b.AccountId,
                        InBalance = b.InBalance,
                        Calculate = b.Calculation,
                        Pay = p.Where(p => p.Date.Year.ToString() == b.Period.Substring(0, 4)
                                    && p.Date.Month == int.Parse(b.Period.Substring(4)))
                               .Sum(pay => pay.Sum),
                        OutBalance = b.InBalance + b.Calculation - p.Where(p => p.Date.Year.ToString() == b.Period.Substring(0, 4)
                                    && p.Date.Month == int.Parse(b.Period.Substring(4)))
                               .Sum(pay => pay.Sum)
                    }).ToList();

                            int count = 0;

                            foreach (var re in result)
                            {
                                if (count != 0)
                                {
                                    result[count].InBalance = result[count - 1].OutBalance;
                                }
                                count++;
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
