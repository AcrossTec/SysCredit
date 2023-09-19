namespace SysCredit.Api.Attributes;

/// <summary>
///     Permite especificar la implementación de un Endpoint.
/// </summary>
/// <param name="Route">
///     URL del recurso del Endpoint.
/// </param>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class EndpointAttribute(string Route) : Attribute
{
    /// <summary>
    ///     URL del recurso del Endpoint.
    /// </summary>
    public readonly string Route = Route;
}
