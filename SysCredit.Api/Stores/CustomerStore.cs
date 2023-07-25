using Dapper;
using SysCredit.Api.Helpers.Pagination;
using SysCredit.Api.Models;
using SysCredit.Api.ViewModels;
using SysCredit.Api.ViewModels.Customer;
using static Dapper.SqlMapper;
using static System.Data.CommandType;

namespace SysCredit.Api.Stores;

public static class CustomerStore
{
    public static async ValueTask<PagedResults<CustomerOptions>> GetCustomersAsync(
        this IStore<Customer> Store, 
        PagingOptions PaginOptions,
        string QueryForSort, 
        string QueryForSearch)
    {
        var Customers = (await Store.Connection.QueryAsync<CustomerOptions>(
           "FetchCustomer",
           new
           {
               Offset = PaginOptions.Offset!.Value,
               Limit = PaginOptions.Limit!.Value, 
               OrderBy = QueryForSort, 
               SearchBy = QueryForSearch
           },
           commandType: StoredProcedure)).ToAsyncEnumerable();

        return new PagedResults<CustomerOptions>
        {
            Items = Customers
        };
    }

    public static async ValueTask<CustomerOptions> AddCustomerAsync(
        this IStore<Customer> Store, 
        CreateCustomer Customer)
    {
        var CustomerId = await Store.Connection.ExecuteScalarAsync<long>(
            "InsertCustomer", 
            Customer, 
            commandType: StoredProcedure);
        
        var CustomerResult = await Store.Connection.QueryFirstAsync<CustomerOptions>(
            "FetchCustomerById", 
            new { CustomerId }, 
            commandType: StoredProcedure);

        return CustomerResult;
    }
}
