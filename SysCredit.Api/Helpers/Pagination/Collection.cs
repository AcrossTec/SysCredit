using SysCredit.Api.DataTransferObject;
using SysCredit.Api.ViewModels;

namespace SysCredit.Api.Helpers.Pagination;

public record class Collection<T> : Response<PagingOptions>
    where T : class
{
    public IAsyncEnumerable<T> Data { get; set; } = AsyncEnumerable.Empty<T>();
}
