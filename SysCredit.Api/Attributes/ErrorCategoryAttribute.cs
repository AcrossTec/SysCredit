namespace SysCredit.Api.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ErrorCategoryAttribute : Attribute
{
    public ErrorCategoryAttribute(string Category) => this.Category = Category;

    public string Category { get; }
}
