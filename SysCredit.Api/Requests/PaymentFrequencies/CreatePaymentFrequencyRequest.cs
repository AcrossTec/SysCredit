namespace SysCredit.Api.Requests.PaymentFrequencies;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.PaymentFrequencies;

/// <summary>
///     Request para crear una nueva frecuencia de pago
/// </summary>
[Validator<CreatePaymentFrequencyValidator>]
public class CreatePaymentFrequencyRequest : IRequest
{
    public string Name { get; set; } = string.Empty;
}