namespace SysCredit.Api.Requests.Authentications;

/// <summary>
///     Crea la solicitud de Reclamo.
/// </summary>
public class CreateClaimRequest : IRequest
{
    /// <summary>
    ///     Tipo de Reclamo que se recibe.
    /// </summary>
    public string ClaimType { get; set; } = string.Empty;

    /// <summary>
    ///     Valor de la Reclamacion que se recibe.
    /// </summary>
    public string ClaimValue { get; set; } = string.Empty;
}
