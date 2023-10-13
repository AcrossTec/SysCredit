namespace SysCredit.Api.Extensions;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Properties;

using System.Reflection;
using System.Runtime.CompilerServices;

/// <summary>
///     Extensión de utilería para obtener información de los metadatos del método de forma sencilla.
/// </summary>
public static class ReflectionExtensions
{
    /// <summary>
    ///     Verifica si <paramref name="MethodInfo"/> tiene configurado <see cref="MethodIdAttribute.MethodId"/>.
    /// </summary>
    /// <param name="MethodInfo">
    ///     Método que se buscara su MethodId.
    /// </param>
    /// <returns>
    ///     Regresa <see langword="true"/> si existe <see cref="MethodIdAttribute.MethodId"/> en <paramref name="MethodInfo"/> sino <see langword="false"/>.
    /// </returns>
    public static bool HasMethodId(this MethodBase? MethodInfo)
    {
        return MethodInfo.GetMethodId() is not null;
    }

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
    ///     Obtiene el código de error correspondiente a un error de SysCredit.
    /// </summary>
    /// <param name="MethodInfo">
    ///     Método que declara el tipo que tiene el prefijo correspondiente de código de error.
    /// </param>
    /// <param name="PredefinedErrorCodeNumber">
    ///     Código de error predefinido.
    /// </param>
    /// <seealso cref="PredefinedErrorCodeNumbers" />
    /// <returns>
    ///     Regresa el código de error con el prefijo correspondiente al tipo donde es declarado el método <paramref name="MethodInfo" />.
    /// </returns>
    private static string? GetPredefinedErrorCode(this MethodBase? MethodInfo, string PredefinedErrorCodeNumber)
    {
        string? Prefix = MethodInfo?.GetErrorCodePrefix();

        if (Prefix is not null)
        {
            return $"{Prefix}{PredefinedErrorCodeNumber}";
        }

        return null;
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
    public static string? GetServiceValidationErrorCode(this MethodBase? MethodInfo)
    {
        return MethodInfo.GetPredefinedErrorCode(PredefinedErrorCodeNumbers.ServiceValidationError);
    }

    /// <summary>
    ///     Obtiene el código de error correspondiente a un error de base de datos.
    /// </summary>
    /// <param name="MethodInfo">
    ///     Método que declara el tipo que tiene el prefijo correspondiente de código de error.
    /// </param>
    /// <returns>
    ///     Regresa el código de error con el prefijo correspondiente al tipo donde es declarado el método <paramref name="MethodInfo" />.
    /// </returns>
    public static string? GetStoreSQLServerErrorCode(this MethodBase? MethodInfo)
    {
        return MethodInfo.GetPredefinedErrorCode(PredefinedErrorCodeNumbers.StoreSQLServerError);
    }

    /// <summary>
    ///     Obtiene el código de error correspondiente a un error de base de datos.
    /// </summary>
    /// <param name="MethodInfo">
    ///     Método que declara el tipo que tiene el prefijo correspondiente de código de error.
    /// </param>
    /// <returns>
    ///     Regresa el código de error con el prefijo correspondiente al tipo donde es declarado el método <paramref name="MethodInfo" />.
    /// </returns>
    public static string? GetStoreExecuteErrorCode(this MethodBase? MethodInfo)
    {
        return MethodInfo.GetPredefinedErrorCode(PredefinedErrorCodeNumbers.StoreExecuteError);
    }

    /// <summary>
    ///     Obtiene el código de error correspondiente a un error de base de datos.
    /// </summary>
    /// <param name="MethodInfo">
    ///     Método que declara el tipo que tiene el prefijo correspondiente de código de error.
    /// </param>
    /// <returns>
    ///     Regresa el código de error con el prefijo correspondiente al tipo donde es declarado el método <paramref name="MethodInfo" />.
    /// </returns>
    public static string? GetServiceExecuteErrorCode(this MethodBase? MethodInfo)
    {
        return MethodInfo.GetPredefinedErrorCode(PredefinedErrorCodeNumbers.ServiceExecuteError);
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
    public static string GetServiceValidationErrorCodeMessage(this MethodBase? MethodInfo)
    {
        return ErrorCodeMessages.GetMessageFromCode(MethodInfo.GetServiceValidationErrorCode()!)!;
    }

    /// <summary>
    ///     Obtiene el mensaje del código de error correspondiente a un error de base de datos.
    /// </summary>
    /// <param name="MethodInfo">
    ///     Método que declara el tipo que tiene el prefijo correspondiente de código de error del que se obtendré el mensaje de error.
    /// </param>
    /// <returns>
    ///     Regresa el mensaje del código de error con el prefijo correspondiente al tipo donde es declarado el método <paramref name="MethodInfo" />.
    /// </returns>
    public static string GetStoreSQLServerErrorCodeMessage(this MethodBase? MethodInfo)
    {
        return ErrorCodeMessages.GetMessageFromCode(MethodInfo.GetStoreSQLServerErrorCode()!)!;
    }

    /// <summary>
    ///     Obtiene el mensaje del código de error correspondiente a un error de ejecución del Store.
    /// </summary>
    /// <param name="MethodInfo">
    ///     Método que declara el tipo que tiene el prefijo correspondiente de código de error del que se obtendré el mensaje de error.
    /// </param>
    /// <returns>
    ///     Regresa el mensaje del código de error con el prefijo correspondiente al tipo donde es declarado el método <paramref name="MethodInfo" />.
    /// </returns>
    public static string GetStoreExecuteErrorCodeMessage(this MethodBase? MethodInfo)
    {
        return ErrorCodeMessages.GetMessageFromCode(MethodInfo.GetStoreExecuteErrorCode()!)!;
    }

    /// <summary>
    ///     Obtiene el mensaje del código de error correspondiente a un error de ejecución del Servicio.
    /// </summary>
    /// <param name="MethodInfo">
    ///     Método que declara el tipo que tiene el prefijo correspondiente de código de error del que se obtendré el mensaje de error.
    /// </param>
    /// <returns>
    ///     Regresa el mensaje del código de error con el prefijo correspondiente al tipo donde es declarado el método <paramref name="MethodInfo" />.
    /// </returns>
    public static string GetServiceExecuteErrorCodeMessage(this MethodBase? MethodInfo)
    {
        return ErrorCodeMessages.GetMessageFromCode(MethodInfo.GetServiceExecuteErrorCode()!)!;
    }

    /// <summary>
    ///     Verifica si un método regresa una tarea asincrona.
    /// </summary>
    /// <param name="Method">
    ///     Método que será verificado.
    /// </param>
    /// <returns>
    ///     Regresa <see cref="bool">True</see> si el método regresa una tarea asincrona sino <see cref="bool">False</see>.
    /// </returns>
    public static bool CheckMethodReturnTypeIsTaskType(this MethodInfo Method)
    {
        Type ReturnType = Method.ReturnType;

        if (ReturnType.IsGenericType)
        {
            if (ReturnType.IsInterface && ReturnType.GetGenericTypeDefinition() == typeof(IAsyncEnumerable<>))
            {
                return true;
            }
            else if (ReturnType.GetGenericTypeDefinition() == typeof(Task<>) || ReturnType.GetGenericTypeDefinition() == typeof(ValueTask<>))
            {
                return true;
            }
        }
        else if (ReturnType == typeof(Task) || ReturnType == typeof(ValueTask))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Verifica si un método es asincrono.
    /// </summary>
    /// <param name="Method">
    ///     Método que será verificado.
    /// </param>
    /// <returns>
    ///     Regresa True si el método es asincrono en caso contrario False.
    /// </returns>
    public static bool IsAsyncMethod(this MethodInfo Method)
    {
        // TODO: IAsyncEnumerable<>
        // bool IsAsyncStateMachine = Attribute.IsDefined(Method, typeof(AsyncStateMachineAttribute), inherit: false);
        bool IsTaskType = CheckMethodReturnTypeIsTaskType(Method);
        return /* IsAsyncStateMachine && */ IsTaskType;
    }
}
