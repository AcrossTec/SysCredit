namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;

/// <summary>
///     Clase que verifica el Id de la frecuencia de pago de la ruta.
/// </summary>
/// <typeparam name="T">
///     Tipo del store.
/// </typeparam>
public class VerifyRouteWithPaymentFrequencyIdValidator<T> : PropertyValidator<T, long?>
{
    /// <summary>
    ///     Valida que el Id de la frecuencia de pago de la ruta coincida con el Id de la frecuencia de pago.
    /// </summary>
    /// <param name="Context">
    ///     Obtiene el objeto donde fue usado el validador.
    /// </param>
    /// <param name="PaymentFrequencyId">
    ///     Id de la Frecuencia de pago.
    /// </param>
    /// <returns>
    ///     Retorna un true si ambos si el Id del route es igual al Id del body.
    /// </returns>
    public override bool IsValid(ValidationContext<T> Context, long? PaymentFrequencyId)
    {
        // Obtener el PaymentFrequencyId de la ruta
        var RoutePaymentFrequencyId = Context.RootContextData["RoutePaymentFrequencyId"].As<long?>();

        // Verificar si coinciden
        return RoutePaymentFrequencyId == PaymentFrequencyId;
    }

    /// <summary>
    ///     Lanza el error del validador.
    /// </summary>
    /// <param name="ErrorCode">
    ///     Código del mensaje de error.
    /// </param>
    /// <returns>
    ///     Retorna el mensaje de error.
    /// </returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return ErrorCodeMessages.GetMessageFromCode(ErrorCode)!;
    }

    /// <summary>
    ///     Nombre del validador.
    /// </summary>
    public override string Name => "VerifyRouteWithPaymentFrequencyIdValidator";
}
