namespace SysCredit.Api.Requests;

public record class PaginationRequest : IRequest
{
    public int? Offset { get; set; }

    public int? Limit { get; set; }
}
