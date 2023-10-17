namespace SysCredit.Api.Requests.Authentications.Roles;

/// <summary>
///     Solicita la actualizacion de rol de Usuario
/// </summary>
public class UpdateRoleUserRequest : IRequest
{
    /// <summary>
    ///     Id del rol que viene de la url
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    ///     RoleId es un Array de objetos de AssignTypeRequest.
    ///     Se establece como una propiedad  auto-implementada y se inicializa como un Array Vacio.
    /// </summary>
    public AssignTypeRequest[] RoleId { get; set; } = Array.Empty<AssignTypeRequest>();
}