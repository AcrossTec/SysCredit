namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;
using SysCredit.Api.Extensions;
using SysCredit.Api.ViewModels.PaymentFrequencies;


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