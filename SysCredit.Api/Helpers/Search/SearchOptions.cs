namespace SysCredit.Api.Helpers.Search;

public record class SearchOptions<TViewModel>
{
    public string[] Search { get; set; } = { };

    public SearchOptionsProcessor<TViewModel> Processor()
    {
        return new SearchOptionsProcessor<TViewModel>(Search);
    }
}
