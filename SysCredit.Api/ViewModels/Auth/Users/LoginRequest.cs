namespace SysCredit.Api.ViewModels.Auth.Users;

public class LoginRequest : IViewModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
