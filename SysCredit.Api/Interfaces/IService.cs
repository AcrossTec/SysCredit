﻿namespace SysCredit.Api.Interfaces;

using SysCredit.Models;

/// <summary>
///     Interfaz base de todos los servicios.
/// </summary>
/// <typeparam name="TModel">
///     Tipo del modelo derivado de <see cref="IEntity" />.
/// </typeparam>
public interface IService<in TModel> : IManager where TModel : IEntity
{
}

/// <summary>
///     Interfaz base para identificar un Servicio.
/// </summary>
public interface IService
{
}
