namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.DataTransferObject.Commons;
using SysCredit.Api.DataTransferObject.StoredProcedures;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.Helpers;
using SysCredit.Api.Models;
using SysCredit.Api.ViewModels.Guarantors;

using System.Data;

using static Constants.ErrorCodeIndex;
using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(ErrorCategories.GuarantorStore)]
public static class GuarantorStore
{
    [MethodId("D094D436-1107-4455-9D8D-EA82683A319F")]
    public static async ValueTask<bool> ExistsGuarantorAsync(this IStore<Guarantor> Store, long GuarantorId)
    {
        var Guarantor = await Store.FetchGuarantorById(GuarantorId);
        return Guarantor is not null;
    }

    [MethodId("078DDE01-E89D-44CB-8026-7C05D300EEAC")]
    public static async ValueTask<FetchGuarantor?> FetchGuarantorById(this IStore<Guarantor> Store, long? GuarantorId)
    {
        return await Store.ExecFirstOrDefaultAsync<FetchGuarantor?>("[dbo].[FetchGuarantorById]", new { GuarantorId });
    }

    [MethodId("564BA87E-6767-4EA7-86F6-924EDDE109DE")]
    public static async ValueTask<FetchGuarantor?> FetchGuarantorByIdentification(this IStore<Guarantor> Store, string? Identification)
    {
        return await Store.ExecFirstOrDefaultAsync<FetchGuarantor?>("[dbo].[FetchGuarantorByIdentification]", new { Identification });
    }

    [MethodId("B147024A-54AF-4B79-91D5-9D7E8C7336E6")]
    public static async ValueTask<FetchGuarantor?> FetchGuarantorByEmail(this IStore<Guarantor> Store, string? Email)
    {
        return await Store.ExecFirstOrDefaultAsync<FetchGuarantor?>("[dbo].[FetchGuarantorByEmail]", new { Email });
    }

    [MethodId("23168FC2-1967-4FC3-AD88-0A6FC2B29827")]
    public static async ValueTask<FetchGuarantor?> FetchGuarantorByPhone(this IStore<Guarantor> Store, string? Phone)
    {
        return await Store.ExecFirstOrDefaultAsync<FetchGuarantor?>("[dbo].[FetchGuarantorByPhone]", new { Phone });
    }

    [MethodId("190F125C-971A-4C67-A4FD-0C6187246707")]
    public static IAsyncEnumerable<FetchGuarantor> FetchGuarantorsAsync(this IStore<Guarantor> Store)
    {
        return Store.ExecQueryAsync<FetchGuarantor>("[dbo].[FetchGuarantors]");
    }

    [MethodId("BAEC4217-08E5-4714-BD80-D2C37696BB45")]
    [ErrorCode(Prefix: GuarantorStorePrefix, Codes: new[] { _0001, _0002 })]
    public static async ValueTask<EntityId> InsertGuarantorAsync(this IStore<Guarantor> Store, CreateGuarantorRequest Request)
    {
        DynamicParameters Parameters = new DynamicParameters(Request);
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
            SysCreditException SysCreditEx = Ex.ToSysCreditException(typeof(GuarantorStore),
                "BAEC4217-08E5-4714-BD80-D2C37696BB45", CodeIndex0, "Error al registrar el fiador.", Ex);

            try
            {
                // Attempt to roll back the transaction.
                SqlTransaction.Rollback();
            }
            catch (Exception ExRollback)
            {
                // Throws an InvalidOperationException if the connection is closed or the transaction has already been rolled back on the server.
                SysCreditEx = ExRollback.ToSysCreditException(typeof(GuarantorStore),
                    "BAEC4217-08E5-4714-BD80-D2C37696BB45", CodeIndex1, "Error interno del servidor al registrar el fiador.", SysCreditEx);
            }

            throw SysCreditEx;
        }
    }
}
