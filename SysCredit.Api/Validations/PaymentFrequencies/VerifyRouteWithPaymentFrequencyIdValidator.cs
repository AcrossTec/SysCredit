namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

public class VerifyRouteWithPaymentFrequencyIdValidator<T> : PropertyValidator<T, long?>
{
    public override bool IsValid(ValidationContext<T> Context, long? PaymentFrequencyId)
    {
        // Obtener el PaymentFrequencyId de la ruta
        var RoutePaymentFrequencyId = Context.RootContextData["RoutePaymentFrequencyId"].As<long?>();

        // Verificar si coinciden
        return RoutePaymentFrequencyId == PaymentFrequencyId;
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return ErrorCodeMessages.GetMessageFromCode(ErrorCode)!;
    }

    public override string Name => "VerifyRouteWithPaymentFrequencyIdValidator";
}
