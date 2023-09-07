namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.ViewModels.LoanType;
using SysCredit.Api.ViewModels.LoanTypes;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Data;

using static Constants.ErrorCodeIndex;
using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(ErrorCategories.LoanTypeStore)]
public static class LoanTypeStore
{
    [MethodId("84370664-26c5-45ed-9590-6df60a9efac4")]
    [ErrorCode(Prefix: LoanTypeStorePrefix, Codes: new[] { _0001, _0002 })]
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
        catch (Exception Ex)
        {
            SysCreditException SysCreditEx = Ex.ToSysCreditException(typeof(LoanTypeStore),
                "84370664-26c5-45ed-9590-6df60a9efac4", CodeIndex0, "Error al registar el LoanType", Ex);

            try
            {
                // Attempt to roll back the transaction.
                SqlTransaction.Rollback();
            }
            catch (Exception ExRollback)
            {
                // Throws an InvalidOperationException if the connection is closed or the transaction has already been rolled back on the server.
                SysCreditEx = ExRollback.ToSysCreditException(typeof(LoanTypeStore),
                    "84370664-26c5-45ed-9590-6df60a9efac4", CodeIndex1, "Error interno del servidor al registrar el LoanType.", SysCreditEx);
            }

            throw SysCreditEx;
        }
    }

    [MethodId("FC5C3FA3-E112-49D7-B5EE-37181239695C")]
    [ErrorCode(Prefix: LoanTypeStorePrefix, Codes: new[] { _0003, _0004 })]
    public static async ValueTask<bool> DeleteLoanTypeAsync(this IStore<LoanType> Store, DeleteLoanTypeRequest Request)
    {
        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            int Result = await Store.ExecAsync("[dbo].[DeleteLoanType]", Request, SqlTransaction);
            SqlTransaction.Commit();

            return Result > 0;
        }
        catch (Exception Ex)
        {
            SysCreditException SysCreditEx = Ex.ToSysCreditException(typeof(LoanTypeStore),
                "FC5C3FA3-E112-49D7-B5EE-37181239695C", CodeIndex0, "Error al eliminar el Tipo de Prestamo", Ex); ;

            try
            {
                SqlTransaction.Rollback();
            }
            catch (Exception ExRollback)
            {
                SysCreditEx = ExRollback.ToSysCreditException(typeof(LoanTypeStore),
                    "FC5C3FA3-E112-49D7-B5EE-37181239695C", CodeIndex1, "Error interno del servidor al eliminar el Tipo de Prestamo", SysCreditEx);
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

    [MethodId("C367398E-F4F3-4350-86A5-AE2B3DBEBED7")]
    public static async ValueTask<EntityId?> UpdateLoanTypeAsync(this IStore<LoanType> Store, UpdateLoanTypeRequest Request)
    {
        DynamicParameters Parameters = Request.ToDynamicParameters();
        Parameters.Add(nameof(LoanType.LoanTypeId), default, DbType.Int64, ParameterDirection.Output);

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            await Store.ExecAsync("[dbo].[UpdateLoanTypeById]", Parameters, SqlTransaction);
            SqlTransaction.Commit();

            return Parameters.Get<long?>(nameof(LoanType.LoanTypeId));
        }
        catch (Exception Ex) 
        {
            SysCreditException SysCreditEx = Ex.ToSysCreditException(typeof(LoanTypeStore),
                "C367398E-F4F3-4350-86A5-AE2B3DBEBED7", CodeIndex0, "Error al modificar el Tipo de Prestamo", Ex);

            try
            {
                SqlTransaction.Rollback();
            }
            catch (Exception ExRollback)
            {
                SysCreditEx = ExRollback.ToSysCreditException(typeof(LoanTypeStore),
                    "C367398E-F4F3-4350-86A5-AE2B3DBEBED7", CodeIndex1, "Error interno del servidor al registrar el Tipo de Prestamo", SysCreditEx);
            }

            throw SysCreditEx;
        }
    }
}
