namespace SysCredit.Api.Extensions;

using SysCredit.Api.Attributes;

using System.Reflection;

/// <summary>
/// 
/// </summary>
public static class ReflectionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="MethodInfo"></param>
    /// <returns></returns>
    public static string? GetMethodId(this MethodBase? MethodInfo)
    {
        return MethodInfo?.GetCustomAttribute<MethodIdAttribute>()?.MethodId;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MethodInfo"></param>
    /// <returns></returns>
    public static string? GetErrorCategory(this MethodBase? MethodInfo)
    {
        return MethodInfo?.DeclaringType.GetErrorCategory();
    }
}
