namespace SysCredit.Api;

using SysCredit.Models;

/// <summary>
///     Clase usada como TCategoryName para <see cref="ILogger{TCategoryName}"/>.
/// </summary>
/// <typeparam name="TEndpoint">
///     Nombre de tipo para clases que heredan de <see cref="IEntity"/>.
/// </typeparam>
public class Endpoint<TEndpoint> where TEndpoint : IEntity
{
}
