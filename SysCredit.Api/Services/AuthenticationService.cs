namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.Authentications.Roles;
using SysCredit.Api.Requests.Authentications.Users;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Reflection;
using System.Threading.Tasks;

using static Constants.ErrorCodePrefix;
using static Constants.ErrorCodes;

using static SysCredit.Helpers.ContextData;

/// <summary>
/// 
/// </summary>
/// <param name="Store"></param>
/// <param name="AuthorizationService"></param>
[Service<IAuthenticationService>]
[ErrorCategory(nameof(AuthenticationService))]
[ErrorCodePrefix(AuthenticationServicePrefix)]
public class AuthenticationService(IStore Store) : IAuthenticationService
{
    private readonly IStore<User> UserStore = Store.GetStore<User>();
    private readonly IStore<Role> RoleStore = Store.GetStore<Role>();
}
