namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.DataTransferObject;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.Helpers;
using SysCredit.Api.Models;
using SysCredit.Api.ViewModels.Guarantors;

using System.Data;
using System.Data.SqlClient;

using static Constants.ErrorCodeIndex;
using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(ErrorCategories.GuarantorStore)]
[ErrorCode(Prefix = GuarantorStorePrefix, Codes = new[] { _0001, _0002 })]
public static class GuarantorStore
{
    public static async ValueTask<GuarantorDataTransferObject?> FetchGuarantorById(this IStore<Guarantor> Store, long? GuarantorId)
    {
        return await Store.ExecFirstOrDefaultAsync<GuarantorDataTransferObject?>("[dbo].[FetchGuarantorById]", new { GuarantorId });
    }

    public static async ValueTask<GuarantorDataTransferObject?> FetchGuarantorByIdentification(this IStore<Guarantor> Store, string? Identification)
    {
        return await Store.ExecFirstOrDefaultAsync<GuarantorDataTransferObject?>("[dbo].[FetchGuarantorByIdentification]", new { Identification });
    }

    public static async ValueTask<GuarantorDataTransferObject?> FetchGuarantorByEmail(this IStore<Guarantor> Store, string? Email)
    {
        return await Store.ExecFirstOrDefaultAsync<GuarantorDataTransferObject?>("[dbo].[FetchGuarantorByEmail]", new { Email });
    }

    public static async ValueTask<GuarantorDataTransferObject?> FetchGuarantorByPhone(this IStore<Guarantor> Store, string? Phone)
    {
        return await Store.ExecFirstOrDefaultAsync<GuarantorDataTransferObject?>("[dbo].[FetchGuarantorByPhone]", new { Phone });
    }

    public static IAsyncEnumerable<GuarantorDataTransferObject> FetchGuarantorsAsync(this IStore<Guarantor> Store)
    {
        return Store.ExecQueryAsync<GuarantorDataTransferObject>("[dbo].[FetchGuarantors]");
    }

    public static async ValueTask<EntityId> InsertGuarantorAsync(this IStore<Guarantor> Store, CreateGuarantorViewModel ViewModel)
    {
        DynamicParameters Parameters = new DynamicParameters(ViewModel);
        Parameters.Add(nameof(Guarantor.GuarantorId), dbType: DbType.Int64, direction: ParameterDirection.Output);

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            await Store.ExecAsync("[dbo].[InsertGuarantor]", Parameters, SqlTransaction);
            SqlTransaction.Commit();

            return Parameters.Get<long?>(nameof(Guarantor.GuarantorId));
        }
        catch (Exception Ex)
        {
            // Handle the exception if the transaction fails to commit.
            SysCreditException SysCreditEx = new SysCreditException(Ex.Message, Ex);
            SysCreditEx.Status.ErrorMessage = "Error al registrar el fiador.";
            SysCreditEx.Status.ErrorCategory = typeof(GuarantorStore).GetErrorCategory();
            SysCreditEx.Status.ErrorCode = typeof(GuarantorStore).GetErrorCode(CodeIndex0);
            SysCreditEx.Status.Errors.Add(nameof(Ex.Message), Ex.GetMessages().ToArray());

            try
            {
                // Attempt to roll back the transaction.
                SqlTransaction.Rollback();
            }
            catch (Exception ExRollback)
            {
                // Throws an InvalidOperationException if the connection
                // is closed or the transaction has already been rolled
                // back on the server.

                SysCreditEx = new SysCreditException(Ex.Message, SysCreditEx);
                SysCreditEx.Status.ErrorMessage = "Error interno del servidor al registrar el fiador.";
                SysCreditEx.Status.ErrorCategory = typeof(GuarantorStore).GetErrorCategory();
                SysCreditEx.Status.ErrorCode = typeof(GuarantorStore).GetErrorCode(CodeIndex1);
                SysCreditEx.Status.Errors.Add(nameof(ExRollback.Message), ExRollback.GetMessages().ToArray());
            }

            throw SysCreditEx;
        }
    }
}
