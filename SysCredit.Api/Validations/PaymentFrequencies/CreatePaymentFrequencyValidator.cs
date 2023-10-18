namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.PaymentFrequencies;
using System.Configuration;

/// <summary>
///     Clase validadora de <see cref="CreatePaymentFrequencyRequest"/>
/// </summary>
public class CreatePaymentFrequencyValidator : AbstractValidator<CreatePaymentFrequencyRequest>
{
    /// <summary>
    ///     Valida el nombre de la Frecuencia de pago
    /// </summary>
    public CreatePaymentFrequencyValidator()
    {
        RuleFor(T => T.Name)
            .NotEmpty()
            .NotNull()
            .PaymentFrequencyUniqueNameAsync()
            .WithName("Nombre");
    }

}
