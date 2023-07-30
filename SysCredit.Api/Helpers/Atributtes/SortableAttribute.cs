namespace SysCredit.Api.Helpers.Atributtes;

using SysCredit.Api.Constants;

[Obsolete(SysCreditConstants.Empty, true)]
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SortableAttribute : Attribute
{
    public string EntityProperty { get; set; } = string.Empty;

    public bool Default { get; set; }
}
