using SysCredit.Api.Helpers;
using SysCredit.Api.Helpers.Atributtes;
using SysCredit.Api.ViewModels;
using System.Reflection;

public class SortOptionsProcessor<TViewModel>
{
    private readonly string[] OrderBy;

    public SortOptionsProcessor(string[] OrderBy)
    {
        this.OrderBy = OrderBy;
    }

    private static IEnumerable<SortTerm> GetTermsFromModel()
        => typeof(TViewModel).GetTypeInfo()
        .DeclaredProperties
        .Where(p => p.GetCustomAttributes<SortableAttribute>().Any())
        .Select(p => new SortTerm
        {
            Name = p.Name,
            EntityName = p.GetCustomAttribute<SortableAttribute>()!.EntityProperty,
            Default = p.GetCustomAttribute<SortableAttribute>()!.Default
        });

    public (bool IsValid, string Value) Apply()
    {
        var DefaultTerm = GetTermsFromModel().FirstOrDefault(D => D.Default = true);

        if (DefaultTerm is null) return (false, "Specify the default sort");

        if (OrderBy.Length == 0) return (true, $"{DefaultTerm.Name} ASC");
        
        var Params = OrderBy[0].BuildArrayForSort();
        
        if (!Params.Success) return (false, Params.Message);

        var Result = GetTermsFromModel().IsValid(Params.Props);

        if (!Result.IsValid) return (false, Result.Message);

        return (true, Result.SortClause);
    }
}
