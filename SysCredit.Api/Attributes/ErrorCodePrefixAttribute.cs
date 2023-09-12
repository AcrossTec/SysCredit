namespace SysCredit.Api.Attributes;

/// <summary>
/// 
/// </summary>
/// <param name="Prefix"></param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ErrorCodePrefixAttribute(string Prefix) : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public readonly string Prefix = Prefix;
}
