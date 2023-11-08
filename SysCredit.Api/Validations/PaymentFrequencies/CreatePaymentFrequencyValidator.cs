namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.PaymentFrequencies;
using System.Configuration;
using static SysCredit.Api.Properties.ErrorCodeMessages;
using static SysCredit.Api.Constants.ErrorCodes;
using static SysCredit.Api.Properties.SysCreditMessages;

/// <summary>
///     Clase validadora de <see cref="CreatePaymentFrequencyRequest"/>.
/// </summary>
public class CreatePaymentFrequencyValidator : AbstractValidator<CreatePaymentFrequencyRequest>
{
    /// <summary>
    ///     Valida el nombre de la Frecuencia de pago.
    /// </summary>
    public CreatePaymentFrequencyValidator()
    {
        RuleFor(T => T.Name)
            .NotEmpty().WithErrorCode(SERVPF0103).WithMessage(GetMessageFromCode(SERVPF0103))
            .NotNull().WithErrorCode(SERVPF0104).WithMessage(GetMessageFromCode(SERVPF0104))
            .PaymentFrequencyUniqueNameAsync().WithErrorCode(SERVPF0101).WithMessage(GetMessageFromCode(SERVPF0101))
            .WithName(GetMessage("Name"));
    }

}
