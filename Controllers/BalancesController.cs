using Billing.Application.DTOs;
using Billing.Application.Interfaces;
using Billing.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BalancesController : ControllerBase
    {
        private readonly IGetBalancesService _getBalancesService;
        private readonly IGetDebtService _getDebtService;

        public BalancesController(IGetBalancesService getBalancesService, IGetDebtService getDebtService) 
            => (_getBalancesService, _getDebtService) = (getBalancesService, getDebtService);


        [HttpGet("GetBalances")]
        public async Task<IActionResult> GetBalances([FromQuery] GetBalancesParametersDto parametersDto)
        {
            var result = await _getBalancesService.GetBalances(parametersDto);

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
