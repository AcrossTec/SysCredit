namespace SysCredit.Api.Helpers.Atributtes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SearchableAttribute : Attribute
{
    public string EntityProperty { get; set; } = string.Empty;

}