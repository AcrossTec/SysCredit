namespace SysCredit.Api.Extensions;

using SysCredit.Api.Attributes;

using System.Reflection;

/// <summary>
/// 
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static Type GetUnderlyingType(this Type Type)
    {
        static Type NullableGetUnderlyingType(Type Type) => Nullable.GetUnderlyingType(Type) ?? Type;

        if (Type.IsEnum)
        {
            return Enum.GetUnderlyingType(NullableGetUnderlyingType(Type));
        }

        return NullableGetUnderlyingType(Type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static string? GetErrorCategory(this Type? Type)
    {
        return Type?.GetCustomAttribute<ErrorCategoryAttribute>()?.Category;
    }
}
