namespace SysCredit.Api.Stores;

using System.Data.Common;
using System.Text.Json;

using Microsoft.Extensions.Options;

using SysCredit.Helpers;
using SysCredit.Models;

/// <summary>
///     Interfaz base de todos los repositorios que realizan operaciones en base de datos.
/// </summary>
public interface IStore : IDisposable
{
    /// <summary>
    ///     Logger para informar sobre los distintos pasos realizados sobre el repositorio.
    /// </summary>
    ILogger<IStore> Logger { get; }

    /// <summary>
    ///     Objeto de conexión con el servidor.
    /// </summary>
    /// <seealso cref="Store{TModel}.Connection" />
    DbConnection Connection { get; }

    /// <summary>
    ///     Permite crear un Store desde otro Store.
    /// </summary>
    /// <typeparam name="TModel">
    ///     Cualquier tipo que herede de <see cref="Entity" />.
    /// </typeparam>
    /// <returns>
    ///     Regresa un nuevo Store con los campos específicos asociados al <typeparamref name="TModel" />.
    /// </returns>
    IStore<TModel> GetStore<TModel>() where TModel : Entity;

    /// <summary>
    ///     Libera todos los recursos del objeto de conexión.
    /// </summary>
    void IDisposable.Dispose() => Connection?.Dispose();
}

/// <summary>
///     Interfaz base de todos los repositorios que realizan operaciones en base de datos.
/// </summary>
/// <typeparam name="TModel">
///     Tipo de cualquier clase que hereda de <see cref="Entity" />.
/// </typeparam>
public interface IStore<TModel> : IStore where TModel : Entity
{
    /// <summary>
    ///     Logger para informar sobre los distintos pasos realizados sobre el repositorio.
    /// </summary>
    ILogger<IStore> IStore.Logger => Logger;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    /// <summary>
    ///     Logger para informar sobre los distintos pasos realizados sobre el repositorio.
    /// </summary>
    ILogger<IStore<TModel>> Logger { get; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    /// <summary>
    ///     Permite convertir un <paramref name="ViewModel" /> de tipo <typeparamref name="TViewModel"/> a un <typeparamref name="TModel" />.
    /// </summary>
    /// <typeparam name="TViewModel">
    ///     Tipo del <paramref name="ViewModel" />.
    /// </typeparam>
    /// <param name="ViewModel">
    ///     <paramref name="ViewModel" /> que ser convertirá al tipo <typeparamref name="TModel" />.
    /// </param>
    /// <returns>
    ///     Regresa un objeto de tipo <typeparamref name="TModel" /> que es el resultado convertir un objeto de tipo <typeparamref name="TViewModel" />.
    /// </returns>
    TModel? ToModel<TViewModel>(TViewModel ViewModel)
    {
        var JsonTypeInfo = SysCreditSerializerContext.Default.GetTypeInfo(typeof(TViewModel))!;
        JsonTypeInfo.Options.PropertyNamingPolicy = DefaultJsonNamingPolicy.Default;

        var Json = JsonSerializer.Serialize(ViewModel, JsonTypeInfo);
        return (TModel?)JsonSerializer.Deserialize(Json, JsonTypeInfo);
    }
}

/// <summary>
///     Implementación con la funcionalidad necesaria para realizar operaciones a la base de datos.
/// </summary>
/// <typeparam name="TModel">
///     Tipo del modelo de base de datos para las distintas operaciones del repositorio.
/// </typeparam>
/// <param name="Options">
///     Opciones globales del proyecto SysCredit.Api.
/// </param>
/// <param name="LoggerFactory">
///     Proveedor de Logs que crea nuevos Logs específicos para el método: <see cref="Store{TModel}.GetStore{TEntity}" />.
/// </param>
public class Store<TModel>(IOptions<SysCreditOptions> Options, ILoggerFactory LoggerFactory) : IStore<TModel> where TModel : Entity
{
    /// <summary>
    ///     Objeto de conexión a algún proveedor de base de datos.
    /// </summary>
    /// <seealso cref="SysCreditOptions.CreateDbConnection" />
    public DbConnection Connection { get; } = Options.Value.CreateDbConnection();

    /// <summary>
    ///     Objeto de Logs para informar sobre los distintos pasos que realiza un Store.
    /// </summary>
    public ILogger<IStore<TModel>> Logger { get; } = new Logger<IStore<TModel>>(LoggerFactory);

    /// <summary>
    ///     Permite crear un Store desde otro Store.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     Cualquier tipo que herede de <see cref="Entity" />.
    /// </typeparam>
    /// <returns>
    ///     Regresa un nuevo Store con los campos específicos asociados al <typeparamref name="TEntity" />.
    /// </returns>
    public IStore<TEntity> GetStore<TEntity>() where TEntity : Entity
    {
        return new Store<TEntity>(Options, LoggerFactory);
    }
}
