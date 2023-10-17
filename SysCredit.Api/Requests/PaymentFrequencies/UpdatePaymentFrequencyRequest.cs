namespace SysCredit.Api.Requests.PaymentFrequencies;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.PaymentFrequencies;

/// <summary>
///     Request para actualizar una frequencia de pago
/// </summary>
[Validator<UpdatePaymentFrequencyValidator>]
public class UpdatePaymentFrequencyRequest : IRequest
{
    public long? PaymentFrequencyId { get; set; }

    public string Name { get; set; } = String.Empty;
}
