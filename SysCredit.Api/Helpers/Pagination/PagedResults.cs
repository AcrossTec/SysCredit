namespace SysCredit.Api.Helpers.Pagination;

public class PagedResults<T>
{
    public IAsyncEnumerable<T> Items { get; set; } = AsyncEnumerable.Empty<T>();
}
