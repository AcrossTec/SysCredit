namespace SysCredit.Api.ViewModels.PaymentFrequencies;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.PaymentFrequencies;

[Validator<CreatePaymentFrequencyValidator>]
public class CreatePaymentFrequencyRequest : IViewModel
{
    public string Name { get; set; } = string.Empty;
}