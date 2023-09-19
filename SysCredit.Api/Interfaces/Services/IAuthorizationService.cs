namespace SysCredit.Api.Interfaces.Services;

using SysCredit.DataTransferObject.Commons;

using System.Security.Claims;

public interface IAuthorizationService
{
    string GenerateToken(UserInfo? User, IEnumerable<Claim> Claims);
}
