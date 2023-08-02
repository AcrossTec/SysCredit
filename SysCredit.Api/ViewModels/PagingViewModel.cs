namespace SysCredit.Api.ViewModels;

public class PagingViewModel : IViewModel
{
    public int Offset { get; set; }

    public int Limit { get; set; }
}
