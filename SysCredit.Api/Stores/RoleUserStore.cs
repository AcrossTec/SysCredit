namespace SysCredit.Api.Stores;

using SysCredit.Api.Requests.Authentications.Roles;
using SysCredit.Helpers;
using SysCredit.Models;

public static class RoleUserStore
{
    public static ValueTask<EntityId> UpdateRoleUserAsync(this IStore<Role> Store, UpdateRoleUserRequest Request)
    {
        throw new NotImplementedException();
    }
}
