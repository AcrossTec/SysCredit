﻿namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.PaymentFrequencies;

/// <summary>
///     Clase validadora de <see cref="UpdatePaymentFrequencyRequest"/>
/// </summary>
public class UpdatePaymentFrequencyValidator : AbstractValidator<UpdatePaymentFrequencyRequest>
{
    /// <summary>
    ///     Valida tanto el nombre como el Id de la Frecuencia de pago
    /// </summary>
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
