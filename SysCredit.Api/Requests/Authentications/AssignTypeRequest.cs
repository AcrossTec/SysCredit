namespace SysCredit.Api.Requests.Authentications;

/// <summary>
///     Request del tipo de asignación.
/// </summary>
public class AssignTypeRequest : IRequest
{
    /// <summary>
    ///     Nombre del tipo de Asignación que se espera de la url.
    /// </summary>
    public string RoleName { get; set; } = string.Empty;
}
