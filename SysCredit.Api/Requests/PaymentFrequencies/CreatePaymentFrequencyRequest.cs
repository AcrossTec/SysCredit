namespace SysCredit.Api.Requests.PaymentFrequencies;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.PaymentFrequencies;

[Validator<CreatePaymentFrequencyValidator>]
public class CreatePaymentFrequencyRequest : IRequest
{
    public string Name { get; set; } = string.Empty;
}