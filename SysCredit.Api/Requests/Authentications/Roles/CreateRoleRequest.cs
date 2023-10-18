namespace SysCredit.Api.Requests.Authentications.Roles;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Authentications.Roles;

/// <summary>
///     Crea una solicitud rol.
/// </summary>
[Validator<CreateRoleValidator>]
public class CreateRoleRequest : IRequest
{
    /// <summary>
    ///     Nombre del rol que viene de la Url.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     La propiedad RoleClaims es un arreglo de objetos CreateClaimRequest.
    ///     Se establece como una propiedad auto-implementada y se inicializa como un arreglo vacío.  
    /// </summary>
    public CreateClaimRequest[] RoleClaims { get; set; } = Array.Empty<CreateClaimRequest>();
}
