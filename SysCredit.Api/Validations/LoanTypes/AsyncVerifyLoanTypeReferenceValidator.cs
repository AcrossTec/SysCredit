namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Stores;
using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Clase validadora para verificar que el tipo de préstamo no esté siendo usado por un préstamo.
/// </summary>
/// <typeparam name="T">
///     Tipo del store.
/// </typeparam>
public class AsyncVerifyLoanTypeReferenceValidator<T> : AsyncPropertyValidator<T, long?>
{
    /// <summary>
    ///     Valida que la referencia exista.
    /// </summary>
    /// <param name="Context">
    ///     Obtiene el objeto donde fue usado el validador.
    /// </param>
    /// <param name="LoanTypeId">
    ///     Id del tipo de préstamo.
    /// </param>
    /// <param name="Cancellation">
    ///     Método para cancelar la validación.
    /// </param>
    /// <returns>
    ///     Retorna true si el tipo de préstamo no está siendo utilizado.
    /// </returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long? LoanTypeId, CancellationToken Cancellation)
    {
        return (await Context.RootContextData[nameof(LoanTypeStore)].AsStore<LoanType>().VerifyLoanTypeReference(LoanTypeId)) is false;
    }

    /// <summary>
    ///     Lanza el error del validador.
    /// </summary>
    /// <param name="ErrorCode">
    ///     Código del mensaje de error.
    /// </param>
    /// <returns>
    ///     Retorna el mensaje de error.
    /// </returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return ErrorCodeMessages.GetMessageFromCode(ErrorCode)!;
    }

    /// <summary>
    ///     Nombre del validador.
    /// </summary>
    public override string Name => "AsyncVerifyLoanTypeReferenceValidator";
}