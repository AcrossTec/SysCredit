namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Clase que verifica el Id de la frecuencia de pago de la ruta
/// </summary>
/// <typeparam name="T">Tipo del store</typeparam>
public class VerifyRouteWithPaymentFrequencyIdValidator<T> : PropertyValidator<T, long?>
{
    /// <summary>
    ///     Valida que el Id de la frecuencia de pago de la ruta coincida con el Id de la frecuencia de pago
    /// </summary>
    /// <param name="Context">Obtiene el objeto donde fue usado el validador</param>
    /// <param name="PaymentFrequencyId">Id de la Frecuencia de pago</param>
    /// <returns>Retorna un booleano indicando el estado de la validación</returns>
    public override bool IsValid(ValidationContext<T> Context, long? PaymentFrequencyId)
    {
        // Obtener el PaymentFrequencyId de la ruta
        var RoutePaymentFrequencyId = Context.RootContextData["RoutePaymentFrequencyId"].As<long?>();

        // Verificar si coinciden
        return RoutePaymentFrequencyId == PaymentFrequencyId;
    }

    /// <summary>
    ///     Retorna el mensaje de error
    /// </summary>
    /// <param name="ErrorCode">Codigo del mensaje de error</param>
    /// <returns></returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "El '{PropertyName}' en la ruta no coincide con el Id proporcionado en el cuerpo de la solicitud.";
    }

    /// <summary>
    ///     Nombre del validador
    /// </summary>
    public override string Name => "VerifyRouteWithPaymentFrequencyIdValidator";
}
