namespace SysCredit.Api.Requests.PaymentFrequencies;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.PaymentFrequencies;

/// <summary>
///     Request para borrar una frecuencia de pago que no está siendo usada por un préstamo.
/// </summary>
/// <param name="PaymentFrequencyId">
///     Propiedad que representa el Id de la frecuencia de pago.
/// </param>
[Validator<DeletePaymentFrequencyValidator>]
public record DeletePaymentFrequencyRequest(long PaymentFrequencyId) : IRequest;
