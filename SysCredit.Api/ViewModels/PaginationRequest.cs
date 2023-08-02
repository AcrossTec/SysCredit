namespace SysCredit.Api.ViewModels;

public class PaginationRequest : IViewModel
{
    public int Offset { get; set; }

    public int Limit { get; set; }
}
