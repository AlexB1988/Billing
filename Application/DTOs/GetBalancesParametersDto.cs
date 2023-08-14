using Billing.Application.Services;

namespace Billing.Application.DTOs
{
    public class GetBalancesParametersDto
    {
        public int AccountId { get; set; }
        public Period Period { get; set; }
    }
}
