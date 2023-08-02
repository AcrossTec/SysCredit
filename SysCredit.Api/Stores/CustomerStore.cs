namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.DataTransferObject.StoredProcedures;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.Helpers;
using SysCredit.Api.Models;
using SysCredit.Api.ViewModels.Customers;

using System.Data;

using static Constants.ErrorCodeIndex;
using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(ErrorCategories.CustomerStore)]
public static class CustomerStore
{
    [MethodId("D767D480-09E4-4B08-BD31-11D24A599FAF")]
    public static async ValueTask<FetchCustomer?> FetchCustomerById(this IStore<Customer> Store, long? CustomerId)
    {
        return await Store.ExecFirstOrDefaultAsync<FetchCustomer?>("[dbo].[FetchCustomerById]", new { CustomerId });
    }

    [MethodId("39B222E4-EA19-4C38-9AD3-1E55843ADEDC")]
    public static async ValueTask<FetchCustomer?> FetchCustomerByIdentification(this IStore<Customer> Store, string? Identification)
    {
        return await Store.ExecFirstOrDefaultAsync<FetchCustomer?>("[dbo].[FetchCustomerByIdentification]", new { Identification });
    }

    [MethodId("C70ABA49-4546-481C-98F3-5C8C54D5225A")]
    public static async ValueTask<FetchCustomer?> FetchCustomerByEmail(this IStore<Customer> Store, string? Email)
    {
        return await Store.ExecFirstOrDefaultAsync<FetchCustomer?>("[dbo].[FetchCustomerByEmail]", new { Email });
    }

    [MethodId("7FC0C0C0-58AA-4724-97B9-FA96288688B6")]
    public static async ValueTask<FetchCustomer?> FetchCustomerByPhone(this IStore<Customer> Store, string? Phone)
    {
        return await Store.ExecFirstOrDefaultAsync<FetchCustomer?>("[dbo].[FetchCustomerByPhone]", new { Phone });
    }

    [MethodId("44AFFF21-7AB3-44D7-9E15-4A07D4352B63")]
    public static IAsyncEnumerable<FetchCustomer> FetchCustomersAsync(this IStore<Customer> Store)
    {
        return Store.ExecQueryAsync<FetchCustomer>("[dbo].[FetchCustomers]");
    }

    /// <summary>
    ///     How To Pass Array Or List To Stored Procedure
    ///     https://www.mytecbits.com/microsoft/sql-server/pass-array-or-list-to-stored-procedure
    /// </summary>
    /// <param name="Store"></param>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("5B53C4A1-4033-4778-A1A7-CB8144B52065")]
    [ErrorCode(Prefix: CustomerStorePrefix, Codes: new[] { _0001, _0002 })]
    public static async ValueTask<EntityId> InsertCustomerAsync(this IStore<Customer> Store, CreateCustomerRequest Request)
    {
        DynamicParameters Parameters = Request.ToDynamicParameters();
        Parameters.Add(nameof(Customer.CustomerId), default, DbType.Int64, ParameterDirection.Output);

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            await Store.ExecAsync("[dbo].[InsertCustomer]", Parameters, SqlTransaction);
            SqlTransaction.Commit();

            return Parameters.Get<long?>(nameof(Customer.CustomerId));
        }
        catch (Exception Ex)
        {
            // Handle the exception if the transaction fails to commit.
            SysCreditException SysCreditEx = Ex.ToSysCreditException(typeof(CustomerStore),
                "5B53C4A1-4033-4778-A1A7-CB8144B52065", CodeIndex0, "Error al registrar el cliente.", Ex);

            try
            {
                // Attempt to roll back the transaction.
                SqlTransaction.Rollback();
            }
            catch (Exception ExRollback)
            {
                // Throws an InvalidOperationException if the connection is closed or the transaction has already been rolled back on the server.
                SysCreditEx = ExRollback.ToSysCreditException(typeof(CustomerStore),
                    "5B53C4A1-4033-4778-A1A7-CB8144B52065", CodeIndex1, "Error interno del servidor al registrar el cliente.", SysCreditEx);
            }

            throw SysCreditEx;
        }
    }
}
