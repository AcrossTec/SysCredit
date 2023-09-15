namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.Attributes;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Auths.Users;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

using System.Data;
using System.Reflection;
using System.Security.Claims;

using static Constants.ErrorCodes;

[Store]
[ErrorCategory(nameof(UserStore))]
public static class UserStore
{
    /// <summary>
    ///     Asynchronously creates a new user and returns user information.
    /// </summary>
    /// <param name="Store">The user store used for user creation.</param>
    /// <param name="Request">The request containing user information to create a new user.</param>
    /// <returns>The user information of the newly created user.</returns>
    [MethodId("46D50A8E-A1AB-4169-A738-4A3615309C16")]
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
            SysCreditException SysCreditEx = Ex.ToSysCreditException(MethodBase.GetCurrentMethod(), DATAAU0001);

            try
            {
                Transaction.Rollback();
            }
            catch (Exception ExRollback)
            {
                throw ExRollback.ToSysCreditException(MethodBase.GetCurrentMethod(), DATAAU0002);
            }

            throw SysCreditEx;
        }
    }

    /// <summary>
    ///     Asynchronously fetches user information by their email address.
    /// </summary>
    /// <param name="Store">The user store used for database interactions.</param>
    /// <param name="Email">The email address of the user to fetch.</param>
    /// <returns>The user information associated with the provided email address, or null if not found.</returns>
    [MethodId("F98C3EEC-A01D-4A9D-BFA6-F55CDE46181B")]
    public static async ValueTask<UserInfo?> FetchUserByEmailAsync(this IStore<User> Store, string? Email)
    {
        return await Store.ExecQueryAsync<UserInfo>("[dbo].[FetchUserByEmail]", new { Email }).SingleOrDefaultAsync();
    }

    /// <summary>
    ///     Asynchronously fetches user information by their Id.
    /// </summary>
    /// <param name="Store">The user store used for database interactions.</param>
    /// <param name="UserId">The email address of the user to fetch.</param>
    /// <returns>The user information associated with the provided email address, or null if not found.</returns>
    [MethodId("BDFA8A45-73AC-4FDE-992F-F8101F27E238")]
    public static async ValueTask<UserInfo?> FetchUserByIdAsync(this IStore<User> Store, long? UserId)
    {
        return await Store.ExecQueryAsync<UserInfo>("[dbo].[FetchUserById]", new { UserId }).SingleOrDefaultAsync();
    }

    /// <summary>
    ///     Asynchronously fetches user information by their username.
    /// </summary>
    /// <param name="Store">The user store used for database interactions.</param>
    /// <param name="Name">The username of the user to fetch.</param>
    /// <returns>The user information associated with the provided username, or null if not found.</returns>
    [MethodId("E5F347C9-7ADE-42D6-95F5-A516FD3E7473")]
    public static async ValueTask<UserInfo?> FetchUserByUserNameAsync(this IStore<User> Store, string? Name)
    {
        return await Store.ExecQueryAsync<UserInfo>("[dbo].[FetchUserByName]", new { Name }).SingleOrDefaultAsync();
    }

    /// <summary>
    ///     Asynchronously fetches user information by their phone number.
    /// </summary>
    /// <param name="Store">The user store used for database interactions.</param>
    /// <param name="Phone">The phone number of the user to fetch.</param>
    /// <returns>The user information associated with the provided phone number, or null if not found.</returns>
    [MethodId("80F6F1B2-6003-4D5D-A7B5-322209EFECFC")]
    public static ValueTask<UserInfo?> FetchUserByPhoneAsync(this IStore<User> Store, string? Phone)
    {
        return Store.ExecQueryAsync<UserInfo>("[dbo].[FetchUserByPhone]", new { Phone }).SingleOrDefaultAsync();
    }

    [MethodId("7818A2E1-25D4-47CE-A161-CECD808E28E0")]
    public static ValueTask<string> ForgotPasswordAsync(this IStore<User> Store, string Email)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Asynchronously verifies user credentials and checks if a user can log in.
    /// </summary>
    /// <param name="Store">The user store used for database interactions.</param>
    /// <param name="Login">The login request containing user credentials.</param>
    /// <returns>
    ///     <c>true</c> if the provided credentials match a user in the database and the user can log in; otherwise, <c>false</c>.
    /// </returns>
    [MethodId("04DCE1D6-EA3D-486A-A4CB-A04A63B880FD")]
    public static async ValueTask<bool> LoginAsync(this IStore<User> Store, TokenRequest Login)
    {
        Login.Password = Convert.ToBase64String(Login.Password.ComputeHashSha512());
        return await Store.ExecQueryAsync<UserInfo>("[dbo].[FetchLogin]", Login).SingleOrDefaultAsync() is not null;
    }

    /// <summary>
    ///     Asynchronously fetches user claims by their associated user ID and converts them into claims.
    /// </summary>
    /// <param name="Store">The user store used for database interactions.</param>
    /// <param name="UserId">The unique identifier of the user.</param>
    /// <returns>An asynchronous stream of claims representing user-specific claims.</returns>
    [MethodId("B96E8177-AE70-49C9-A8E2-01E8E36B88DB")]
    public static IAsyncEnumerable<Claim> FetchClaimsByUserId(this IStore<User> Store, long? UserId)
    {
        // TODO: Return UserId for validations
        return Store.ExecQueryAsync<UserClaim>("[dbo].[FetchClaimByUserId]", new { UserId }).Select(C => new Claim(C.ClaimType, C.ClaimValue));
    }
}