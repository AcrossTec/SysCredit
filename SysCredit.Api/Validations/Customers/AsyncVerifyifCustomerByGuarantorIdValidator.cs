namespace SysCredit.Api.Validations.Customers;

using FluentValidation;
using FluentValidation.Validators;
using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Stores;
using SysCredit.Models;

/// <summary>
///     Validador que verifica si un cliente tiene un fiador..
/// </summary>
/// <typeparam name="T">Tipo del Request que se Va a validar.
/// </typeparam>
public class AsyncVerifyifCustomerByGuarantorIdValidator <T> : AsyncPropertyValidator<T ,long?>
{
    /// <summary>
    ///     Valida si El Cliente tiene en uso un Fiador
    /// </summary>
    /// <param name="Context">Contiene información del Request que se esta validando.
    /// </param>
    /// <param name="GuarantorId"> Id del cliente que se va a Validar.
    /// </param>
    /// <param name="Cancellation"> Mètodo que detiene la Validaciòn.
    /// </param>
    /// <returns>
    ///     Retorna true si el cliente no tiene un fiador por el Id del fiador.
    /// </returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long? GuarantorId, CancellationToken Cancellation)
    {
        var Customer = await Context.RootContextData[nameof(CustomerStore)].AsStore<Customer>().FetchCustomerByGuarantorIdAsync(GuarantorId);
        return Customer is null;
    }
    /// <summary>
    ///     Lanza el error del validador.
    /// </summary>
    /// <param name="ErrorCode">Codigo de Error asignado.
    /// </param>
    /// <returns>
    ///     Retorna el mensaje de error.
    /// </returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return ErrorCodeMessages.GetMessageFromCode(ErrorCode)!;
        //return base.GetDefaultMessageTemplate(ErrorCode);
    }

    /// <summary>
    ///     Nombre ùníco del Valdiador.
    /// </summary>
    public override string Name => " AsyncVerifyifCustomerByGuarantorIdValidator ";
}