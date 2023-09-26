namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.PaymentFrequencies;

public class DeletePaymentFrequencyValidator : AbstractValidator<DeletePaymentFrequencyRequest>
{
    public DeletePaymentFrequencyValidator()
    {
        RuleFor(Pf => Pf.PaymentFrequencyId)
            .NotNull()
            .WithName("Frecuencia de pago");
    }
}
