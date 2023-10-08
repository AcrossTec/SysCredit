namespace SysCredit.Api.Extensions;

using System.Reflection;

/// <summary>
///     Métodos de utilería para una mejor busqueda y manipulación de los atributos.
/// </summary>
public static class AttributeExtensions
{
    /// <summary>
    ///     Busca el atributo genérico <paramref name="GenericAttributeTypeDefinition" /> dentro del objeto <paramref name="object" />.
    /// </summary>
    /// <param name="object">
    ///     Objeto que contiene el atributo genérico ha buscar.
    /// </param>
    /// <param name="GenericAttributeTypeDefinition">
    ///     Tipo del atributo genérico que se buscará.
    /// </param>
    /// <returns>
    ///     Regresa una instancia del atributo genérico encontrado.
    /// </returns>
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
    ///     Obtiene un array con todos los tipos usados por el atributo genérico.
    /// </summary>
    /// <param name="object">
    ///     Objeto que contiene el atributo genérico ha buscar.
    /// </param>
    /// <param name="GenericTypeDefinition">
    ///     Tipo del atributo genérico que se buscará.
    /// </param>
    /// <returns>
    ///     Regresa todos los tipos de los argumentos genéricos de <paramref name="GenericTypeDefinition" />.
    /// </returns>
    public static Type[]? LookupGenericTypeArgumentsFromGenericAttribute(this object @object, Type GenericTypeDefinition)
    {
        return @object.LookupGenericAttribute(GenericTypeDefinition)?.GetType().GenericTypeArguments;
    }
}
