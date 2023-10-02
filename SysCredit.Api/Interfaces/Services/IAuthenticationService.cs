namespace SysCredit.Api.Interfaces.Services;

using SysCredit.Api.Requests.Authentications.Roles;
using SysCredit.Api.Requests.Authentications.Users;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;

public interface IAuthenticationService
{
    ValueTask<IServiceResult<EntityId>> CreateRoleAsync(CreateRoleRequest CreateRoleRequest);

    ValueTask<IServiceResult<UserInfo?>> CreateUserAsync(CreateUserRequest Request);

    ValueTask<UserInfo?> FetchUserByEmailAsync(string? Email);

    ValueTask<UserInfo?> FetchUserByIdAsync(long? UserId);

    ValueTask<IServiceResult<AuthInfo?>> TokenRequestAsync(TokenRequest ViewModel);
}
