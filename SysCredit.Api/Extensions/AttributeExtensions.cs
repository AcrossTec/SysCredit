namespace SysCredit.Api.Extensions;

using SysCredit.Api.Attributes;

using System.Reflection;

public static class AttributeExtensions
{
    public static Attribute? SearchGenericAttribute(this object @object, Type GenericTypeDefinition)
    {
        GenericTypeDefinition = GenericTypeDefinition.GetGenericTypeDefinition();

        var Query = from Attribute in @object.GetType().GetCustomAttributes()
                    let AttributeType = Attribute.GetType()
                    where AttributeType.IsGenericType && AttributeType.GetGenericTypeDefinition() == GenericTypeDefinition
                    select Attribute;

        return Query.FirstOrDefault();
    }

    public static Type[]? SearchGenericTypeArgumentsFromGenericAttribute(this object @object, Type GenericTypeDefinition)
    {
        return @object.SearchGenericAttribute(GenericTypeDefinition)?.GetType().GenericTypeArguments;
    }

    public static string GetErrorCode(this Type Type, int CodeIndex)
    {
        return Type.GetCustomAttribute<ErrorCodeAttribute>()!.GetErrorCode(CodeIndex);
    }

    public static string GetErrorCategory(this Type Type)
    {
        return Type.GetCustomAttribute<ErrorCategoryAttribute>()!.Category;
    }
}
