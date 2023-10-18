namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Constants;
using SysCredit.Api.Requests.PaymentFrequencies;

/// <summary>
///     Clase validadora de <see cref="UpdatePaymentFrequencyRequest"/>.
/// </summary>
public class UpdatePaymentFrequencyValidator : AbstractValidator<UpdatePaymentFrequencyRequest>
{
    /// <summary>
    ///     Valida el nombre y el Id de la Frecuencia de pago.
    /// </summary>
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
