namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.Attributes;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;

using SysCredit.Api.Requests.Auths;
using SysCredit.Api.Requests.Auths.Roles;

using SysCredit.DataTransferObject.Commons;

using SysCredit.Helpers;
using SysCredit.Models;

using System;
using System.Data;
using System.Reflection;
using System.Security.Claims;

using static Constants.ErrorCodes;
using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(nameof(RoleStore))]
[ErrorCodePrefix(RoleStorePrefix)]
public static class RoleStore
{
    /// <summary>
    ///     Creates a new user role asynchronously and returns the entity ID of the created role.
    /// </summary>
    /// <param name="Store">The role store used for role creation.</param>
    /// <param name="Request">The request containing role information to create a new role.</param>
    /// <returns>The entity ID of the created role.</returns>
    [MethodId("CBEE7E75-07DE-4ED0-BBDB-525AA7D462CD")]
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
            SysCreditException SysCreditEx = Ex.ToSysCreditException(MethodBase.GetCurrentMethod(), DATAARS0001);

            try
            {
                SqlTransaction.Rollback();
            }
            catch (Exception ExRollback)
            {
                throw ExRollback.ToSysCreditException(MethodBase.GetCurrentMethod(), DATAARS0002);
            }

            throw SysCreditEx;
        }
    }

    /// <summary>
    ///     Checks if a collection of role IDs exists in the database and if there are any duplicate role IDs.
    /// </summary>
    /// <param name="Store">The role store used for database interactions.</param>
    /// <param name="Request">The collection of role requests containing role IDs to check.</param>
    /// <returns>
    ///     <c>true</c> if all role IDs exist in the database and there are no duplicates; otherwise, <c>false</c>.
    /// </returns>
    [MethodId("59DD1322-694B-4FF6-A167-454072475BE2")]
    public static async ValueTask<bool> ExistAndDuplicatedRolesAsync(this IStore<Role> Store, IEnumerable<AssignTypeRequest> Request)
    {
        var Parameters = new DynamicParameters();
        Parameters.Add("RoleNames", Request.ToDataTable(), DbType.Object, ParameterDirection.Input);

        var Result = await Store.ExecAsync("[dbo].[CheckExistAndDuplicatedRoles]", Parameters);
        return Result > 0;
    }

    /// <summary>
    ///     Asynchronously fetches user roles as claims by their associated user ID.
    /// </summary>
    /// <param name="Store">The role store used for database interactions.</param>
    /// <param name="UserId">The unique identifier of the user.</param>
    /// <returns>An asynchronous stream of claims representing user roles.</returns>
    [MethodId("F8B04734-355E-4818-BF9C-56FD1348482A")]
    public static async ValueTask<RoleInfo?> FetchRoleByUserId(this IStore<Role> Store, long UserId)
    {
        return await Store.ExecQueryAsync<RoleInfo>("[dbo].[FetchRoleUserById]", new { UserId }).SingleOrDefaultAsync();
    }

    /// <summary>
    /// Obtiene las reclamaciones (claims) de un rol asociado a un usuario por su identificador de usuario.
    /// </summary>
    /// <param name="Store">La interfaz de almacenamiento que proporciona acceso a los roles y reclamaciones.</param>
    /// <param name="UserId">El identificador de usuario para el que se desean obtener las reclamaciones del rol.</param>
    /// <returns>
    /// Un flujo asincrónico de objetos Claim que representan las reclamaciones del rol asociado al usuario,
    /// o una secuencia vacía si no se encuentran reclamaciones para el usuario o el rol.
    /// </returns>
    [MethodId("38D50296-56AC-4824-A200-A9E151878A55")]
    public static IAsyncEnumerable<Claim> FetchRoleClaimsByUserId(this IStore<Role> Store, long? UserId)
    {
        return Store.ExecQueryAsync<RoleClaim>("[dbo].[FetchRoleClaimByUserId]", new { UserId }).Select(C => new Claim(C.ClaimType, C.ClaimValue));
    }

    /// <summary>
    /// Busca un rol por su nombre en la base de datos.
    /// </summary>
    /// <param name="Store">La interfaz de almacenamiento que proporciona acceso a la base de datos.</param>
    /// <param name="RoleName">El nombre del rol que se desea buscar.</param>
    /// <returns>
    /// Un objeto RoleInfo si se encuentra un rol con el nombre especificado,
    /// o null si no se encuentra ningún rol con ese nombre.
    /// </returns>
    [MethodId("38D50296-56AC-4824-A200-A9E151878A55")]
    public static async ValueTask<RoleInfo?> FetchRoleByName(this IStore<Role> Store, string? RoleName)
    {
        return await Store.ExecQueryAsync<RoleInfo>("[dbo].[FetchRoleByName]", new { RoleName }).SingleOrDefaultAsync();
    }
}