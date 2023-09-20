namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.PaymentFrequencies;

public class UpdatePaymentFrequencyValidator : AbstractValidator<UpdatePaymentFrequencyRequest>
{
    public UpdatePaymentFrequencyValidator()
    {
        RuleFor(Upf => Upf.Name)
            .NotEmpty()
            .NotNull()
            .PaymentFrequencyUniqueNameAsync()
            .WithName("Nombre");

        RuleFor(Upf => Upf.PaymentFrequencyId)
            .NotEmpty()
            .NotNull()
            .VerifyRouteWithPaymentFrequencyId()
            .WithName("Id de la Frecuencia de Pago");
    }
}
