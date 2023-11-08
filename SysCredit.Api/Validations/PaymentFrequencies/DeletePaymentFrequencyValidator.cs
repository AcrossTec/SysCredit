namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.PaymentFrequencies;
using static SysCredit.Api.Properties.ErrorCodeMessages;
using static SysCredit.Api.Constants.ErrorCodes;
using static SysCredit.Api.Properties.SysCreditMessages;

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
            .NotNull().WithErrorCode(SERVPF0105).WithMessage(GetMessageFromCode(SERVPF0105))
            .WithName(GetMessage("PaymentFrequencyId"));
    }
}
