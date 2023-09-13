namespace SysCredit.Api.Stores;

using Dapper;
using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.ViewModels.PaymentFrequencies;
using SysCredit.DataTransferObject.Commons;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Data;

using static Constants.ErrorCodes;
using System.Reflection;
using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(nameof(PaymentFrequencyStore))]
[ErrorCodePrefix(PaymentFrequencyStorePrefix)]
public static class PaymentFrequencyStore
{
    /// <summary>
    /// Este método ejecuta una consulta en una base de datos y 
    /// devuelve los resultados como un flujo de elementos de 
    /// tipo PaymentFrequencyInfo (DTO) a través de IAsyncEnumerable
    /// </summary>
    /// <param name="Store"></param>
    /// <returns></returns>
    [MethodId("2EF5FEB6-201C-4FF5-A70C-8D338B7241BD")]
    public static IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync(this IStore<PaymentFrequency> Store)
    {
        return Store.ExecQueryAsync<PaymentFrequencyInfo>("[dbo].[FetchPaymentFrequency]");
    }

    [MethodId("1A320F97-0E0C-4833-87B8-C35D546A8C4B")]
    public static async ValueTask<PaymentFrequencyInfo> FetchPaymentFrequencyByIdAsync(this IStore<PaymentFrequency> Store, long PaymentFrequencyId)
    {
        return await Store.ExecFirstOrDefaultAsync<PaymentFrequencyInfo>("[dbo].[FetchPaymentFrequencyById]", new { PaymentFrequencyId });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Store"></param>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("4CEA02D0-B5BF-4922-AA24-342F32386095")]
    public static async ValueTask<bool> UpdatePaymentFrequencyAsync(this IStore<PaymentFrequency> Store, UpdatePaymentFrequencyRequest Request)
    {
        using IDbTransaction SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            int Result = await Store.ExecAsync("[dbo].[UpdatePaymentFrequency]", Request, SqlTransaction);
            SqlTransaction.Commit();

            return Result > 0;

        }
        catch (Exception SqlEx)
        {
            SysCreditException SysCreditEx = SqlEx.ToSysCreditException(MethodInfo.GetCurrentMethod(), DATAPF0005);

            try
            {
                SqlTransaction.Rollback();
            }
            catch (Exception Ex)
            {

                throw Ex.ToSysCreditException(MethodInfo.GetCurrentMethod(), DATAPF0006);
            }

            throw SysCreditEx;
        }
    }

    [MethodId("016C0C42-BEB3-4821-A1B1-11E91C03BB27")]
    public static async ValueTask<PaymentFrequencyInfo?> FetchPaymentFrequencyByName(this IStore<PaymentFrequency> Store, string? Name)
    {
        return await Store.ExecFirstOrDefaultAsync<PaymentFrequencyInfo?>("[dbo].[FetchPaymentFrequencyByName]", new { Name });
    }

    /// <summary>
    /// Este método ejecuta una consulta en una base de datos y 
    /// devuelve los resultados como un flujo de elementos de 
    /// tipo PaymentFrequency a través de IAsyncEnumerable
    /// </summary>
    /// <param name="Store"></param>
    /// <returns></returns>
    [MethodId("2944DF4F-F5C7-41AC-B041-6BDF4CB7C443")]
    public static IAsyncEnumerable<PaymentFrequency> FetchPaymentFrequencyCompleteAsync(this IStore<PaymentFrequency> Store) 
    {
        return Store.ExecQueryAsync<PaymentFrequency>("[dbo].[FetchPaymentFrequency]");
    }

    public static async ValueTask<EntityId> InsertPaymentFrequencyAsync(this IStore<PaymentFrequency> Store, CreatePaymentFrequencyRequest Request)
    {
        DynamicParameters Parameters = Request.ToDynamicParameters();
        Parameters.Add(nameof(PaymentFrequency.PaymentFrequencyId), default, DbType.Int64, ParameterDirection.Output);

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            // Handle the exception if the transaction fails to commit.
            await Store.ExecAsync("[dbo].[InsertPaymentFrequency]", Parameters, SqlTransaction);
            SqlTransaction.Commit();

            return Parameters.Get<long?>(nameof(PaymentFrequency.PaymentFrequencyId));
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
}