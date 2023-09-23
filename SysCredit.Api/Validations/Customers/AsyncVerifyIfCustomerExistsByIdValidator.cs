namespace SysCredit.Api.Validations.Customers;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Validador que verifica si un cliente existe
/// </summary>
/// <typeparam name="T">Tipo del request que se va validar</typeparam>
public class AsyncVerifyIfCustomerExistsByIdValidator<T> : AsyncPropertyValidator<T, long?>
{
    /// <summary>
    ///     Valida si existe un cliente
    /// </summary>
    /// <param name="Context">Contiene información del Request que se está validando y datos compartidos</param>
    /// <param name="Identification">Id del cliente que se va a validar</param>
    /// <param name="Cancellation">Permite cancelar la validación en un proceso asincrono</param>
    /// <returns></returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long? Identification, CancellationToken Cancellation)
    {
        var Customer = await Context.RootContextData[nameof(CustomerStore)].AsStore<Customer>().FetchCustomerByIdAsync(Identification);
        return Customer is not null;
    }

    /// <summary>
    ///     Devuelve el mensaje de error si no encuentra un cliente
    /// </summary>
    /// <param name="ErrorCode">Codigo de error asignado</param>
    /// <returns>Retorna un mensaje de error</returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return ErrorCodeMessages.GetMessageFromCode(ErrorCode)!;
    }

    /// <summary>
    ///     Nombre unico del validador
    /// </summary>
    public override string Name => "AsyncVerifyIfCustomerExistsByIdValidator";
}
