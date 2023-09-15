namespace SysCredit.Api.Requests.Auths.Users;

public class ForgotPasswordRequest : IRequest
{
    public string Email { get; set; } = string.Empty;
}