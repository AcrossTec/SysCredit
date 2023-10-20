namespace SysCredit.Api.Requests.Authentications.Users;

/// <summary>
///     Clase que representa una solicitud para restablecer una contraseña olvidada.
/// </summary>
public class ForgotPasswordRequest : IRequest
{
    /// <summary>
    ///     Obtiene o establece la dirección de correo electrónico asociada a la solicitud de restablecimiento de contraseña.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}