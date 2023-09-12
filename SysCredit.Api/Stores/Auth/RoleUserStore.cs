namespace SysCredit.Api.Stores.Auth;

using SysCredit.Api.ViewModels.Auth.Roles;
using SysCredit.Helpers;
using SysCredit.Models.Auth.Roles;

public static class RoleUserStore
{
    public static ValueTask<EntityId> UpdateRoleUserAsync(this IStore<Role> Store, UpdateRoleUserRequest Request)
    {
        throw new NotImplementedException();
    }
}
