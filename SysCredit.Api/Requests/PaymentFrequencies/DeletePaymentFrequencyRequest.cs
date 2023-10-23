namespace SysCredit.Api.Requests.PaymentFrequencies;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.PaymentFrequencies;

/// <summary>
///     Request para borrar una frecuencia de pago que no está siendo usada por un préstamo.
/// </summary>
[Validator<DeletePaymentFrequencyValidator>]
public class DeletePaymentFrequencyRequest : IRequest
{
    /// <summary>
    ///     Propiedad que representa el Id de la frecuencia de pago.
    /// </summary>
    public long? PaymentFrequencyId { get; set; }
}