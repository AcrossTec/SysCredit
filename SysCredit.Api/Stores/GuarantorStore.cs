using Dapper;
using SysCredit.Api.Helpers.Pagination;
using SysCredit.Api.Models;
using SysCredit.Api.ViewModels;
using SysCredit.Api.ViewModels.Guarantor;
using static System.Data.CommandType;

namespace SysCredit.Api.Stores;

public static class GuarantorStore
{
    public static async ValueTask<PagedResults<GuarantorOption>> GetGuarantorsAsync(
        this IStore<Guarantor> Store,
        PagingOptions PaginOptions,
        string QueryForSort,
        string QueryForSearch)
    {
        var Guarantors = (await Store.Connection.QueryAsync<GuarantorOption>(
            "FetchGuarantor",
            new
            {
                Offset = PaginOptions.Offset!.Value,
                Limit = PaginOptions.Limit!.Value,
                OrderBy = QueryForSort,
                SearchBy = QueryForSearch
            }, commandType: StoredProcedure)).ToAsyncEnumerable();


        return new PagedResults<GuarantorOption>
        {
            Items = Guarantors
        };
    }

    public static async ValueTask<GuarantorOption> AddGuarantorAsync(
        this IStore<Guarantor> Store, CreateGuarantor Guarantor)
    {
        var GuarantorId = await Store.Connection.ExecuteScalarAsync<long>(
            "InsertGuarantor", 
            Guarantor, 
            commandType: StoredProcedure);

        return await Store.Connection.QueryFirstAsync<GuarantorOption>(
            "FetchGuarantorById",
            new { GuarantorId },
            commandType: StoredProcedure);
    }

    public static async ValueTask InsertMany(this IStore<Guarantor> Store, IEnumerable<ViewModels.Guarantor.CustomerGuarantor> SetGuarantor, IEnumerable<long> CustomerId)
    => await Store.Connection.ExecuteAsync("INSERT INTO CustomerGuarantor(CustomerId, GuarantorId) VALUES(@CustomerId, @GuarantorId)", new {CustomerId, SetGuarantor }, commandType: StoredProcedure);
}
