using SysCredit.Api.ViewModels;

namespace SysCredit.Api.Helpers;

public static class ValidParams
{
    private static readonly List<string> Operators = new List<string> { "EQ", "GT", "GE", "LT", "LE" };
    private static readonly List<string> SqlOperators = new List<string> { "=", ">", ">=", "<", "<=" };
    private const string DESCENDING = "DESC";

    public static (bool IsValid, string WhereClause, string Message)
        IsValid(this IEnumerable<SearchTerm> SearchTerms, string[] Params)
    {
        if (!(Params.Count() == 3)) return (false, string.Empty, $"Invalid syntak, Params is empty");

        var SearchTerm = SearchTerms.FirstOrDefault(S =>
        S.Name.Equals(Params[0], StringComparison.OrdinalIgnoreCase));

        if (SearchTerm is null) return (false, string.Empty, $"Invalid Property Name: {Params[0]}");

        string? IsValidOperator = Operators.FirstOrDefault(O => O.Equals(Params[1], StringComparison.OrdinalIgnoreCase));

        if (IsValidOperator is null) return (false, string.Empty, $"Invalid Operator: {Params[1]}");

        int IndexOperator = Operators.IndexOf(IsValidOperator);

        if (SearchTerm.PropertyType.Equals("Decimal") && !IsDecimal(Params[2]))
            return (false, string.Empty, $"Invalid value for: {Params[0]}");

        return (true, $"{Params[0]} {SqlOperators[IndexOperator]} '{Params[2]}'", "Success");
    }

    public static (bool Success, string Message, string[] Props) BuildArrayForSearch(this String Query)
    {
        string[] SearchTermsArray = Query.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string[] Params = new string[3];

        if (SearchTermsArray.Length == 1) return (false, "Error you need to specify the operator", new string[] { });
        if (SearchTermsArray.Length == 2) return (false, "Error you need to specify the Value", new string[] { });

        for (int i = 2; i < SearchTermsArray.Length; i++)
        {
            Params[0] = SearchTermsArray[0];
            Params[1] = SearchTermsArray[1];

            Params[2] += SearchTermsArray[i] + " ";
        }

        return (true, "Succes", Params);
    }

    public static (bool Success, string Message, string[] Props) BuildArrayForSort(this String Query)
    {
        string[] SearchTermsArray = Query.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (SearchTermsArray.Length == 0) return (false, "Error you need to specify the Property", new string[] { });
        
        return (SearchTermsArray.Length == 2 && string.IsNullOrEmpty(SearchTermsArray[1])) ?
            (false, "Just you need specify Property or desc", new string[] { }) : (true, "Succes", SearchTermsArray);
    }

    public static (bool IsValid, string SortClause, string Message) IsValid(this IEnumerable<SortTerm> SortTerms, string[] Params)
    {
        if (Params.Length > 2) return (false, String.Empty, $"Invalid syntak, Params is empty");

        var SortTerm = SortTerms.FirstOrDefault(S => S.Name.Equals(Params[0], StringComparison.OrdinalIgnoreCase));

        if (SortTerm is null) return (false, string.Empty, $"Invalid Property Name: {Params[0]}");

        if (Params.Length == 2 && !Params[1].Equals(DESCENDING, StringComparison.OrdinalIgnoreCase)) 
            return (false, String.Empty, $"Invalid sort option {Params[1]}");
       
        return (Params.Length == 1) ? (true, $"{Params[0]} ASC", "Success") : (true, $"{Params[0]}", DESCENDING);
    }
    private static bool IsDecimal(string number)
    {
        return decimal.TryParse(number, out _);
    }
}