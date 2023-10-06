namespace SysCredit.Api.Stores;


using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Relationships;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

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
    /// 
    /// </summary>
    /// <param name="Store"></param>
    /// <returns></returns>
    [MethodId("9D9648AF-EE89-4B08-9B6E-96016C086D3F")]
    public static IAsyncEnumerable<RelationshipInfo> FetchRelationshipAsync(this IStore<Relationship> Store)
    {
        return Store.ExecuteStoredProcedureQueryAsync<RelationshipInfo>("[dbo].[FetchRelationship]");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Store"></param>
    /// <param name="RelationshipId"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="Store"></param>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("A0F212C3-4A69-4719-B80D-E73AF3AD9D37")]
    public static async ValueTask<long> InsertRelationshipAsync(this IStore<Relationship> Store, CreateRelationshipRequest Request)
    {
        // TODO: Reparar esta implementación
        return await Store.ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<long>("[dbo].[InsertRelationship]", Request);
    }
}
