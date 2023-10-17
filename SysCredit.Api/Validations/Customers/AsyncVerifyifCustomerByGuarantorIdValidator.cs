namespace SysCredit.Api.Validations.Customers;

using FluentValidation;
using FluentValidation.Validators;
using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Stores;
using SysCredit.Models;

/// <summary>
///     Validador que verifica si un Customer Usa un fiador.
/// </summary>
/// <typeparam name="T"> Tipo del Request que se Va a validar</typeparam>
public class AsyncVerifyifCustomerByGuarantorIdValidator <T> : AsyncPropertyValidator<T ,long?>
{
    /// <summary>
    ///     Valida si El Cliente tiene en uso un Fiador
    /// </summary>
    /// <param name="Context">Contiene información del Request Que se esta validando. </param>
    /// <param name="GuarantorId"> Id del Cliente que se va a Validar </param>
    /// <param name="Cancellation"> Metodo que detiene la Validacion</param>
    /// <returns>Devuelve `true` si el Cliente no tiene un Fiador asignado, de lo contrario, devuelve `false`.</returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long? GuarantorId, CancellationToken Cancellation)
    {
        var Customer = await Context.RootContextData[nameof(CustomerStore)].AsStore<Customer>().FetchCustomerByGuarantorIdAsync(GuarantorId);
        return Customer is null;
    }
    /// <summary>
    ///     Retorna la el mensaje de Error.
    /// </summary>
    /// <param name="ErrorCode">Codigo de Error adignado</param>
    /// <returns>Retorna una cadena de texto que contiene el mensaje de error.</returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return base.GetDefaultMessageTemplate(ErrorCode);
    }

    /// <summary>
    ///     Nombre uníco del Valdiador.
    /// </summary>
    public override string Name => " AsyncVerifyifCustomerByGuarantorIdValidator ";
}