namespace SysCredit.Api.Validations.Authentications.Users;

using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;
using SysCredit.Models;

/// <summary>
///     Clase Validadora del Request para saber si el Email es unico.
/// </summary>
/// <typeparam name="T"></typeparam>
public class AsyncUserUniqueEmailValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    ///     Valida si el correo es único por usuario.
    /// </summary>
    /// <param name="Context">Obtiene el objeto donde fue usado el validador. </param>
    /// <param name="Value">Valor del correo unico por usuario. </param>
    /// <param name="Cancellation">Metdo para detener la validacion. </param>
    /// <returns>Retorna Un objeto Task que representa la operación asincrónica con un valor booleano que indica si el correo es único.</returns>
    public override Task<bool> IsValidAsync(FluentValidation.ValidationContext<T> Context, string? Value, CancellationToken Cancellation)
    {
        return Task.FromResult(false);
    }

    /// <summary>
    ///     Retorna el mensaje de Error.
    /// </summary>
    /// <param name="ErrorCode">Codigo del mensaje de Error</param>
    /// <returns>Retorna una cadena de texto que contiene el mensaje de error.</returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Ya existe un registro con este valor.";
    }

    /// <summary>
    ///     Nombre del Validador
    /// </summary>
    public override string Name => "AsyncUserUniqueEmailValidator";
}
