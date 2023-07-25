namespace SysCredit.Api.Helpers.Atributtes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SortableAttribute : Attribute
{
    public string EntityProperty { get; set; } = string.Empty;

    public bool Default { get; set; }
}
