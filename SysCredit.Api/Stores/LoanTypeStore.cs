namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.Attributes;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.ViewModels.LoanType;
using SysCredit.Api.ViewModels.LoanTypes;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Data;

using static Constants.ErrorCodes;
using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;
using System.Reflection;

/// <summary>
/// 
/// </summary>
[Store]
[ErrorCategory(nameof(LoanTypeStore))]
[ErrorCodePrefix(LoanTypeStorePrefix)]
public static class LoanTypeStore
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Store"></param>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("84370664-26c5-45ed-9590-6df60a9efac4")]
    public static async ValueTask<EntityId> InsertLoanTypeAsync(this IStore<LoanType> Store, CreateLoanTypeRequest Request)
    {
        DynamicParameters Parameters = Request.ToDynamicParameters();
        Parameters.Add(nameof(LoanType.LoanTypeId), default, DbType.Int64, ParameterDirection.Output);

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            // Handle the exception if the transaction fails to commit.
            await Store.ExecAsync("[dbo].[InsertLoanType]", Parameters, SqlTransaction);
            SqlTransaction.Commit();

            return Parameters.Get<long?>(nameof(LoanType.LoanTypeId));
        }
        catch (Exception SqlEx)
        {
            SysCreditException SysCreditEx = SqlEx.ToSysCreditException(MethodInfo.GetCurrentMethod(), DATALT0001);

            try
            {
                // Attempt to roll back the transaction.
                SqlTransaction.Rollback();
            }
            catch (Exception Ex)
            {
                // Throws an InvalidOperationException if the connection is closed or the transaction has already been rolled back on the server.
                throw Ex.ToSysCreditException(MethodInfo.GetCurrentMethod(), DATALT0002);
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
    [MethodId("FC5C3FA3-E112-49D7-B5EE-37181239695C")]
    public static async ValueTask<bool> DeleteLoanTypeAsync(this IStore<LoanType> Store, DeleteLoanTypeRequest Request)
    {
        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            int Result = await Store.ExecAsync("[dbo].[DeleteLoanType]", Request, SqlTransaction);
            SqlTransaction.Commit();

            return Result > 0;
        }
        catch (Exception SqlEx)
        {
            SysCreditException SysCreditEx = SqlEx.ToSysCreditException(MethodInfo.GetCurrentMethod(), DATALT0003);

            try
            {
                SqlTransaction.Rollback();
            }
            catch (Exception Ex)
            {
                throw Ex.ToSysCreditException(MethodInfo.GetCurrentMethod(), DATALT0004);
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
    [MethodId("C367398E-F4F3-4350-86A5-AE2B3DBEBED7")]
    public static async ValueTask<bool> UpdateLoanTypeAsync(this IStore<LoanType> Store, UpdateLoanTypeRequest Request)
    {
        DynamicParameters Parameters = Request.ToDynamicParameters();

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            await Store.ExecAsync("[dbo].[UpdateLoanTypeById]", Parameters, SqlTransaction);
            SqlTransaction.Commit();
            return true;
        }
        catch (Exception Ex)
        {
            SysCreditException SysCreditEx = Ex.ToSysCreditException(MethodInfo.GetCurrentMethod(), DATALT0006);

            try
            {
                SqlTransaction.Rollback();
            }
            catch (Exception ExRollback)
            {
                throw ExRollback.ToSysCreditException(MethodInfo.GetCurrentMethod(), DATALT0007);
            }
            throw SysCreditEx;
        }
    }

    [MethodId("9D9648AF-EE89-4B08-9B6E-96016C086D3F")]
    public static IAsyncEnumerable<LoanTypeInfo> FetchLoanTypeAsync(this IStore<LoanType> Store)
    {
        return Store.ExecQueryAsync<LoanTypeInfo>("[dbo].[FetchLoanTypes]");
    }

    [MethodId("313F2142-7FF6-493D-89B8-EDB56579C70C")]
    public static async ValueTask<bool> VerifyLoanTypeReference(this IStore<LoanType> Store, long? LoanTypeId)
    {
        return await Store.Connection.ExecuteScalarAsync<bool>("SELECT [dbo].[VerifyLoanTypeReference](@LoanTypeId)", new { LoanTypeId });
    }

    [MethodId("54B586C2-74AC-4971-AAD1-854D6CA36AD4")]
    public static async ValueTask<LoanTypeInfo?> FetchLoanTypeByName(this IStore<LoanType> Store, string? Name)
    {
        return await Store.ExecFirstOrDefaultAsync<LoanTypeInfo?>("[dbo].[FetchLoanTypeByName]", new { Name });
    }

    [MethodId("16F6B292-7DEB-422D-90AC-7E46B49E296B")]
    public static IAsyncEnumerable<LoanType> FetchLoanTypeCompleteAsync(this IStore<LoanType> Store)
    {
        return Store.ExecQueryAsync<LoanType>("[dbo].[FetchLoanTypes]");
    }

    [MethodId("B7FFCBE6-9430-425D-846C-E0EA17AF48CD")]
    public static async ValueTask<LoanTypeInfo?> FetchLoanTypeByIdAsync(this IStore<LoanType> Store, long? LoanTypeId)
    {
        return await Store.ExecFirstOrDefaultAsync<LoanTypeInfo?>("[dbo].[FetchLoanTypeById]", new { LoanTypeId });
    }
}
