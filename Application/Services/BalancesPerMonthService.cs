using Billing.Application.DTOs;
using Billing.Application.Interfaces;
using Billing.Application.ViewModels;
using Billing.Controllers;
using Billing.Domain.Entities;

namespace Billing.Application.Services
{
    public class BalancesPerMonthService : IBalancesPerMonth
    {
        private readonly IBalanceRepository _balanceRepository;

        private readonly IPaymentRepository _paymentRepository;

        public BalancesPerMonthService(IBalanceRepository balanceRepository, IPaymentRepository paymentRepository) 
            => (_balanceRepository, _paymentRepository) = (balanceRepository, paymentRepository);


        public async Task<ICollection<BalancesPerMonthModel>> GetBalancesPerMonth(int accountId)
        {
            var balance = await _balanceRepository.GetBalances();
            var payment = await _paymentRepository.GetPayments();

            var result = balance.OrderBy(x => x.Period).Where(x => x.AccountId == accountId).GroupJoin(
                                inner: payment,
                                outerKeySelector: balance => balance.AccountId,
                                innerKeySelector: payment => payment.AccountId,
                                resultSelector: (b, p) => new BalancesPerMonthModel
                                {
                                    Period = b.Period,
                                    AccountId = b.AccountId,
                                    InBalance = b.InBalance,
                                    Calculate = b.Calculation,
                                    Pay = p.Where(p => p.Date.Year == b.Period.Year
                                                && p.Date.Month == b.Period.Month)
                                           .Sum(pay => pay.Sum)
                                }).ToList();

            for (int i = 0; i < result.Count(); i++)
            {
                if (i != 0)
                {
                    result[i].InBalance = result[i - 1].OutBalance;
                }
                result[i].OutBalance = result[i].InBalance + result[i].Calculate - result[i].Pay;
            }

            return result;
        }
    }
}
