namespace SysCredit.Api.Extensions;

using SysCredit.Api.Attributes;

using System.Reflection;

/// <summary>
///     Métodos de utilería para obtener información desde un Type.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    ///     Obtiene el Tipo que encapsula un Tipo.
    /// </summary>
    /// <param name="Type">
    ///     Tipo que tiene encapsulado otro Tipo.
    /// </param>
    /// <returns>
    ///     Regresa la representación original del Tipo encapsulado desde <paramref name="Type" />.
    /// </returns>
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
    ///     Obtiene el métadato <see cref="ErrorCategoryAttribute" /> desde la información de tipo <paramref name="Type" />.
    /// </summary>
    /// <param name="Type">
    ///     Información el tipo que tiene los metadatos de errores.
    /// </param>
    /// <returns>
    ///     Regresa el <see cref="ErrorCategoryAttribute" /> correspondiente al tipo.
    /// </returns>
    public static string? GetErrorCategory(this Type? Type)
    {
        return Type?.GetCustomAttribute<ErrorCategoryAttribute>()?.Category;
    }

    /// <summary>
    ///     Obtiene el métadato <see cref="ErrorCodePrefixAttribute" /> desde la información de tipo <paramref name="Type" />.
    /// </summary>
    /// <param name="Type">
    ///     Información el tipo que tiene los metadatos de errores.
    /// </param>
    /// <returns>
    ///     Regresa el <see cref="ErrorCodePrefixAttribute" /> correspondiente al tipo.
    /// </returns>
    public static string? GetErrorCodePrefix(this Type? Type)
    {
        return Type?.GetCustomAttribute<ErrorCodePrefixAttribute>()?.Prefix;
    }

    /// <summary>
    ///     Obtiene el atributo <see cref="StoreAttribute"/> de <paramref name="Type"/>.
    /// </summary>
    /// <param name="Type">
    ///     Tipo en el que se buscará el atributo <see cref="StoreAttribute"/>.
    /// </param>
    /// <returns>
    ///     Regresa una instancia del atributo <see cref="StoreAttribute"/> o <see langword="null"/> si no existe en el tipo.
    /// </returns>
    public static StoreAttribute? GetStoreAttribute(this Type? Type)
    {
        return Type?.GetCustomAttribute<StoreAttribute>();
    }

    /// <summary>
    ///     Verifica si el atributo <see cref="StoreAttribute"/> existe en <paramref name="Type"/>.
    /// </summary>
    /// <param name="Type">
    ///     Tipo en el que se buscará el atributo <see cref="StoreAttribute"/>.
    /// </param>
    /// <returns>
    ///     Regresa <see langword="true"/> si existe <see cref="StoreAttribute"/> en <paramref name="Type"/> sino <see langword="false"/>.
    /// </returns>
    public static bool IsStore(this Type? Type)
    {
        return Type?.GetStoreAttribute() is not null;
    }
}
