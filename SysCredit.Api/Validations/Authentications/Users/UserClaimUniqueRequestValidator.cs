namespace SysCredit.Api.Validations.Authentications.Users;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Requests.Authentications;

/// <summary>
///     Clase Validadora del Reclamo del Request para el Usuario Unico.
/// </summary>
/// <typeparam name="T">Tipo del Request que se Va a validar</typeparam>
public class UserClaimUniqueRequestValidator<T> : PropertyValidator<T, IEnumerable<CreateClaimRequest>>
{
    /// <summary>
    ///     Valida que el Reclamo se Unico por Usuario
    /// </summary>
    /// <param name="Context"> Obtiene el Objeto donde fue usado el Validador</param>
    /// <param name="GuarantorRequests">una peticion de Fiadores.</param>
    /// <returns> Devuelve verdadero si todas las solicitudes de garantía son únicas por usuario, de lo contrario, falso. </returns>
    public override bool IsValid(ValidationContext<T> Context, IEnumerable<CreateClaimRequest> GuarantorRequests)
    {
        var Values = GuarantorRequests.DistinctBy(Request => Request.ClaimType.ToLower());
        return GuarantorRequests.Count() == Values.Count();
    }

    /// <summary>
    ///     Retorna el mensaje de Error.
    /// </summary>
    /// <param name="ErrorCode">Codigo del mensaje de Error</param>
    /// <returns>Retorna una cadena de texto que contiene el mensaje de error.</returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Solicitud con registros duplicados: 'ClaimType' debe ser único.";
    }

    /// <summary>
    ///     Nombre del Validador.
    /// </summary>
    public override string Name => "UserClaimUniqueRequestValidator";
}