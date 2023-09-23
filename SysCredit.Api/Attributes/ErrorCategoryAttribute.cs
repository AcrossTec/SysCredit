namespace SysCredit.Api.Attributes;

/// <summary>
///     Declara una categoría de error que es una agrupación de errores.
/// </summary>
/// <param name="Category">
///     Nombre de la categoría que será declarada.
/// </param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ErrorCategoryAttribute(string Category) : Attribute
{
    /// <summary>
    ///     Nombre de la categoría de error.
    /// </summary>
    public readonly string Category = Category;
}
