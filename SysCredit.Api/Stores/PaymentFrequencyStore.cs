namespace SysCredit.Api.Stores;

using Dapper;
using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.ViewModels.PaymentFrequencys;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;
using System.Data;
using System.Reflection;
using static Constants.ErrorCodes;
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

    public static async ValueTask<PaymentFrequencyInfo?> FetchPaymentFrequencyByName(this IStore<PaymentFrequency> Store, string? Name)
    {
        return await Store.ExecFirstOrDefaultAsync<PaymentFrequencyInfo?>("[dbo].[FetchPaymenFrequencyByName]", new { Name });
    }

}