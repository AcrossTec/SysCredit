namespace SysCredit.Api.Requests.Authentications;

/// <summary>
///     Clase que representa una solicitud de asignación de tipo.
///     Esta solicitud se utiliza para asignar un nombre de rol a una entidad.
/// </summary>
public class AssignTypeRequest : IRequest
{
    /// <summary>
    ///     Obtiene o establece el nombre del rol que se asignará.
    /// </summary>
    public string RoleName { get; set; } = string.Empty;
}
