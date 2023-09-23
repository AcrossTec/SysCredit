namespace SysCredit.Api.Exceptions;

using SysCredit.Helpers;

/// <summary>
///     Excepción que recolecta información detallada de un error en el flujo de ejecución de un Endpoint.
/// </summary>
/// <param name="Status">
///     Objeto que tiene toda la informacion detallada asociado al flujo de ejecución de un Endpoint.
/// </param>
/// <param name="InnerException">
///     Excepción que se va encolar al <see cref="EndpointFlowException" />.
/// </param>
public class EndpointFlowException(ErrorStatus Status, Exception? InnerException = null) : Exception(Status.ErrorMessage, InnerException)
{
    /// <summary>
    ///     Objeto que tiene toda la informacion detallada asociado al flujo de ejecución de un Endpoint.
    /// </summary>
    public ErrorStatus Status { get; } = Status with { HasError = true };
}
