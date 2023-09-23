namespace SysCredit.Api.Exceptions;

/// <summary>
///     Excepción que es lanzado cuando un Proxy genera algún error.
/// </summary>
/// <param name="Message">
///     Mensaje de error que describe porque se dio el error.
/// </param>
/// <param name="InnerException">
///     Excepción que se va ha encolar al <see cref="ProxyException" />.
/// </param>
public class ProxyException(string Message, Exception? InnerException = null) : Exception(Message, InnerException);
