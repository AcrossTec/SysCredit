namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.Models;
using SysCredit.Api.ViewModels.Guarantors;

using System.Data;
using System.Data.SqlClient;

using static Constants.ErrorCodePrefix;
using static Constants.ErrorCodeNumber;
using System.Reflection;
using SysCredit.Api.Helpers;
using SysCredit.Api.DataTransferObject;

[Store]
[ErrorCategory(ErrorCategories.GuarantorStore)]
public static class GuarantorStore
{
    public static IAsyncEnumerable<GuarantorDataTransferObject> FetchGuarantorsAsync(this IStore<Guarantor> Store)
    {
        return Store.ExecQueryAsync<GuarantorDataTransferObject>("[dbo].[FetchGuarantors]");
    }

    [ErrorCode(Prefix = GuarantorStorePrefix, Codes = new[] { _0001, _0002 })]
    public static async ValueTask<EntityId> InsertGuarantorAsync(this IStore<Guarantor> Store, CreateGuarantorViewModel ViewModel)
    {
        DynamicParameters Parameters = new DynamicParameters(ViewModel);
        Parameters.Add(nameof(Guarantor.GuarantorId), dbType: DbType.UInt64, direction: ParameterDirection.Output);

        SqlTransaction Transaction = Store.BeginTransaction();

        try
        {
            await Store.ExecAsync("[dbo].[InsertGuarantor]", Parameters, Transaction);
            return Parameters.Get<long?>(nameof(Guarantor.GuarantorId));
        }
        catch (Exception Ex)
        {
            // Handle the exception if the transaction fails to commit.
            SysCreditException SysCreditEx = new SysCreditException(Ex.Message, Ex);
            SysCreditEx.Status.ErrorMessage = "Error al registrar el fiador.";
            SysCreditEx.Status.ErrorCategory = MethodInfo.GetCurrentMethod()!.GetErrorCategory();
            SysCreditEx.Status.ErrorCode = MethodInfo.GetCurrentMethod()!.GetErrorCode(0);
            SysCreditEx.Status.Errors!.Add(nameof(Ex.Message), Ex.GetMessages().ToArray());

            try
            {
                // Attempt to roll back the transaction.
                Transaction.Rollback();
            }
            catch (Exception ExRollback)
            {
                // Throws an InvalidOperationException if the connection
                // is closed or the transaction has already been rolled
                // back on the server.

                SysCreditEx = new SysCreditException(Ex.Message, SysCreditEx);
                SysCreditEx.Status.ErrorMessage = "Error interno del servidor al registrar el fiador.";
                SysCreditEx.Status.ErrorCategory = MethodInfo.GetCurrentMethod()!.GetErrorCategory();
                SysCreditEx.Status.ErrorCode = MethodInfo.GetCurrentMethod()!.GetErrorCode(1);
                SysCreditEx.Status.Errors!.Add(nameof(ExRollback.Message), ExRollback.GetMessages().ToArray());
            }

            throw SysCreditEx;
        }
    }
}
