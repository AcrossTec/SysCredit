using SysCredit.Api.ViewModels.Auth.Roles;
using SysCredit.Api.ViewModels.Auth.Users;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;

namespace SysCredit.Api.Interfaces;

public interface IAuthService
{
    ValueTask<IServiceResult<EntityId?>> CreateRoleAsync(CreateRoleRequest CreateRoleRequest);

    ValueTask<IServiceResult<UserInfo?>> CreateUserAsync(CreateUserRequest Request);

    ValueTask<UserInfo?> FetchUserByEmailAsync(string? Email);

    ValueTask<UserInfo?> FetchUserByIdAsync(long? UserId);

    ValueTask<IServiceResult<AuthInfo?>> TokenRequestAsync(TokenRequest ViewModel);
}
