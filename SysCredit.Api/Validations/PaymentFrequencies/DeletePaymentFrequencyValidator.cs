namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.PaymentFrequencies;
using SysCredit.Api.Constants;
/// <summary>
///     Clase validadora de <see cref="DeletePaymentFrequencyRequest"/>.
/// </summary>
public class DeletePaymentFrequencyValidator : AbstractValidator<DeletePaymentFrequencyRequest>
{
    /// <summary>
    ///     Valida el Id de la Frecuencia de pago.
    /// </summary>
    public DeletePaymentFrequencyValidator()
    {
        RuleFor(Pf => Pf.PaymentFrequencyId)
            .VeryfyIfExistsLoanByPaymentFrequencyIdAsync().WithErrorCode(ErrorCodes.SERVPF0103)
            .WithName("Frecuencia de pago");
    }
}
