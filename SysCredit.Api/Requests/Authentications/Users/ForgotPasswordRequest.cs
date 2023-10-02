namespace SysCredit.Api.Requests.Authentications.Users;

public class ForgotPasswordRequest : IRequest
{
    public string Email { get; set; } = string.Empty;
}