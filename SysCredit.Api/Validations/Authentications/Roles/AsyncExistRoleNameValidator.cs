namespace SysCredit.Api.Validations.Authentications.Roles;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;
using SysCredit.Models;

/// <summary>
///     Clase Validadora del Request que Revisa si Existe El Nombre del Rol.
/// </summary>
/// <typeparam name="T"></typeparam>
public class AsyncExistRoleNameValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    ///     Valida si el Nombre Existe.
    /// </summary>
    /// <param name="Context">Obtine el objeto donde fue usado el Validado</param>
    /// <param name="Value">Valor del Tipo de Asignacion</param>
    /// <param name="Cancellation">Metodo para cancelar la Validacion</param>
    /// <returns>Retorna Un valor booleano que indica si el nombre ya existe en el contexto.</returns>
    public override Task<bool> IsValidAsync(ValidationContext<T> Context, string? Value, CancellationToken Cancellation)
    {
        return Task.FromResult(false);
    }

    /// <summary>
    ///     Retorna el mensaje de Error.
    /// </summary>
    /// <param name="ErrorCode">Codigo del mensaje de Error. </param>
    /// <returns>Retorna una cadena de texto que contiene el mensaje de error.</returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' 'RoleName' no debe existir. y debe ser único";
    }

    /// <summary>
    ///     Nombre del Validador.
    /// </summary>
    public override string Name => "AsyncExistRoleNameValidator";
}
