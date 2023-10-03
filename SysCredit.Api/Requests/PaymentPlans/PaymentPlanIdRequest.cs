namespace SysCredit.Api.Requests.PaymentPlans;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.PaymentPlans;

[Validator<PaymentPlanIdValidator>]
public class PaymentPlanIdRequest : IRequest
{
    public long? PaymentPlanId { get; set; }
}
