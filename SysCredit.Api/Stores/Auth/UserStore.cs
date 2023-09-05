namespace SysCredit.Api.Stores.Auth;

using Dapper;
using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using System.Data;

using static Constants.ErrorCodeIndex;
using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;
using SysCredit.Api.ViewModels.Auth.Users;
using SysCredit.Api.Stores;
using SysCredit.Models.Auth.Users;
using SysCredit.DataTransferObject.Commons.Auth;
using SysCredit.DataTransferObject.StoredProcedures.Auth;
using System.Security.Claims;

[Store]
[ErrorCategory(ErrorCategories.UserStore)]
public static class UserStore
{
    /// <summary>
    /// Asynchronously creates a new user and returns user information.
    /// </summary>
    /// <param name="Store">The user store used for user creation.</param>
    /// <param name="Request">The request containing user information to create a new user.</param>
    /// <returns>The user information of the newly created user.</returns>
    [MethodId("1759d45e-f83d-4ae6-9137-ae523b2d5844")]
    [ErrorCode(Prefix: UserStorePrefix, new[] { _0001, _0002 })]
    public static async ValueTask<UserInfo> CreateUserAsync(this IStore<User> Store, CreateUserRequest Request)
    {
        Request.Password = Convert.ToBase64String(Request.Password.ComputeHashSha512());

        DynamicParameters Parameters = Request.ToDynamicParameters();
        Parameters.Add(nameof(User.UserId), default, DbType.Int64, ParameterDirection.Output);

        var Transaction = await Store.BeginTransactionAsync();

        try
        {
            await Store.ExecAsync("[dbo].[InsertUser]", Parameters, Transaction);
            Transaction.Commit();

            return new UserInfo() { UserId = Parameters.Get<long>(nameof(User.UserId)), UserName = Request.UserName };
        }
        catch (Exception Ex)
        {
            SysCreditException SysCreditEx = Ex.ToSysCreditException(typeof(UserStore),
                      "1759d45e-f83d-4ae6-9137-ae523b2d5844", CodeIndex0, "Error al registrar el usuario.", Ex);
            try
            {
                Transaction.Rollback();
            }
            catch (Exception ExRollback)
            {
                SysCreditEx = ExRollback.ToSysCreditException(typeof(UserStore),
                   "1759d45e-f83d-4ae6-9137-ae523b2d5844", CodeIndex1, "Error interno del servidor al registrar el usuario.", SysCreditEx);
                throw SysCreditEx;
            }
            throw SysCreditEx;
        }
    }

    /// <summary>
    /// Asynchronously fetches user information by their email address.
    /// </summary>
    /// <param name="Store">The user store used for database interactions.</param>
    /// <param name="Email">The email address of the user to fetch.</param>
    /// <returns>The user information associated with the provided email address, or null if not found.</returns>
    [MethodId("4c5e368b-a740-43cf-b588-e2cc2af2bee1")]
    public static async ValueTask<UserInfo?> FetchUserByEmailAsync(this IStore<User> Store, string? Email)
    {
        return await Store.ExecQueryAsync<FetchUser>("[dbo].[FetchUserByEmail]", new { Email }).ConvertFetchUserToUserInfo().SingleOrDefaultAsync();
    }

    /// <summary>
    /// Asynchronously fetches user information by their username.
    /// </summary>
    /// <param name="Store">The user store used for database interactions.</param>
    /// <param name="Name">The username of the user to fetch.</param>
    /// <returns>The user information associated with the provided username, or null if not found.</returns>
    [MethodId("7bded396-7046-46a0-8630-7673c27d803e")]
    public static async ValueTask<UserInfo?> FetchUserByUserNameAsync(this IStore<User> Store, string? Name)
    {
        return await Store.ExecQueryAsync<FetchUser>("[dbo].[FetchUserByName]", new { Name }).ConvertFetchUserToUserInfo().SingleOrDefaultAsync();
    }

    /// <summary>
    /// Asynchronously fetches user information by their phone number.
    /// </summary>
    /// <param name="Store">The user store used for database interactions.</param>
    /// <param name="Phone">The phone number of the user to fetch.</param>
    /// <returns>The user information associated with the provided phone number, or null if not found.</returns>
    [MethodId("5da62d88-a5d7-4851-a788-4e839dd25c69")]
    public static ValueTask<UserInfo?> FetchUserByPhoneAsync(this IStore<User> Store, string? Phone)
    {
        return Store.ExecQueryAsync<FetchUser>("[dbo].[FetchUserByName]", new { Phone }).ConvertFetchUserToUserInfo().SingleOrDefaultAsync();
    }

    [MethodId("92e27faa-510a-4595-bdc0-91d3abfcdb24")]
    public static ValueTask<string> ForgotPasswordAsync(this IStore<User> Store, string Email)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Asynchronously verifies user credentials and checks if a user can log in.
    /// </summary>
    /// <param name="Store">The user store used for database interactions.</param>
    /// <param name="Login">The login request containing user credentials.</param>
    /// <returns>
    ///     <c>true</c> if the provided credentials match a user in the database and the user can log in; otherwise, <c>false</c>.
    /// </returns>
    [MethodId("c81bf8cb-f5b4-4a25-80f5-27f1c8329702")]
    public static async ValueTask<bool> LoginAsync(this IStore<User> Store, LoginRequest Login)
    {
        //TODO: Make a Stored procedure for this and validate if the user is no deleted
        Login.Password = Convert.ToBase64String(Login.Password.ComputeHashSha512());

        var Command = "SELECT * FROM User WHERE Password = @Password AND Email = @Email";

        return await Store.QueryFirstOrDefaultAsync<UserInfo>(Command, Login) is not null;
    }

    /// <summary>
    /// Converts an asynchronous stream of fetched user data to user information objects.
    /// </summary>
    /// <param name="Users">An asynchronous stream of fetched user data.</param>
    /// <returns>An asynchronous stream of user information objects.</returns>
    private static IAsyncEnumerable<UserInfo> ConvertFetchUserToUserInfo(this IAsyncEnumerable<FetchUser> Users)
        => Users.Select(U => new UserInfo() { UserId = U.UserId, UserName = U.UserName });

    /// <summary>
    /// Asynchronously fetches user claims by their associated user ID and converts them into claims.
    /// </summary>
    /// <param name="Store">The user store used for database interactions.</param>
    /// <param name="UserId">The unique identifier of the user.</param>
    /// <returns>An asynchronous stream of claims representing user-specific claims.</returns>
    [MethodId("603c33f7-0fad-4669-847f-09353b79ea62")]
    public static IAsyncEnumerable<Claim> FetchUserClaimByUserIdAsync(this IStore<User> Store, long UserId)
    {
        return Store.ExecQueryAsync<UserClaim>("[dbo].[FetchUserClaimById]", UserId).Select(R => new Claim(R.ClaimType, R.ClaimValue));
    }
}