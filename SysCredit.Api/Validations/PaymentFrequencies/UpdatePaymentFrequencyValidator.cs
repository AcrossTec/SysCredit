namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.ViewModels.PaymentFrequencies;

public class UpdatePaymentFrequencyValidator : AbstractValidator<UpdatePaymentFrequencyRequest>
{
    public UpdatePaymentFrequencyValidator() 
    {
        RuleFor(X => X.Name)
            .NotEmpty()
            .NotNull()
            .PaymentFrequencyUniqueNameAsync()
            .WithName("Nombre");

        RuleFor(X => X.PaymentFrequencyId)
            .NotEmpty()
            .NotNull()
            .WithName("Id de la Frecuencia de Pago");
    }
}
