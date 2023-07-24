namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.DataTransferObject;
using SysCredit.Api.Models;
using SysCredit.Api.ViewModels;

using static System.Data.CommandType;

public static class CustomerStore
{
    public static async IAsyncEnumerable<CustomerDataTransferObject> GetCustomersAsync(this IStore<Customer> Store)
    {
        foreach (var Customer in await Store.Connection.QueryAsync<CustomerDataTransferObject>("FetchCustomer", commandType: StoredProcedure))
        {
            yield return Customer;
        }
    }

    public static async ValueTask<CustomerDataTransferObject> AddCustomerAsync(this IStore<Customer> Store, CreateCustomer Customer)
    {
        var CustomerId = await Store.Connection.ExecuteScalarAsync<long>("InsertCustomer", Customer, commandType: StoredProcedure);
        return await Store.Connection.QueryFirstAsync<CustomerDataTransferObject>("FetchCustomer", commandType: StoredProcedure);
    }
}
