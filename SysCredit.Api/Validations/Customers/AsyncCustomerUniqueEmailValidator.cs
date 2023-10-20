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
/// 
/// <typeparam name="T">Tipo de Store</typeparam>
public class AsyncCustomerUniqueEmailValidator<T> : AsyncPropertyValidator<T, string?>
{

    /// <summary>
    ///     Verifica si existe el email en otros clientes.
    /// </summary>
    /// <param name="Context">
    ///     Contiene Informaciôn del Request que se esta validado.
    ///     </param>
    /// <param name="Email">Id de cliente que se esta validado.
    /// </param>
    /// <param name="Cancellation">
    /// Metodo que permite detener la validacion 
    /// </param>
    /// <returns>
    ///     Retorna true si el email del cliente es unico.
    /// </returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Email, CancellationToken Cancellation)
    {
        var Customer = await Context.RootContextData[nameof(CustomerStore)].AsStore<Customer>().FetchCustomerByEmailAsync(Email);
        return Customer is null;
    }

    /// <summary>
    ///     Lanza el error del validador.
    /// </summary>
    /// <param name="ErrorCode">
    ///     Codigo de Error asignado </param>
    /// <returns>
    ///     Retorna el mensaje de error.
    /// </returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Ya existe un registro con este valor.";
    }

    /// <summary>
    ///     Nombre del Validador.
    /// </summary>
    public override string Name => "AsyncCustomerUniqueEmailValidator";
}
