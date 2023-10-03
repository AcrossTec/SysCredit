namespace SysCredit.Api.Validations.PaymentPlans;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.PaymentPlans;

using static Constants.ErrorCodes;

public class PaymentPlanIdValidator : AbstractValidator<PaymentPlanIdRequest>
{
    public PaymentPlanIdValidator()
    {
        RuleFor(PP => PP.PaymentPlanId)
            .NotNull()
            .NotEmpty()
            .VerifyIfExistPaymentPlanByIdAsync().WithErrorCode(DATAPP0501)
            .WithName("Id de Plan de Pago");
    }
}
