namespace SysCredit.Api.Helpers.Sorting;

using SysCredit.Api.Constants;

[Obsolete(SysCreditConstants.Empty, true)]
public record class SortOptions<TViewModel>
{
    public string[] OrderBy { get; set; } = { };

    public SortOptionsProcessor<TViewModel> Processor()
    {
        return new SortOptionsProcessor<TViewModel>(OrderBy);
    }
}
