namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.ViewModels.LoanType;

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
    [MethodId("9D9648AF-EE89-4B08-9B6E-96016C086D3F")]
    public static IAsyncEnumerable<LoanTypeInfo> FetchLoanTypeAsync(this IStore<LoanType> Store)
    {
        return Store.ExecQueryAsync<LoanTypeInfo>("[dbo].[FetchLoanTypes]");
    }

    [MethodId("54b586c2-74ac-4971-aad1-854d6ca36ad4")]
    public static async ValueTask<LoanTypeInfo?> FetchLoanTypeByName(this IStore<LoanType> Store, string? Name)
    {
        return await Store.ExecFirstOrDefaultAsync<LoanTypeInfo?>("[dbo].[FetchLoanTypeByName]", new { Name });
    }

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
                SysCreditEx = ExRollback.ToSysCreditException(typeof(CustomerStore),
                    "84370664-26c5-45ed-9590-6df60a9efac4", CodeIndex1, "Error interno del servidor al registrar el LoanType.", SysCreditEx);
            }

            throw SysCreditEx;
        }
    }

    [MethodId("16f6b292-7deb-422d-90ac-7e46b49e296b")]
    public static IAsyncEnumerable<LoanType> FetchLoanTypeCompleteAsync(this IStore<LoanType> Store)
    {
        return Store.ExecQueryAsync<LoanType>("[dbo].[FetchLoanTypes]");
    }
}
