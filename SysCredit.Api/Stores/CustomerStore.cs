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
[ErrorCode(Prefix = CustomerStorePrefix, Codes = new[] { _0001, _0002, _0003 })]
public static class CustomerStore
{
    public static IAsyncEnumerable<FetchCustomer> FetchCustomersAsync(this IStore<Customer> Store)
    {
        return Store.ExecQueryAsync<FetchCustomer>("[dbo].[FetchCustomers]");
    }

    public static async ValueTask<EntityId> InsertCustomerAsync(this IStore<Customer> Store, CreateCustomerRequest ViewModel)
    {
        DynamicParameters Parameters = new DynamicParameters(ViewModel);
        Parameters.Add(nameof(Customer.CustomerId), dbType: DbType.Int64, direction: ParameterDirection.Output);

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            await Store.ExecAsync("[dbo].[InsertCustomer]", Parameters, SqlTransaction);
            SqlTransaction.Commit();

            return Parameters.Get<long?>(nameof(Guarantor.GuarantorId));
        }
        catch (Exception Ex)
        {
            // Handle the exception if the transaction fails to commit.
            SysCreditException SysCreditEx = new SysCreditException(Ex.Message, Ex);
            SysCreditEx.Status.ErrorMessage = "Error al registrar el cliente.";
            SysCreditEx.Status.ErrorCategory = typeof(CustomerStore).GetErrorCategory();
            SysCreditEx.Status.ErrorCode = typeof(CustomerStore).GetErrorCode(CodeIndex0);
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
                SysCreditEx.Status.ErrorMessage = "Error interno del servidor al registrar el cliente.";
                SysCreditEx.Status.ErrorCategory = typeof(CustomerStore).GetErrorCategory();
                SysCreditEx.Status.ErrorCode = typeof(CustomerStore).GetErrorCode(CodeIndex1);
                SysCreditEx.Status.Errors.Add(nameof(ExRollback.Message), ExRollback.GetMessages().ToArray());
            }

            throw SysCreditEx;
        }
    }
}
