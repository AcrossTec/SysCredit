namespace SysCredit.Api.Helpers.Search;

using SysCredit.Api.Constants;
using SysCredit.Api.Helpers.Atributtes;
using SysCredit.Api.ViewModels;

using System.Reflection;

[Obsolete(SysCreditConstants.Empty, true)]
public class SearchOptionsProcessor<TViewModel>
{
    private readonly string[] Search;

    public SearchOptionsProcessor(string[] Search)
    {
        this.Search = Search;
    }

    private static IEnumerable<SearchTerm> GetTermsFromModel()
        => typeof(TViewModel).GetTypeInfo()
        .DeclaredProperties
        .Where(p => p.GetCustomAttributes<SearchableAttribute>().Any())
        .Select(p =>
        {
            var attribute = p.GetCustomAttribute<SearchableAttribute>();
            return new SearchTerm
            {
                Name = p.Name,
                EntityName = attribute!.EntityProperty,
                PropertyType = p.PropertyType.Name
            };
        });

    public (bool IsValid, string Value) Apply()
    {
        if (Search.Length == 0) return (true, "IsDelete = 0");

        var Params = Search[0].BuildArrayForSearch();

        if (!Params.Success) return (false, Params.Message);

        var Terms = GetTermsFromModel().IsValid(Params.Props);

        return (Terms.IsValid) ? (true, Terms.WhereClause) : (false, Terms.Message);
    }
}