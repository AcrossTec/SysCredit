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
///     Clase validadora del nombre del Request para crear un tipo de prestamo
/// </summary>
/// <typeparam name="T">Tipo del store</typeparam>
public class AsyncLoanTypeUniqueNameValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    ///     Valida que el nombre sea unico
    /// </summary>
    /// <param name="Context">Obtiene el objeto donde fue usado el validador</param>
    /// <param name="Name">Nombre del tipo de prestamo</param>
    /// <param name="Cancellation">Metodo para cancelar la validación</param>
    /// <returns>Retorna un booleano indicando el estado de la validación</returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Name, CancellationToken Cancellation)
    {
        var LoanType = await Context.RootContextData[nameof(LoanTypeStore)].AsStore<LoanType>().FetchLoanTypeByName(Name);
        return LoanType is null;
    }

    /// <summary>
    ///     Retorna el mensaje de error
    /// </summary>
    /// <param name="ErrorCode">Codigo del mensaje de error</param>
    /// <returns></returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return ErrorCodeMessages.GetMessageFromCode(ErrorCode)!;
    }

    /// <summary>
    ///     Nombre del validador
    /// </summary>
    public override string Name => "AsyncLoanTypeUniqueNameValidator";
}
