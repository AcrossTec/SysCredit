namespace SysCredit.Api.Constants;

/// <inheritdoc />
public static partial class ErrorCodes
{
    /// <summary>
    ///     Código de error cuando el Proxy del servicio captura una excepción generalizada.
    /// </summary>
    public const string LoggingAdviceServiceInterceptorErrorCode = MID0000;

    /// <summary>
    ///     Código de error de algún error interno del servidor capturado por <see cref="Middlewares.SysCreditMiddleware" />.
    /// </summary>
    public const string InternalServerErrorCode = MID0001;

    /// <summary>
    ///     Error en el proveedor de base de datos.
    /// </summary>
    public const string DatabaseProviderErrorCode = MID0002;
}
