namespace SysCredit.Api.Helpers.Search;

using SysCredit.Api.Constants;

[Obsolete(SysCreditConstants.Empty, true)]
public record class SearchOptions<TViewModel>
{
    public string[] Search { get; set; } = { };

    public SearchOptionsProcessor<TViewModel> Processor()
    {
        return new SearchOptionsProcessor<TViewModel>(Search);
    }
}
