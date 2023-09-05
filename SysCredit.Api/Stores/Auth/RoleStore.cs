namespace SysCredit.Api.Stores.Auth;

using Dapper;
using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.ViewModels.Auth;
using SysCredit.Api.ViewModels.Auth.Roles;
using SysCredit.Helpers;
using SysCredit.Models.Auth.Roles;
using System;
using System.Data;

using System.Security.Claims;
using static Constants.ErrorCodeIndex;
using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(ErrorCategories.RoleStore)]
public static class RoleStore
{
    /// <summary>
    /// Creates a new user role asynchronously and returns the entity ID of the created role.
    /// </summary>
    /// <param name="Store">The role store used for role creation.</param>
    /// <param name="Request">The request containing role information to create a new role.</param>
    /// <returns>The entity ID of the created role.</returns>
    [MethodId("4908e550-64db-44f6-98b0-34d8eaff5eb5")]
    [ErrorCode(Prefix: RoleStorePrefix, Codes: new[] { _0001, _0002 })]
    public static async ValueTask<EntityId> CreateRoleAsync(this IStore<Role> Store, CreateRoleRequest Request)
    {
        DynamicParameters Parameters = Request.ToDynamicParameters();
        Parameters.Add(nameof(Role.RoleId), default, DbType.Int64, ParameterDirection.Output);

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            await Store.ExecAsync("[dbo].[InsertRole]", Parameters, SqlTransaction);

            SqlTransaction.Commit();

            return Parameters.Get<long>(nameof(Role.RoleId));
        }
        catch (Exception Ex)
        {
            SysCreditException SysCreditEx = Ex.ToSysCreditException(typeof(RoleStore),
                "4908e550-64db-44f6-98b0-34d8eaff5eb5", CodeIndex0, "Error al registrar el role.", Ex);

            try
            {
                SqlTransaction.Rollback();
            }
            catch (Exception ExRollback)
            {
                SysCreditEx = ExRollback.ToSysCreditException(typeof(RoleStore),
                   "4908e550-64db-44f6-98b0-34d8eaff5eb5", CodeIndex1, "Error interno del servidor al registrar el role.", SysCreditEx);
                throw SysCreditEx;
            }
            throw SysCreditEx;
        }
    }

    /// <summary>
    /// Checks if a collection of role IDs exists in the database and if there are any duplicate role IDs.
    /// </summary>
    /// <param name="Store">The role store used for database interactions.</param>
    /// <param name="Request">The collection of role requests containing role IDs to check.</param>
    /// <returns>
    ///     <c>true</c> if all role IDs exist in the database and there are no duplicates; otherwise, <c>false</c>.
    /// </returns>
    [MethodId("7e13efb1-06fa-48e2-b0de-41b16cbd75e3")]
    public static async ValueTask<bool> ExistAndDuplicatedRolesAsync(this IStore<Role> Store, IEnumerable<AssingRoleRequest> Request)
    {
        var Distinct = Request.DistinctBy(R => R.RoleId);

        if (!(Distinct.Count() == Request.Count())) return false;

        string Command = "SELECT RoleId FROM Role WHERE RoleId IN @RoleId";

        IEnumerable<long> RolesInDb = await Store.Connection.QueryAsync<long>
        (
           Command, 
           new { RoleId = Request.Select(r => r.RoleId) }
        );

        foreach (var Role in Request)
        {
            if (!RolesInDb.Contains(Role.RoleId)) return false; 
        }

        return true;
    }

    /// <summary>
    /// Asynchronously fetches user roles as claims by their associated user ID.
    /// </summary>
    /// <param name="Store">The role store used for database interactions.</param>
    /// <param name="UserId">The unique identifier of the user.</param>
    /// <returns>An asynchronous stream of claims representing user roles.</returns>
    [MethodId("603c33f7-0fad-4669-847f-09353b79ea62")]
    public static IAsyncEnumerable<Claim> FetchRoleAsClaimByUserIdAsync(this IStore<Role> Store, long UserId)
    {
        return Store.ExecQueryAsync<Role>("[dbo].[FetchRoleUserById]", UserId).Select(R => new Claim(ClaimTypes.Role, R.RoleName));
    }

    /// <summary>
    /// Asynchronously fetches role claims by their associated role ID and converts them into claims.
    /// </summary>
    /// <param name="Store">The role store used for database interactions.</param>
    /// <param name="RoleId">The unique identifier of the role.</param>
    /// <returns>An asynchronous stream of claims representing role-specific claims.</returns>
    [MethodId("603c33f7-0fad-4669-847f-09353b79ea62")]
    public static IAsyncEnumerable<Claim> FetchRoleClaimsByUserIdAsClaimAsync(this IStore<Role> Store, long RoleId)
    {
        return Store.ExecQueryAsync<RoleClaim>("[dbo].[FetchRoleClaimById]", RoleId).Select(R => new Claim(R.ClaimType, R.ClaimValue));
    }
}