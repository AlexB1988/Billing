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
        
        public BalancesController(IGetBalancesService getBalancesService) =>  _getBalancesService = getBalancesService;


        [HttpGet("GetBalances/{accountId}/{period}")]
        public async Task<IActionResult> GetBalances(int accountId, Period period)
        {
            var result = await _getBalancesService.GetBalances(accountId, period);

            return Ok(result);
        }
    }
}
