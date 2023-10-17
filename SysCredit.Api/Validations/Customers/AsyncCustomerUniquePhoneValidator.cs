namespace SysCredit.Api.Validations.Customers;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Validador que verifica si el Cliente tiene un Unico Numero de Telefono.
/// </summary>
/// <typeparam name="T">Tipo de Request a Validar</typeparam>
public class AsyncCustomerUniquePhoneValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    ///     Valida si el Cliente tiene Un nuemero Unico
    /// </summary>
    /// <param name="Context"> Contiene la Informacion del Request que se esta Valdidado</param>
    /// <param name="Phone">Numero del Cliente se va a Validar </param>
    /// <param name="Cancellation">Metodo que detiene la Validacion</param>
    /// <returns>Devuelve un valor booleano que indica si el número de teléfono es único (verdadero) o no (falso).</returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Phone, CancellationToken Cancellation)
    {
        var Customer = await Context.RootContextData[nameof(CustomerStore)].AsStore<Customer>().FetchCustomerByPhoneAsync(Phone);
        return Customer is null;
    }

    /// <summary>
    ///     Retorna el mensaje de Error.
    /// </summary>
    /// <param name="ErrorCode">Codigo de Error Asignado</param>
    /// <returns>Retorna una cadena de texto que contiene el mensaje de error.</returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Ya existe un registro con este valor.";
    }

    /// <summary>
    ///     Nombre del Validador
    /// </summary>
    public override string Name => "AsyncCustomerUniquePhoneValidator";
}
