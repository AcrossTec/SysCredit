namespace SysCredit.Api.Interfaces;

using SysCredit.Api.ViewModels.Auth.Roles;
using SysCredit.Api.ViewModels.Auth.Users;
using SysCredit.DataTransferObject.Commons.Auth;
using SysCredit.Helpers;

public interface IAuthService
{
    ValueTask<IServiceResult<UserInfo?>> InsertUserAsync(CreateUserRequest Request);

    ValueTask<IServiceResult<EntityId?>> CreateRoleAsync(CreateRoleRequest ViewModel);

    ValueTask<IServiceResult<UserInfo?>> LoginAsync(LoginRequest ViewModel);
}