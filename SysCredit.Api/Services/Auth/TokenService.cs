namespace SysCredit.Api.Services.Auth;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SysCredit.Api.Attributes;
using SysCredit.Api.Interfaces;
using SysCredit.DataTransferObject.Commons.Auth;

[Service<ITokenService>]
public class TokenService : ITokenService
{
    private readonly IConfiguration Configuration;
    private readonly SymmetricSecurityKey Key;

    public TokenService(IConfiguration Configuration)
    {
        // Use dotnet user secrets for key 
        this.Configuration = Configuration;
        Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Token:Key"]!));
    }

    /// <summary>
    /// Generates a JSON Web Token (JWT) through claims for user authentication and authorization.
    /// </summary>
    /// <param name="user">The user information associated with the token.</param>
    /// <param name="claims">The claims to be included in the token.</param>
    /// <returns>A JWT token as a string.</returns>
    public string GenerateToken(UserInfo User, IEnumerable<Claim> Claims)
    {
        var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

        var TokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(Claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = Credentials,
            Issuer = Configuration["Token:Issuer"]
        }; 

        var TokenHandler = new JwtSecurityTokenHandler();

        var Token = TokenHandler.CreateToken(TokenDescriptor);

        return TokenHandler.WriteToken(Token);
    }
}
