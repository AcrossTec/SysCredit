namespace SysCredit.Api.Validations.Customers;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Validador que verifica si la Identificacion del Cliente es Unica.
/// </summary>
/// <typeparam name="T">Tipo de Request a Validar</typeparam>
public class AsyncCustomerUniqueIdentificationValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    ///     Valida si el Cliente Tiene Una Identificación Unica
    /// </summary>
    /// <param name="Context">Contiene informacion del Request que se va a Validar</param>
    /// <param name="Identification">Identificacipn del Cliente que se va a Validar </param>
    /// <param name="Cancellation">Metodo que detiene la Validacion  </param>
    /// <returns>Retorna Un valor booleano que indica si el cliente tiene una identificacion unica</returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Identification, CancellationToken Cancellation)
    {
        var Customer = await Context.RootContextData[nameof(CustomerStore)].AsStore<Customer>().FetchCustomerByIdentificationAsync(Identification);
        return Customer is null;
    }

    /// <summary>
    ///     Retorna el mensaje de Error
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
    public override string Name => "AsyncCustomerUniqueIdentificationValidator";
}
