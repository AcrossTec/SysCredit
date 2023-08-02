namespace SysCredit.Api.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class MethodIdAttribute : Attribute
{
    public MethodIdAttribute(string MethodId) => this.MethodId = MethodId;

    public string MethodId { get; }
}
