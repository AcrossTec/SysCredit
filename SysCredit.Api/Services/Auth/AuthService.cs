namespace SysCredit.Api.Services.Auth;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Interfaces;
using SysCredit.Api.ViewModels.Auth.Roles;
using SysCredit.Api.ViewModels.Auth.Users;
using SysCredit.Helpers;
using System.Threading.Tasks;

using static Constants.ErrorCodeIndex;
using static SysCredit.Helpers.ContextData;
using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;
using SysCredit.Api.Stores;
using SysCredit.Models.Auth.Users;
using SysCredit.Models.Auth.Roles;
using SysCredit.DataTransferObject.Commons.Auth;
using SysCredit.Api.Extensions;
using SysCredit.Api.Stores.Auth;

[Service<IAuthService>]
[ErrorCategory(ErrorCategories.AuthService)]
public class AuthService : IAuthService
{
    private readonly IStore Store;
    private readonly ITokenService TokenService;
    private readonly IStore<User> UserStore;
    private readonly IStore<Role> RoleStore;

    public AuthService(IStore Store, ITokenService TokenService)
    {
        this.Store = Store;
        UserStore = Store.GetStore<User>();
        RoleStore = Store.GetStore<Role>();
        this.TokenService = TokenService;
    }

    /// <summary>
    /// Creates a new user role asynchronously and returns a service result with the role's entity ID.
    /// </summary>
    /// <param name="ViewModel">The request containing role information to create a new role.</param>
    /// <returns>A task representing the service result with the entity ID of the created role or null if creation fails.</returns>
    [MethodId("9ce328c4-4afd-4965-89c4-47133284d432")]
    [ErrorCode(Prefix: AuthServicePrefix, Codes: new[] { _0001, _0002 })]
    public ValueTask<IServiceResult<EntityId?>> CreateRoleAsync(CreateRoleRequest ViewModel)
    {
        // TODO: Create stored procedure for this method and yours validations
        throw new NotImplementedException();
    }

    /// <summary>
    /// Inserts a new user asynchronously and returns a service result with user information.
    /// </summary>
    /// <param name="Request">The request containing user information to create a new user.</param>
    /// <returns>A task representing the service result with user information or null if the insertion fails.</returns>
    [MethodId("395707b3-f7d6-4c5a-ba6e-2a7c73b84d14")]
    [ErrorCode(Prefix: AuthServicePrefix, Codes: new[] { _0003 })]
    public async ValueTask<IServiceResult<UserInfo?>> InsertUserAsync(CreateUserRequest Request)
    {
        // TODO: Create stored procedure for this method and yours validations
        var Result = await Request.ValidateAsync(
            Key(nameof(UserStore)).Value(UserStore).
            Key(nameof(RoleStore)).Value(RoleStore));

        if (!Result.IsValid)
        {
            return await Result.CreateResultAsync<UserInfo?>(
                      typeof(AuthService),
                      "395707b3-f7d6-4c5a-ba6e-2a7c73b84d14",
                      CodeIndex2,
                      $"Error al crear el usuario: {Request.UserName}");
        }

        UserInfo User = await UserStore.CreateUserAsync(Request);

        return await User.CreateResultAsync();
    }

    /// <summary>
    /// Logs in a user asynchronously and generates an authentication token.
    /// </summary>
    /// <param name="ViewModel">The login request view model containing user credentials.</param>
    /// <returns>A task representing the service result with user information or null if login fails.</returns>
    [MethodId("2132f30d-d092-46bf-9331-5c90cfebd19c")]
    [ErrorCode(Prefix: AuthServicePrefix, Codes: new[] { _0005, _0006 })]
    public ValueTask<IServiceResult<UserInfo?>> LoginAsync(LoginRequest ViewModel)
    {
        // TODO: Create stored procedure for this method and yours validations

        //var UserClaims = await UserStore.FetchUserClaimByUserIdAsync(User.UserId).ToListAsync();
        //var RoleAsClaim = await RoleStore.FetchRoleAsClaimByUserIdAsync(User.UserId).ToListAsync();
        //var RoleClaims = await RoleStore.FetchRoleClaimsByUserIdAsClaimAsync(User.UserId).ToListAsync();

        //UserClaims.AddRange(RoleAsClaim);
        //UserClaims.AddRange(RoleClaims);

        //User.Token = TokenService.GenerateToken(User, UserClaims);
        throw new NotImplementedException();
    }
}
