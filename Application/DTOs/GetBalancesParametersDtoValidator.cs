using FluentValidation;

namespace Billing.Application.DTOs
{
    public class GetBalancesParametersDtoValidator : AbstractValidator<GetBalancesParametersDto>
    {
        public GetBalancesParametersDtoValidator()
        {
            RuleFor(x => x.AccountId).NotNull().WithMessage("Поле {PropertyName} не может быть пустым");
        }
    }
}
