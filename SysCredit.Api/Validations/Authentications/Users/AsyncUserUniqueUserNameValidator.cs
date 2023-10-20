namespace SysCredit.Api.Validations.Authentications.Users;

using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;
using SysCredit.Models;

/// <summary>
///     Clase Validadora del nombre del Request para Crear un Usuario.
/// </summary>
/// <typeparam name="T">Tipo del Store</typeparam>
public class AsyncUserUniqueUserNameValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    ///     Valida que el Usuario tenga un nombre Unico.
    /// </summary>
    /// <param name="Context"> Obtine el objeto donde fue usado el Validador. </param>
    /// <param name="Value"> Valor del Nombre unico por usuario. </param>
    /// <param name="Cancellation"> Metdo para detener la validacion. </param>
    /// <returns>Retorna Un valor booleano que indica si el cliente tiene nobre de usuario unico</returns>
    public override Task<bool> IsValidAsync(FluentValidation.ValidationContext<T> Context, string? Value, CancellationToken Cancellation)
    {
        
        return Task.FromResult(false);
    }

    /// <summary>
    ///     Retorna el mensaje de Error
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
    public override string Name => "AsyncUserUniqueUserNameValidator";
}