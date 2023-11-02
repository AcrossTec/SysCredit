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
///     Validador que verifica si el Cliente tiene un ùnico Nùmero de Telèfono.
/// </summary>
/// <typeparam name="T">Tipo de Request a Validar
/// </typeparam>
public class AsyncCustomerUniquePhoneValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    ///     Valida si el Cliente tiene Un nuemero Unico.
    /// </summary>
    /// <param name="Context"> Contiene la Informaciòn del Request que se esta Valdidado.
    /// </param>
    /// <param name="Phone">Nùmero del Cliente se va a Validar.
    /// </param>
    /// <param name="Cancellation">Mètodo que detiene la Validaciòn.
    /// </param>
    /// <returns>Retorna true si el numero de telefono es unico.
    /// </returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Phone, CancellationToken Cancellation)
    {
        var Customer = await Context.RootContextData[nameof(CustomerStore)].AsStore<Customer>().FetchCustomerByPhoneAsync(Phone);
        return Customer is null;
    }

    /// <summary>
    ///     Lanza el error del validador.
    /// </summary>
    /// <param name="ErrorCode">Codigo de Error Asignado</param>
    /// <returns>Retorna una cadena de texto que contiene el mensaje de error.</returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return ErrorCodeMessages.GetMessageFromCode(ErrorCode)!;
    }

    /// <summary>
    ///     Lanza el error del validador.
    /// </summary>
    public override string Name => "AsyncCustomerUniquePhoneValidator";
}
