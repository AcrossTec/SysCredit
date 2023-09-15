namespace SysCredit.Api.Requests.PaymentFrequencies;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.PaymentFrequencies;

[Validator<UpdatePaymentFrequencyValidator>]
public class UpdatePaymentFrequencyRequest : IRequest
{
    public long? PaymentFrequencyId { get; set; }

    public string Name { get; set; } = String.Empty;
}
