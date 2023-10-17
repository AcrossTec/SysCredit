namespace SysCredit.Api.Requests.Authentications.Users;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Authentications.Users;

/// <summary>
///     Representa una solicitud de token de autenticación.
/// </summary>
[Validator<TokenRequestValidator>]
public class TokenRequest : IRequest
{
    /// <summary>
    ///     Obtiene o establece la dirección de correo electrónico del usuario que solicita el token.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    ///     Obtiene o establece la contraseña del usuario que solicita el token.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}