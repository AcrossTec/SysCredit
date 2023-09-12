namespace SysCredit.Api.Interfaces;

using SysCredit.DataTransferObject.Commons;
using System.Security.Claims;

public interface ITokenService
{
    string GenerateToken(UserInfo? User, IEnumerable<Claim> Claims);
}
