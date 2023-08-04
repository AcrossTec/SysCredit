namespace SysCredit.Api.ViewModels;

public record class PaginationRequest : IViewModel
{
    public int? Offset { get; set; }

    public int? Limit { get; set; }
}
