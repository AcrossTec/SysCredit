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
    /// <param name="GenericAttributeTypeDefinition"></param>
    /// <returns></returns>
    public static Attribute? LookupGenericAttribute(this object @object, Type GenericAttributeTypeDefinition)
    {
        var Type = @object switch
        {
            Type T => T,
            _ => @object.GetType()
        };

        var GenericTypeDefinition = GenericAttributeTypeDefinition.GetGenericTypeDefinition();

        var Query = from Attribute in Type.GetCustomAttributes()
                    let AttributeType = Attribute.GetType()
                    where AttributeType.IsGenericType
                       && AttributeType.GetGenericTypeDefinition() == GenericTypeDefinition
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
