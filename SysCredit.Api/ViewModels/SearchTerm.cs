namespace SysCredit.Api.ViewModels;

using SysCredit.Api.Constants;

[Obsolete(SysCreditConstants.Empty, true)]
public class SearchTerm
{
    public string Name { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public string Operator { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public bool ValidSyntax { get; set; }

    public string PropertyType { get; set; } = string.Empty;
}