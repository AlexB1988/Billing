using Billing.Application.Interfaces;
using Billing.Application.ViewModels;
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
            var paymentsTemp = await _payment.GetPayments();

            var result = new List<GetBalancesViewModel>();

            var balances = balancesTemp.Where(account => account.AccountId == accountId).OrderBy(period => period.Period);
            var payments = paymentsTemp.Where(account => account.AccountId == accountId);

            decimal paymentPeriodSum=0;
            int balancesCounter=0;


            foreach(var balance in balances)
            {
                result.Add(new GetBalancesViewModel 
                        { 
                            Period = DateTime.ParseExact(balance.Period, "yyyyMM", CultureInfo.InvariantCulture),
                            AccountId = balance.AccountId,
                            InBalance = (balancesCounter !=0) ?(result[balancesCounter - 1].OutBalance):(balance.InBalance),
                            Calculate = balance.Calculation
                        });

                foreach (var payment in payments)
                {

                    if (result[balancesCounter].Period.Month == payment.Date.Month 
                                && result[balancesCounter].Period.Year == payment.Date.Year)
                    {
                        paymentPeriodSum += payment.Sum;
                    }
                }

                result[balancesCounter].Pay = paymentPeriodSum;
                result[balancesCounter].OutBalance = result[balancesCounter].InBalance 
                    + result[balancesCounter].Calculate 
                    - result[balancesCounter].Pay;

                balancesCounter++;
                paymentPeriodSum = 0;
            }

            switch (period)
            {
                case Period.Month:
                    return result.OrderByDescending(period => period.Period).ToList();                
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
