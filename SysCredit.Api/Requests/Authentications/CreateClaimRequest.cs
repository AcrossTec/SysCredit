namespace SysCredit.Api.Requests.Authentications;

/// <summary>
///     Clase que representa una solicitud para crear una reclamación (claim).
/// </summary>
public class CreateClaimRequest : IRequest
{
    /// <summary>
    ///     Obtiene o establece el tipo de reclamación.
    /// </summary>
    public string ClaimType { get; set; } = string.Empty;

    /// <summary>
    ///     Obtiene o establece el valor de la reclamación.
    /// </summary>
    public string ClaimValue { get; set; } = string.Empty;
}
