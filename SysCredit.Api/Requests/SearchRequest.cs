namespace SysCredit.Api.Requests;

public record class SearchRequest : PaginationRequest
{
    public string? Value { get; set; }
}
