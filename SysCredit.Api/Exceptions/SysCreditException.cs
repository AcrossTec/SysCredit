﻿namespace SysCredit.Api.Exceptions;

using SysCredit.Helpers;

/// <summary>
///     Excepción que es lanzado cuando se genera algún error.
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
public class SysCreditException(string? Message = null, ErrorStatus? ErrorStatus = null, Exception? InnerException = null) : Exception(Message, InnerException)
{
    /// <summary>
    ///     Información detallada de los errores de SysCredit.Api.
    /// </summary>
    public ErrorStatus? ErrorStatus { get; } = ErrorStatus ?? new() { HasError = true };
}
