namespace SysCredit.Api.Extensions;

using SysCredit.Api.Attributes;

using System.Reflection;

/// <summary>
/// 
/// </summary>
public static class AttributeExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="object"></param>
    /// <param name="GenericTypeDefinition"></param>
    /// <returns></returns>
    public static Attribute? SearchGenericAttribute(this object @object, Type GenericTypeDefinition)
    {
        GenericTypeDefinition = GenericTypeDefinition.GetGenericTypeDefinition();

        var Query = from Attribute in @object.GetType().GetCustomAttributes()
                    let AttributeType = Attribute.GetType()
                    where AttributeType.IsGenericType && AttributeType.GetGenericTypeDefinition() == GenericTypeDefinition
                    select Attribute;

        return Query.FirstOrDefault();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="object"></param>
    /// <param name="GenericTypeDefinition"></param>
    /// <returns></returns>
    public static Type[]? SearchGenericTypeArgumentsFromGenericAttribute(this object @object, Type GenericTypeDefinition)
    {
        return @object.SearchGenericAttribute(GenericTypeDefinition)?.GetType().GenericTypeArguments;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="MethodId"></param>
    /// <param name="CodeIndex"></param>
    /// <returns></returns>
    public static string? GetErrorCode(this Type Type, string MethodId, int CodeIndex)
    {
        var ErrorCodeAttributes =
            from Method in Type.GetMethods()
            let MethodAttribute = Method.GetCustomAttribute<MethodIdAttribute>()
            where MethodAttribute is not null && MethodAttribute.MethodId == MethodId
            select Method.GetCustomAttribute<ErrorCodeAttribute>();

        return ErrorCodeAttributes.SingleOrDefault()?.GetErrorCode(CodeIndex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static string? GetErrorCategory(this Type Type)
    {
        return Type.GetCustomAttribute<ErrorCategoryAttribute>()?.Category;
    }
}
