namespace SysCredit.Api.Requests.PaymentFrequencies;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.PaymentFrequencies;

[Validator<DeletePaymentFrequencyValidator>]
public class DeletePaymentFrequencyRequest : IRequest
{
    public long PaymentFrequencyId { get; set; }
}
