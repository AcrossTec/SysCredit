namespace SysCredit.Api.ViewModels;

public record class SearchRequest : PaginationRequest
{
    public string? Value { get; set; }
}
