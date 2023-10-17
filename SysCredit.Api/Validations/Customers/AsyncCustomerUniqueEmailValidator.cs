namespace SysCredit.Api.Validations.Customers;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Validador que verifica si Cliente tiene Un Email Unico.
/// </summary>
/// <typeparam name="T">Tipo de Store</typeparam>
public class AsyncCustomerUniqueEmailValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    ///     Verifica si Existe el Email en otros Clientes
    /// </summary>
    /// <param name="Context">Contiene Informacion del Request que se esta validado</param>
    /// <param name="Email">Id dek Cliente que se esta valdiadndo</param>
    /// <param name="Cancellation">Metodp que permite detener la Validacion </param>
    /// <returns>Retorna Un valor booleano que indica si el cliente tiene un Email unico</returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Email, CancellationToken Cancellation)
    {
        var Customer = await Context.RootContextData[nameof(CustomerStore)].AsStore<Customer>().FetchCustomerByEmailAsync(Email);
        return Customer is null;
    }

    /// <summary>
    ///     Retorna El mensaje de Error.
    /// </summary>
    /// <param name="ErrorCode">Codigo de Error asignado </param>
    /// <returns>Retorna una cadena de texto que contiene el mensaje de error.</returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Ya existe un registro con este valor.";
    }

    /// <summary>
    ///     Nombre del Validador.
    /// </summary>
    public override string Name => "AsyncCustomerUniqueEmailValidator";
}
