namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.DataTransferObject;
using SysCredit.Api.Models;
using SysCredit.Api.ViewModels;

using static System.Data.CommandType;

public static class CustomerStore
{
    public static IAsyncEnumerable<CustomerDataTransferObject> GetCustomersAsync(this IStore<Customer> Store)
    {
        return Store.ExecQueryAsync<CustomerDataTransferObject>("FetchCustomer");
    }

    public static async ValueTask<CustomerDataTransferObject> AddCustomerAsync(this IStore<Customer> Store, CreateCustomer Customer)
    {
        var DynamicParameters = new DynamicParameters();

        Customer Model = Store.ToModel(Customer)!;
        DynamicParameters.AddDynamicParams(Model);
        DynamicParameters.Output(Model, M => M.CustomerId);

        var CustomerId = await Store.ExecScalarAsync<long>("InsertCustomer", DynamicParameters);
        return await Store.ExecFirstAsync<CustomerDataTransferObject>("FetchCustomer");
    }
}
