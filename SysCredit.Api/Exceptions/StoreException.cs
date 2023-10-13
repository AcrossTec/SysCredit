namespace SysCredit.Api.Exceptions;

using SysCredit.Helpers;

/// <summary>
///     Representa los errores ocurridos en un Store.
/// </summary>
/// <param name="Message">
///     Mensaje de error que describe porque se dio el error.
/// </param>
/// <param name="ErrorStatus">
///     Información detallada de los errores de SysCredit.Api.
/// </param>
/// <param name="InnerException">
///     Excepción que se va ha encolar al <see cref="SysCreditException"/>.
/// </param>
public class StoreException(string? Message = null, ErrorStatus? ErrorStatus = null, Exception? InnerException = null)
        : SysCreditException(Message, ErrorStatus, InnerException);
