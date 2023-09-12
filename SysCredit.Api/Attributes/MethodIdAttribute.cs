namespace SysCredit.Api.Attributes;

/// <summary>
/// 
/// </summary>
/// <param name="MethodId"></param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class MethodIdAttribute(string MethodId) : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public readonly string MethodId = MethodId;
}
