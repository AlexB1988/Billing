using Billing.Application.Enums;
using Billing.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BalancesController : ControllerBase
    {
        private readonly IBalancesService _getBalancesService;
        private readonly IDebtService _getDebtService;

        public BalancesController(IBalancesService getBalancesService, IDebtService getDebtService) 
            => (_getBalancesService, _getDebtService) = (getBalancesService, getDebtService);


        [HttpGet("GetBalances/{accountId}/{period}")]
        public async Task<IActionResult> GetBalances(int accountId, Period period)
        {
            var result = await _getBalancesService.GetBalances(accountId,period);

            return Ok(result);
        }

        [HttpGet("GetDebt/{accountId}")]
        public async Task<IActionResult> GetDebt(int accountId)
        {
            var result = await _getDebtService.GetDebt(accountId);
            return Ok(result);
        }
    }
}
