namespace SysCredit.Api.Stores;

using Dapper;
using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Relationships;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Data;
using System.Reflection;

using static Constants.ErrorCodePrefix;

/// <summary>
/// 
/// </summary>
[Store]
[ErrorCategory(nameof(RelationshipStore))]
[ErrorCodePrefix(RelationshipStorePrefix)]
public static partial class RelationshipStore
{
    /// <summary>
    ///     Obtiene todos los <see cref="Relationship"/>
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos
    /// </param>
    /// <returns>
    ///     Regresa todos los <see cref="Relationship"/>
    /// </returns>
    [MethodId("9D9648AF-EE89-4B08-9B6E-96016C086D3F")]
    public static IAsyncEnumerable<RelationshipInfo> FetchRelationshipAsync(this IStore<Relationship> Store)
    {
        return Store.ExecuteStoredProcedureQueryAsync<RelationshipInfo>("[dbo].[FetchRelationship]");
    }

    /// <summary>
    ///     Regresa un registro de la tabla <see cref="Models.Relationship"/>
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos
    /// </param>
    /// <param name="RelationshipId">
    ///     Id obtenido de la ruta
    /// </param>
    /// <returns>
    ///     Regresa un registro de la tabla <see cref="Models.Relationship"/>
    /// </returns>
    [MethodId("44434D9E-2ECE-4DA2-A6F6-567D915E0230")]
    public static async ValueTask<RelationshipInfo?> FetchRelationshipByIdAsync(this IStore<Relationship> Store, long? RelationshipId)
    {
        return await Store.ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<RelationshipInfo?>("[dbo].[FetchRelationshipById]", new { RelationshipId });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Store"></param>
    /// <param name="Name"></param>
    /// <returns></returns>
    [MethodId("DA450A19-3E93-4C48-B1E3-A1ADDCD92E7E")]
    public static async ValueTask<RelationshipInfo?> FetchRelationshipByNameAsync(this IStore<Relationship> Store, string? Name)
    {
        return await Store.ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<RelationshipInfo?>("[dbo].[FetchRelationshipByName]", new { Name });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Store"></param>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("537E74C8-FD13-4A72-80BD-E0D5C3AD0D4C")]
    public static async ValueTask<bool> UpdateRelationshipAsync(this IStore<Relationship> Store, UpdateRelationshipRequest Request)
    {
        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            int Result = await Store.ExecuteStoredProcedureAsync("[dbo].[UpdateRelationship]", Request, SqlTransaction);
            SqlTransaction.Commit();

            return Result > 0;
        }
        catch (Exception SqlEx)
        {
            SysCreditException SysCreditEx = SqlEx.ToSysCreditException(MethodInfo.GetCurrentMethod(), string.Empty /*DATAR0501*/);

            try
            {
                SqlTransaction.Rollback();
            }
            catch (Exception Ex)
            {
                throw Ex.ToSysCreditException(MethodInfo.GetCurrentMethod(), string.Empty /*DATAR0502*/);
            }

            throw SysCreditEx;
        }

    }

    /// <summary>
    ///     Invoca el store para crear el <see cref="Models.Relationship"/> en la base de datos
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos
    /// </param>
    /// <param name="Request">
    ///     egresa el nuevo Id del <see cref="Models.Relationship"/> 
    /// </param>
    /// <returns></returns>
    [MethodId("A0F212C3-4A69-4719-B80D-E73AF3AD9D37")]
    public static async ValueTask<EntityId> InsertRelationshipAsync(this IStore<Relationship> Store, CreateRelationshipRequest Request)
    {
        DynamicParameters Parameters = Request.ToDynamicParameters();
        Parameters.Add(nameof(Relationship.RelationshipId), default, DbType.Int64, ParameterDirection.Output);

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            // Handle the exception if the transaction fails to commit.
            await Store.ExecuteStoredProcedureAsync("[dbo].[InsertRelationship]", Parameters, SqlTransaction);
            SqlTransaction.Commit();

            return Parameters.Get<long?>(nameof(Relationship.RelationshipId));
        }
        catch (Exception SqlEx)
        {
            SysCreditException SysCreditEx = SqlEx.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATALT0001*/);

            try
            {
                // Attempt to roll back the transaction.
                SqlTransaction.Rollback();
            }
            catch (Exception Ex)
            {
                // Throws an InvalidOperationException if the connection is closed or the transaction has already been rolled back on the server.
                throw Ex.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATALT0002*/);
            }

            throw SysCreditEx;
        }
    }

    /// <summary>
    ///     Realiza la eliminacion del <see cref="Models.Relationship;"/> de la base de datos
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos
    /// </param>
    /// <param name="Request">
    ///     Id del <see cref="Models.Relationship"/> a eliminar
    /// </param>
    /// <returns>
    ///     Regresa <see langword="true"/> si se elimino <paramref name="Request"/>, en caso contrario <see langword="false"/>.
    /// </returns>
    [MethodId("3ABCEA14-0B75-4612-A017-FA86E2A234F8")]
    public static async ValueTask<bool> DeleteRelationshipAsync(this IStore<Relationship> Store, DeleteRelationshipRequest Request)
    {
        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            int Result = await Store.ExecuteStoredProcedureAsync("[dbo].[DeleteRelationship]", Request, SqlTransaction);
            SqlTransaction.Commit();

            return Result > 0;
        }
        catch (Exception SqlEx)
        {
            SysCreditException SysCreditEx = SqlEx.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATAPF0003*/);

            try
            {
                SqlTransaction.Rollback();
            }
            catch (Exception Ex)
            {
                throw Ex.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATAPF0004*/);
            }

            throw SysCreditEx;
        }
    }
}
