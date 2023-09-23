namespace SysCredit.Api.Attributes;

/// <summary>
///     Especifica algun prefijo de error usado por SourceGenerator para clase generada <see cref="Constants.ErrorCodes" />.
/// </summary>
/// <param name="Prefix">
///     Prefijo que será usado para las nuevas entradas en <see cref="Constants.ErrorCodes" />.
/// </param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ErrorCodePrefixAttribute(string Prefix) : Attribute
{
    /// <summary>
    ///     Nombre del prefijo usado para la lista de errores en <see cref="Constants.ErrorCodes" />.
    /// </summary>
    public readonly string Prefix = Prefix;
}
