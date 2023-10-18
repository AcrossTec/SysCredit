namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;
using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Clase validadora de la referencia del tipo de prestamo
/// </summary>
/// <typeparam name="T">Tipo del store</typeparam>
public class AsyncVerifyLoanTypeReferenceValidator<T> : AsyncPropertyValidator<T, long?>
{
    /// <summary>
    ///     Valida que la referencia exista
    /// </summary>
    /// <param name="Context">Obtiene el objeto donde fue usado el validador</param>
    /// <param name="LoanTypeId">Id del tipo de prestamo</param>
    /// <param name="Cancellation">Método para cancelar la validación</param>
    /// <returns>Retorna un booleano indicando el estado de la validación</returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long? LoanTypeId, CancellationToken Cancellation)
    {
        return (await Context.RootContextData[nameof(LoanTypeStore)].AsStore<LoanType>().VerifyLoanTypeReference(LoanTypeId)) is false;
    }

    /// <summary>
    ///     Retorna el mensaje de error
    /// </summary>
    /// <param name="ErrorCode">Codigo del mensaje de error</param>
    /// <returns>Retorna un string indicando el mensaje de error</returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' esta siendo usado en otros registros";
    }

    /// <summary>
    ///     Nombre del validador
    /// </summary>
    public override string Name => "AsyncVerifyLoanTypeReferenceValidator";
}