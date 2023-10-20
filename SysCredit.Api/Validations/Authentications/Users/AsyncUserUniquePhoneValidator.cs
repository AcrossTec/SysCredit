namespace SysCredit.Api.Validations.Authentications.Users;

using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;
using SysCredit.Models;

/// <summary>
///     Clase Validador del Numero del Request para Crear un usuario.
/// </summary>
/// <typeparam name="T">Tipo del Store</typeparam>
public class AsyncUserUniquePhoneValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    ///     Valida que el Usuario tenga Un numero unico de telefono.
    /// </summary>
    /// <param name="Context">Obtiene el objeto donde fue usado el validador</param>
    /// <param name="Value">Valor del Numero unico por usuario</param>
    /// <param name="Cancellation">Metdo para detener la validacion</param>
    /// <returns>Retorna Un valor booleano que indica si la validación es exitosa o no.</returns>
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
    public override string Name => "AsyncUserUniquePhoneValidator";
}
