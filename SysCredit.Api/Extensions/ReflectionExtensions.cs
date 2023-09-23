namespace SysCredit.Api.Extensions;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Properties;

using System.Reflection;

/// <summary>
///     Extensión de utilería para obtener información de los metadatos del método de forma sencilla.
/// </summary>
public static class ReflectionExtensions
{
    /// <summary>
    ///     Método del que se obtendrá el metadato: <see cref="MethodIdAttribute" />.
    /// </summary>
    /// <param name="MethodInfo">
    ///     Información del método con los metadatos de errores.
    /// </param>
    /// <returns>
    ///     Regresa el MethodId de <paramref name="MethodInfo" />.
    /// </returns>
    public static string? GetMethodId(this MethodBase? MethodInfo)
    {
        return MethodInfo?.GetCustomAttribute<MethodIdAttribute>()?.MethodId;
    }

    /// <summary>
    ///     Método del que se obtendrá el metadato: <see cref="ErrorCategoryAttribute" /> desde la clase que lo declara.
    /// </summary>
    /// <param name="MethodInfo">
    ///     Información del método con los metadatos de errores.
    /// </param>
    /// <returns>
    ///     Regresa el ErrorCategory de <paramref name="MethodInfo" />.
    /// </returns>
    public static string? GetErrorCategory(this MethodBase? MethodInfo)
    {
        return MethodInfo?.DeclaringType.GetErrorCategory();
    }

    /// <summary>
    ///     Método del que se obtendrá el metadato: <see cref="ErrorCodePrefixAttribute" /> desde la clase que lo declara.
    /// </summary>
    /// <param name="MethodInfo">
    ///     Información del método con los metadatos de errores.
    /// </param>
    /// <returns>
    ///     Regresa el ErrorCodePrefix de <paramref name="MethodInfo" />.
    /// </returns>
    public static string? GetErrorCodePrefix(this MethodBase? MethodInfo)
    {
        return MethodInfo?.DeclaringType.GetErrorCodePrefix();
    }

    /// <summary>
    ///     Obtiene el código de error correspondiente a un error de validación.
    /// </summary>
    /// <param name="MethodInfo">
    ///     Método que declara el tipo que tiene el prefijo correspondiente de código de error.
    /// </param>
    /// <returns>
    ///     Regresa el código de error con el prefijo correspondiente al tipo donde es declarado el método <paramref name="MethodInfo" />.
    /// </returns>
    public static string? GetValidationErrorCode(this MethodBase? MethodInfo)
    {
        string? Prefix = MethodInfo?.GetErrorCodePrefix();

        if (Prefix is not null)
        {
            return $"{Prefix}{PredefinedErrorCodeNumbers.ValidationError}";
        }

        return null;
    }

    /// <summary>
    ///     Obtiene el mensaje del código de error correspondiente a un error de validación.
    /// </summary>
    /// <param name="MethodInfo">
    ///     Método que declara el tipo que tiene el prefijo correspondiente de código de error del que se obtendré el mensaje de error.
    /// </param>
    /// <returns>
    ///     Regresa el mensaje del código de error con el prefijo correspondiente al tipo donde es declarado el método <paramref name="MethodInfo" />.
    /// </returns>
    public static string? GetValidationErrorCodeMessage(this MethodBase? MethodInfo)
    {
        return ErrorCodeMessages.GetMessageFromCode(MethodInfo.GetValidationErrorCode() ?? SysCreditConstants.DefaultKey);
    }
}
