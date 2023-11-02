namespace SysCredit.Api.Validations.Guarantors;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Clase validadora del Request para verificar la existencia de una relación del fiador por medio del Id.
/// </summary>
/// <typeparam name="T">
///     Tipo del Store
/// </typeparam>
public class AsyncExistsGuarantorValidator<T> : AsyncPropertyValidator<T, long>
{

    /// <summary>
    ///     Valida si existe una relación del fiador por medio del Id.
    /// </summary>
    /// <param name="Context">
    ///     Obtiene el objeto donde fue usado el validador.
    /// </param>
    /// <param name="GuarantorId">
    ///     El Id del fiador a validar.
    /// </param>
    /// <param name="Cancellation">
    ///     Método para cancelar la validación.
    /// </param>
    /// <returns>
    ///     Retorna true si la relación del fiador existe.
    /// </returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long GuarantorId, CancellationToken Cancellation)
    {
        return await Context.RootContextData[nameof(GuarantorStore)].AsStore<Guarantor>().ExistsGuarantorAsync(GuarantorId);
    }

    /// <summary>
    ///     En caso de error lanza el mensaje con el codigo de error.
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
    public override string Name => "AsyncExistsGuarantorValidator";
}
