namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Clase validadora del nombre del request para crear una Frecuencia de pago
/// </summary>
/// <typeparam name="T">Tipo del store</typeparam>
public class AsyncPaymentFrequencyUniqueNameValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    ///     Valida que el nombre sea único
    /// </summary>
    /// <param name="Context">Obtiene el objeto donde fue usado el validador</param>
    /// <param name="Name">Nombre del tipo de Frecuencia de pago</param>
    /// <param name="Cancellation">Metodo para cancelar la validación</param>
    /// <returns>Retorna un booleano indicando el estado de la validación</returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Name, CancellationToken Cancellation)
    {
        var PaymentFrequency = await Context.RootContextData[nameof(PaymentFrequencyStore)].AsStore<PaymentFrequency>().FetchPaymentFrequencyByNameAsync(Name);
        return PaymentFrequency is null;
    }

    /// <summary>
    ///     Retorna el mensaje de error
    /// </summary>
    /// <param name="errorCode">Codigo del mensaje de error</param>
    /// <returns></returns>
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "'{PropertyName}'. Ya existe un registro con este nombre";
    }

    /// <summary>
    ///     Nombre del validador
    /// </summary>
    public override string Name => "AsyncPaymentFrequencyUniqueNameValidator";

}
