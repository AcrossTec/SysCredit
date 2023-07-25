namespace SysCredit.Api.Helpers.Sorting;

public record class SortOptions<TViewModel>
{
    public string[] OrderBy { get; set; } = { };

    public SortOptionsProcessor<TViewModel> Processor()
    {
        return new SortOptionsProcessor<TViewModel>(OrderBy);
    }
}
