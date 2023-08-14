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
        
        public BalancesController(IGetBalancesService getBalancesService) =>  _getBalancesService = getBalancesService;


        [HttpGet("GetBalances")]
        public async Task<IActionResult> GetBalances([FromQuery] GetBalancesParametersDto parametersDto)
        {
            var result = await _getBalancesService.GetBalances(parametersDto);

            return Ok(result);
        }
    }
}
