namespace SysCredit.Api.Interfaces;

using SysCredit.DataTransferObject.Commons.Auth;
using System.Security.Claims;

public interface ITokenService
{
    string GenerateToken(UserInfo user, IEnumerable<Claim> Claims);
}
