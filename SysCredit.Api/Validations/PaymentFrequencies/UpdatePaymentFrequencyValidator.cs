namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Constants;
using SysCredit.Api.Requests.PaymentFrequencies;

public class UpdatePaymentFrequencyValidator : AbstractValidator<UpdatePaymentFrequencyRequest>
{
    public UpdatePaymentFrequencyValidator()
    {
        RuleFor(Upf => Upf.Name)
            .NotEmpty()
            .NotNull()
            .PaymentFrequencyUniqueNameAsync().WithErrorCode(ErrorCodes.SERVPF0101)
            .WithName("Nombre");

        RuleFor(Upf => Upf.PaymentFrequencyId)
            .NotEmpty()
            .NotNull()
            .VerifyRouteWithPaymentFrequencyId().WithErrorCode(ErrorCodes.SERVPF0102)
            .WithName("Id de la Frecuencia de Pago");
    }
}
