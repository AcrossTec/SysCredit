namespace SysCredit.Api.Attributes;

/// <summary>
///     Permite especificar la implementación de un conjunto de Endpoint.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class EndpointsAttribute : Attribute;
