using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Auth.Users;

namespace SysCredit.Api.ViewModels.Auth.Users;

[Validator<TokenRequestValidator>]
public class TokenRequest : IViewModel
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
