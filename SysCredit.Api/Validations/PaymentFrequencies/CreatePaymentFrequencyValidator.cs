namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.PaymentFrequencies;


public class CreatePaymentFrequencyValidator : AbstractValidator<CreatePaymentFrequencyRequest>
{
    public CreatePaymentFrequencyValidator()
    {
        RuleFor(T => T.Name)
            .NotEmpty()
            .NotNull()
            .PaymentFrequencyUniqueNameAsync().WithErrorCode(ErrorCodes.SERVPF0101)
            .WithName("Nombre");
    }
}