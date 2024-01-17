namespace SysCredit.Api.Constants;

/// <summary>
///     Números de códigos de errores predifinidos.
/// </summary>
public static class PredefinedErrorCodeNumbers
{
    /// <summary>
    ///     Número de código de error para un error de validación.
    /// </summary>
    /// <seealso cref="Proxies.LoggingAdviceServiceInterceptor" />
    public const string ServiceValidationError = "0001";

    /// <summary>
    ///     Número de código de error para un error en el Store.
    /// </summary>
    /// <seealso cref="Proxies.LoggingAdviceServiceInterceptor.CreateServiceException(Castle.DynamicProxy.IInvocation, Exception)" />.
    public const string ServiceExecuteError = "0002";

    /// <summary>
    ///     Número de código de error para un error de base de datos.
    /// </summary>
    /// <seealso cref="Patchers.StorePatcher.InterceptAsync(Stores.IStore, System.Reflection.MethodInfo, Task)" />
    /// <seealso cref="Patchers.StorePatcher.InterceptAsync(Stores.IStore, System.Reflection.MethodInfo, ValueTask)" />
    /// <seealso cref="Patchers.StorePatcher.InterceptAsync{TSource}(Stores.IStore, System.Reflection.MethodInfo, IAsyncEnumerable{TSource})" />
    // /// <seealso cref="Patchers.StorePatcher.InterceptAsync{TResult}(Stores.IStore, System.Reflection.MethodInfo, ValueTask{TResult?})" />
    // /// <seealso cref="Patchers.StorePatcher.InterceptAsync{TResult}(Stores.IStore, System.Reflection.MethodInfo, Task{TResult?})" />
    public const string StoreSQLServerError = "0001";

    /// <summary>
    ///     Número de código de error para un error en el Store.
    /// </summary>
    /// <seealso cref="Patchers.StorePatcher.CreateStoreException(Exception, System.Reflection.MethodInfo)" />.
    public const string StoreExecuteError = "0002";
}
