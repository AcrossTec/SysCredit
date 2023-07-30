namespace SysCredit.Api.ViewModels;

using SysCredit.Api.Constants;

[Obsolete(SysCreditConstants.Empty, true)]
public class SortTerm
{
    public string Name { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public bool Descending { get; set; }

    public bool Default { get; set; } = false;
}