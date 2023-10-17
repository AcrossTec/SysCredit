namespace SysCredit.Api.Validations.Authentications.Roles;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Authentications;
using SysCredit.Api.Stores;
using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Clase validadora del Nombre del Request para Crear un Rol.
/// </summary>
/// <typeparam name="T">Tipo del Store</typeparam>
public class AsyncExistRoleValidator<T> : AsyncPropertyValidator<T, IEnumerable<AssignTypeRequest>>
{
    /// <summary>
    ///     Valida que el nombre sea Unico.
    /// </summary>
    /// <param name="Context">Obtine el objeto donde fue usado el Validador</param>
    /// <param name="Value">Valor del Tipo de Asignacion</param>
    /// <param name="Cancellation"> Metodo para camcelar la Validacion</param>
    /// <returns>Retorna Un valor booleano que indica si la validación fue exitosa o no.</returns>
    public override Task<bool> IsValidAsync(ValidationContext<T> Context, IEnumerable<AssignTypeRequest> Value, CancellationToken Cancellation)
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
        return "'{PropertyName}' Solicitud con registros inexistentes o duplicados: 'RoleId' debe existir. y debe ser único";
    }
    
    /// <summary>
    ///     Nombre del Validador.
    /// </summary>
    public override string Name => "UserRolesUniqueRequestValidator";
}