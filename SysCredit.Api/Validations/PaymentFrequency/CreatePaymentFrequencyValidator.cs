using FluentValidation;
using SysCredit.Api.Extensions;
using SysCredit.Api.ViewModels.PaymentFrequencys;

namespace SysCredit.Api.Validations.PaymentFrequency;

public class CreatePaymentFrequencyValidator : AbstractValidator<CreatePaymentFrequencyRequest>
{
    public CreatePaymentFrequencyValidator()
    {
        RuleFor(T => T.Name)
            .NotEmpty()
            .NotNull()
            .PaymentFrequencyUniqueNameAsync()
            .WithName("Nombre");
    }
}