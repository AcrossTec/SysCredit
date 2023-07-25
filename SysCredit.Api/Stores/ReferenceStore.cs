using SysCredit.Api.Helpers.Pagination;
using SysCredit.Api.Models;
using Dapper;
using static System.Data.CommandType;
using SysCredit.Api.ViewModels.Reference;
using SysCredit.Api.ViewModels;

namespace SysCredit.Api.Stores;

public static class ReferenceStore
{
    public static async ValueTask<ReferenceOption> AddRelationShipAsync(this IStore<Reference> Store, CreateReference Customer)
    {
        var CustomerId = await Store.Connection.ExecuteScalarAsync<long>(
            "InsertCustomer",
            Customer,
            commandType: StoredProcedure);

        var RelationshipResult = await Store.Connection.QueryFirstAsync<ReferenceOption>(
            "FetchCustomerById",
            new { CustomerId },
            commandType: StoredProcedure);

        return RelationshipResult;
    }

    public static async ValueTask<PagedResults<ReferenceOption>> GetRelationshipsAsync(
        this IStore<Reference> Store,
        PagingOptions PaginOptions,
        string QueryForSort,
        string QueryForSearch)
    {
        var Customers = (await Store.Connection.QueryAsync<ReferenceOption>(
        "FetchReferences",
           new
           {
               Offset = PaginOptions.Offset!.Value,
               Limit = PaginOptions.Limit!.Value,
               OrderBy = QueryForSort,
               SearchBy = QueryForSearch
           },
           commandType: StoredProcedure)).ToAsyncEnumerable();

        return new PagedResults<ReferenceOption>
        {
            Items = Customers
        };
    }

    public static async ValueTask InsertMany(this IStore<Reference> Store, IEnumerable<CustomerReference> SetReferences, IEnumerable<long> CustomerId)
    => await Store.Connection.ExecuteAsync("INSERT INTO CustomerReference(CustomerId, ReferenceId) VALUES(@CustomerId, @ReferenceId)", new { CustomerId, SetReferences });
}
