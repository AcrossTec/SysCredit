namespace SysCredit.Api.Requests.PaymentFrequencies;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.PaymentFrequencies;

/// <summary>
///     Request para actualizar una frecuencia de pago con un nombre unico.
/// </summary>
[Validator<UpdatePaymentFrequencyValidator>]
public class UpdatePaymentFrequencyRequest : IRequest
{
    /// <summary>
    ///     Propiedad que representa el Id de la frecuencia de pago.
    /// </summary>
    public long? PaymentFrequencyId { get; set; }
    
    /// <summary>
    ///     Propiedad que representa el nombre de la frecuencia de pago.
    /// </summary>
    public string Name { get; set; } = String.Empty;
}
