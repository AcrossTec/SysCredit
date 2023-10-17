namespace SysCredit.Api.Requests.PaymentFrequencies;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.PaymentFrequencies;

/// <summary>
///     Request para borrar una frequencia de pago
/// </summary>
[Validator<DeletePaymentFrequencyValidator>]
public class DeletePaymentFrequencyRequest : IRequest
{
    public long PaymentFrequencyId { get; set; }
}
