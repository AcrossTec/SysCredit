namespace SysCredit.Api.Interfaces;

using SysCredit.Models;

/// <summary>
///     Interfaz base de todos los managers.
/// </summary>
/// <typeparam name="TModel">
///     Tipo del modelo derivado de <see cref="IEntity" />.
/// </typeparam>
public interface IManager<in TModel> : IService<TModel> where TModel : IEntity
{
}

/// <summary>
///     Interfaz base para identificar un Manager.
/// </summary>
public interface IManager : IService
{
}
