namespace SysCredit.Api.Exceptions;

/// <summary>
///     Excepción que es lanzado cuando se genera algún error.
/// </summary>
/// <param name="Message">
///     Mensaje de error que describe porque se dio el error.
/// </param>
/// <param name="InnerException">
///     Excepción que se va ha encolar al <see cref="SysCreditException" />.
/// </param>
public class SysCreditException(string? Message, Exception? InnerException = null) : Exception(Message, InnerException);
