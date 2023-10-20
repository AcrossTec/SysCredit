namespace SysCredit.Api.Validations.Customers;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Validador que verifica si la Identificaciòn del Cliente es ùnica.
/// </summary>
/// <typeparam name="T">Tipo de Request a Validar.
/// </typeparam>
public class AsyncCustomerUniqueIdentificationValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    ///     Valida si el Cliente Tiene Una Identificación Unica
    /// </summary>
    /// <param name="Context">Contiene informaciòn del Request que se va a Validar.
    /// </param>
    /// <param name="Identification">Identificaciòn del cliente que se va a Validar.
    /// </param>
    /// <param name="Cancellation">
    ///     Mètodo que detiene la Validaciòn.
    /// </param>
    /// <returns>
    ///     Retorna true si la identificación es ùnica.
    /// </returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Identification, CancellationToken Cancellation)
    {
        var Customer = await Context.RootContextData[nameof(CustomerStore)].AsStore<Customer>().FetchCustomerByIdentificationAsync(Identification);
        return Customer is null;
    }

    /// <summary>
    ///     Lanza el error del validador.
    /// </summary>
    /// <param name="ErrorCode">Retorna el mensaje de error.
    /// </param>
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
    public override string Name => "AsyncCustomerUniqueIdentificationValidator";
}
