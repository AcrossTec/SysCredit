namespace SysCredit.Api.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using SysCredit.Api.Attributes;
using SysCredit.Api.Interfaces.Services;
using SysCredit.DataTransferObject.Commons;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

/// <summary>
/// 
/// </summary>
/// <param name="Options"></param>
[Service<IAuthorizationService>]
public class AuthorizationService(IOptions<SysCreditOptions> Options) : IAuthorizationService
{
    private readonly SysCreditOptions Options = Options.Value;
    private readonly SymmetricSecurityKey Key = new(Encoding.UTF8.GetBytes(Options.Value.TokenInfo.Key));

    /// <summary>
    ///     Generates a JSON Web Token (JWT) through claims for user authentication and authorization.
    /// </summary>
    /// <param name="user">The user information associated with the token.</param>
    /// <param name="claims">The claims to be included in the token.</param>
    /// <returns>A JWT token as a string.</returns>
    public string GenerateToken(UserInfo? User, IEnumerable<Claim> Claims)
    {
        var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

        var TokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(Claims),
            SigningCredentials = Credentials,
            Issuer = Options.TokenInfo.Issuer,
            Expires = DateTime.Now.AddDays(1),
        };

        var TokenHandler = new JwtSecurityTokenHandler();

        var Token = TokenHandler.CreateToken(TokenDescriptor);

        return TokenHandler.WriteToken(Token);
    }
}
