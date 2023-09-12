namespace SysCredit.Api.ViewModels.Auth.Users;

public class ForgotPasswordRequest : IViewModel
{
    public string Email { get; set; } = string.Empty;
}