namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Constants;
using SysCredit.Api.Requests.PaymentFrequencies;
using static SysCredit.Api.Properties.ErrorCodeMessages;
using static SysCredit.Api.Constants.ErrorCodes;
using static SysCredit.Api.Properties.SysCreditMessages;

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
            .NotEmpty().WithErrorCode(SERVPF0103).WithMessage(GetMessageFromCode(SERVPF0103))
            .NotNull().WithErrorCode(SERVPF0104).WithMessage(GetMessageFromCode(SERVPF0104))
            .PaymentFrequencyUniqueNameAsync().WithErrorCode(SERVPF0101).WithMessage(GetMessageFromCode(SERVPF0101))
            .WithName(GetMessage("Name"));

        RuleFor(Upf => Upf.PaymentFrequencyId)
            .NotEmpty().WithErrorCode(SERVPF0106).WithMessage(GetMessageFromCode(SERVPF0106))
            .NotNull().WithErrorCode(SERVPF0105).WithMessage(GetMessageFromCode(SERVPF0105))
            .VerifyRouteWithPaymentFrequencyId().WithErrorCode(SERVPF0102).WithMessage(GetMessageFromCode(SERVPF0102))
            .WithName(GetMessage("PaymentFrequencyId"));
    }
}
