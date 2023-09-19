namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.Auths.Roles;
using SysCredit.Api.Requests.Auths.Users;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Reflection;
using System.Threading.Tasks;

using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;
using static SysCredit.Helpers.ContextData;

[Service<IAuthenticationService>]
[ErrorCategory(nameof(AuthenticationService))]
[ErrorCodePrefix(AuthenticationServicePrefix)]
public class AuthenticationService(IStore Store, IAuthorizationService AuthorizationService) : IAuthenticationService
{
    private readonly IStore<User> UserStore = Store.GetStore<User>();
    private readonly IStore<Role> RoleStore = Store.GetStore<Role>();

    /// <summary>
    ///     Creates a new user role asynchronously and returns a service result with the role's entity ID.
    /// </summary>
    /// <param name="Request">The request containing role information to create a new role.</param>
    /// <returns>A task representing the service result with the entity ID of the created role or null if creation fails.</returns>
    [MethodId("33502022-6438-4C99-A1DD-DACE2EA00266")]
    public async ValueTask<IServiceResult<EntityId?>> CreateRoleAsync(CreateRoleRequest Request)
    {
        var Result = await Request.ValidateAsync(
            Key(nameof(RoleStore)).
            Value(RoleStore));

        if (Result.HasError())
        {
            return await Result.CreateServiceResultAsync<EntityId?>
            (
                MethodInfo: MethodBase.GetCurrentMethod(),
                 ErrorCode: $"{AuthenticationServicePrefix}{_0002}" // ErrorMessage: "La solicitutd de creación del rol no es válida"
            );
        }

        return await RoleStore.CreateRoleAsync(Request).CreateServiceResultAsync();
    }

    /// <summary>
    ///     Inserts a new user asynchronously and returns a service result with user information.
    /// </summary>
    /// <param name="Request">The request containing user information to create a new user.</param>
    /// <returns>A task representing the service result with user information or null if the insertion fails.</returns>
    [MethodId("2DDAFB57-ABB4-4699-9DC1-A0EDE3B515CE")]
    public async ValueTask<IServiceResult<UserInfo?>> CreateUserAsync(CreateUserRequest Request)
    {
        var Result = await Request.ValidateAsync(
            Key(nameof(UserStore)).Value(UserStore).
            Key(nameof(RoleStore)).Value(RoleStore));

        if (Result.HasError())
        {
            return await Result.CreateServiceResultAsync<UserInfo?>
            (
                MethodInfo: MethodBase.GetCurrentMethod(),
                 ErrorCode: $"{AuthenticationServicePrefix}{_0002}" // ErrorMessage: $"Error al crear el usuario: {Request.UserName}"
            );
        }

        UserInfo User = await UserStore.CreateUserAsync(Request);
        return await User.CreateServiceResultAsync();
    }

    /// <summary>
    ///     Busca un usuario por su dirección de correo electrónico en la base de datos de forma asincrónica.
    /// </summary>
    /// <param name="Email">La dirección de correo electrónico del usuario que se desea buscar.</param>
    /// <returns>
    ///     Un objeto UserInfo si se encuentra un usuario con la dirección de correo electrónico especificada,
    ///     o null si no se encuentra ningún usuario con ese correo electrónico.
    /// </returns>
    [MethodId("1333281A-B038-4224-9BAD-1E12CA47FF9D")]
    public ValueTask<UserInfo?> FetchUserByEmailAsync(string? Email)
    {
        return UserStore.FetchUserByEmailAsync(Email);
    }

    /// <summary>
    ///     Busca un usuario por su identificador de usuario de forma asincrónica.
    /// </summary>
    /// <param name="UserId">El identificador de usuario del usuario que se desea buscar.</param>
    /// <returns>
    ///     Un objeto UserInfo si se encuentra un usuario con el identificador de usuario especificado,
    ///     o null si no se encuentra ningún usuario con ese identificador.
    /// </returns>
    [MethodId("68FDCE46-5844-476B-9391-5686C539189C")]
    public ValueTask<UserInfo?> FetchUserByIdAsync(long? UserId)
    {
        return UserStore.FetchUserByIdAsync(UserId);
    }

    /// <summary>
    ///     Logs in a user asynchronously and generates an authentication token.
    /// </summary>
    /// <param name="Request">The login request view model containing user credentials.</param>
    /// <returns>A task representing the service result with user information or null if login fails.</returns>
    [MethodId("8C909B20-7CE6-4CD8-B55E-FF4F60919EBA")]
    public async ValueTask<IServiceResult<AuthInfo?>> TokenRequestAsync(TokenRequest Request)
    {
        var Result = await Request.ValidateAsync();

        if (Result.HasError())
        {
            return await Result.CreateServiceResultAsync<AuthInfo>
            (
                MethodInfo: MethodBase.GetCurrentMethod(),
                 ErrorCode: $"{AuthenticationServicePrefix}{_0003}" // ErrorMessage: $"Error al iniciar sesión"
            );
        }

        var IsValid = await UserStore.LoginAsync(Request);

        if (!IsValid)
        {
            return await Result.CreateServiceResultAsync<AuthInfo>
            (
                MethodInfo: MethodBase.GetCurrentMethod(),
                 ErrorCode: $"{AuthenticationServicePrefix}{_0003}" // ErrorMessage: $"Error al iniciar sesión"
            );
        }

        UserInfo? User = await UserStore.FetchUserByEmailAsync(Request.Email);

        var UserClaims = await UserStore.FetchClaimsByUserId(User?.UserId).ToListAsync();

        var RoleClaims = await RoleStore.FetchRoleClaimsByUserId(User?.UserId).ToListAsync();

        UserClaims.AddRange(RoleClaims);

        var Token = AuthorizationService.GenerateToken(User, UserClaims);

        return await new AuthInfo
        {
            UserId = User!.UserId,
            UserName = User.UserName,
            Email = User.Email,
            Phone = User.Phone,
            Token = Token,
        }.CreateServiceResultAsync();
    }
}
