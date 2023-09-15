namespace SysCredit.Api.Requests.Auths.Users;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Auths.Users;

[Validator<TokenRequestValidator>]
public class TokenRequest : IRequest
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
