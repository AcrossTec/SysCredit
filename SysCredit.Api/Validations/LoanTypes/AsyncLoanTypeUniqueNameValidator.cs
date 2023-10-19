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
///     Clase validadora del nombre del Request para crear un tipo de préstamo.
/// </summary>
/// <typeparam name="T">Tipo del store</typeparam>
public class AsyncLoanTypeUniqueNameValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    ///     Valida que el nombre sea único.
    /// </summary>
    /// <param name="Context">
    ///     Obtiene el objeto donde fue usado el validador.
    /// </param>
    /// <param name="Name">
    ///     Nombre del tipo de préstamo.
    /// </param>
    /// <param name="Cancellation">
    ///     Método para cancelar la validación.
    /// </param>
    /// <returns>
    ///     Retorna true si el LoanType no existe.
    /// </returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Name, CancellationToken Cancellation)
    {
        var LoanType = await Context.RootContextData[nameof(LoanTypeStore)].AsStore<LoanType>().FetchLoanTypeByName(Name);
        return LoanType is null;
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
    public override string Name => "AsyncLoanTypeUniqueNameValidator";
}
