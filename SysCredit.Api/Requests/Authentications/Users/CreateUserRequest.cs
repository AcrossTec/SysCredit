namespace SysCredit.Api.Requests.Authentications.Users;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Authentications.Users;

/// <summary>
///     Clase que representa una solicitud para crear un usuario.
/// </summary>
[Validator<CreateUserValidator>]
public class CreateUserRequest : IRequest
{
    /// <summary>
    ///     Obtiene o establece el nombre de usuario del nuevo usuario.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    ///     Obtiene o establece la dirección de correo electrónico del nuevo usuario.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    ///     Obtiene o establece la contraseña del nuevo usuario.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    ///     Obtiene o establece el número de teléfono del nuevo usuario.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    ///     Obtiene o establece los roles asignados al nuevo usuario.
    /// </summary>
    public AssignTypeRequest[] Roles { get; set; } = Array.Empty<AssignTypeRequest>();

    /// <summary>
    ///     Obtiene o establece las reclamaciones del nuevo usuario.
    /// </summary>
    public CreateClaimRequest[] UserClaims { get; set; } = Array.Empty<CreateClaimRequest>();

}