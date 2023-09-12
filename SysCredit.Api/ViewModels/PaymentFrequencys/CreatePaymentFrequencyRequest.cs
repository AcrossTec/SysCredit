namespace SysCredit.Api.ViewModels.PaymentFrequencys;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.PaymentFrequency;

[Validator<CreatePaymentFrequencyValidator>]
public class CreatePaymentFrequencyRequest : IViewModel
{   
    public string Name { get; set; } = string.Empty;
}