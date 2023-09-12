namespace SysCredit.Api.Extensions;

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
    /// <param name="GenericTypeAttributeDefinition"></param>
    /// <returns></returns>
    public static Attribute? LookupGenericAttribute(this object @object, Type GenericTypeAttributeDefinition)
    {
        GenericTypeAttributeDefinition = GenericTypeAttributeDefinition.GetGenericTypeDefinition();

        var Query = from Attribute in @object.GetType().GetCustomAttributes()
                    let AttributeType = Attribute.GetType()
                    where AttributeType.IsGenericType
                       && AttributeType.GetGenericTypeDefinition() == GenericTypeAttributeDefinition
                    select Attribute;

        return Query.FirstOrDefault();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="object"></param>
    /// <param name="GenericTypeDefinition"></param>
    /// <returns></returns>
    public static Type[]? LookupGenericTypeArgumentsFromGenericAttribute(this object @object, Type GenericTypeDefinition)
    {
        return @object.LookupGenericAttribute(GenericTypeDefinition)?.GetType().GenericTypeArguments;
    }
}
