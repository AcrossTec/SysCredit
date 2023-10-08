namespace SysCredit.Api.Extensions;

using Dapper;

using SysCredit.Api.Stores;

using System.Data;

/// <summary>
///     Métodos de utilería para todos los Stores.
/// </summary>
public static class StoreExtensions
{
    /// <summary>
    ///     Inicia una transacción de base de datos.
    /// </summary>
    /// <param name="Store">
    ///     Objeto que tiene el contexto de base de datos.
    /// </param>
    /// <returns>
    ///     Regresa una nueva instancia con la transacción.
    /// </returns>
    public static async ValueTask<IDbTransaction> BeginTransactionAsync(this IStore Store)
    {
        await Store.Connection.OpenAsync();
        return await Store.Connection.BeginTransactionAsync();
    }

    /// <summary>
    ///     Ejecuta un procedimiento almacenado de base de datos.
    /// </summary>
    /// <param name="Store">
    ///     Store que contiene el contexto de base de datos.
    /// </param>
    /// <param name="Sql">
    ///     Sentencia SQL ha ejecutar.
    /// </param>
    /// <param name="Parameters">
    ///     Parámetros del procedimiento almacenado.
    /// </param>
    /// <param name="Transaction">
    ///     Transacción usada por sentencia de base de datos.
    /// </param>
    /// <param name="CommandTimeout">
    ///     Tiempo fuera usado como valor límite que debe tardar en ejecutar el procedimiento almacenado.
    /// </param>
    /// <returns>
    ///     Regresa el número de estado del procedimiento almacenado.
    /// </returns>
    public static ValueTask<int> ExecuteStoredProcedureAsync(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return new ValueTask<int>(Store.Connection.ExecuteAsync(Sql, Parameters, Transaction, CommandTimeout, CommandType.StoredProcedure));
    }

    /// <summary>
    ///     Ejecuta un procedimiento almacenado de base de datos y obtiene su resultado como un valor Scalar.
    /// </summary>
    /// <param name="Store">
    ///     Store que contiene el contexto de base de datos.
    /// </param>
    /// <param name="Sql">
    ///     Sentencia SQL ha ejecutar.
    /// </param>
    /// <param name="Parameters">
    ///     Parámetros del procedimiento almacenado.
    /// </param>
    /// <param name="Transaction">
    ///     Transacción usada por sentencia de base de datos.
    /// </param>
    /// <param name="CommandTimeout">
    ///     Tiempo fuera usado como valor límite que debe tardar en ejecutar el procedimiento almacenado.
    /// </param>
    /// <returns>
    ///     Regresa el primer resultado del procedimiento almacenado convertido en un objeto de tipo <typeparamref name="T" />.
    /// </returns>
    public static ValueTask<T> ExecuteStoredProcedureScalarAsync<T>(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return new ValueTask<T>(Store.Connection.ExecuteScalarAsync<T>(Sql, Parameters, Transaction, CommandTimeout, CommandType.StoredProcedure));
    }

    /// <summary>
    ///     Ejecuta un procedimiento almacenado de base de datos.
    /// </summary>
    /// <param name="Store">
    ///     Store que contiene el contexto de base de datos.
    /// </param>
    /// <param name="Sql">
    ///     Sentencia SQL ha ejecutar.
    /// </param>
    /// <param name="Parameters">
    ///     Parámetros del procedimiento almacenado.
    /// </param>
    /// <param name="Transaction">
    ///     Transacción usada por sentencia de base de datos.
    /// </param>
    /// <param name="CommandTimeout">
    ///     Tiempo fuera usado como valor límite que debe tardar en ejecutar el procedimiento almacenado.
    /// </param>
    /// <returns>
    ///     Regresa los resultados del procedimiento almacenado convertidos a objetos de tipo <typeparamref name="T" />.
    /// </returns>
    public static IEnumerable<T> ExecuteStoredProcedureQuery<T>(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return Store.Connection.Query<T>(Sql, Parameters, Transaction, false, CommandTimeout, CommandType.StoredProcedure);
    }

    /// <summary>
    ///     Ejecuta un procedimiento almacenado de base de datos.
    /// </summary>
    /// <param name="Store">
    ///     Store que contiene el contexto de base de datos.
    /// </param>
    /// <param name="Sql">
    ///     Sentencia SQL ha ejecutar.
    /// </param>
    /// <param name="Parameters">
    ///     Parámetros del procedimiento almacenado.
    /// </param>
    /// <param name="Transaction">
    ///     Transacción usada por sentencia de base de datos.
    /// </param>
    /// <param name="CommandTimeout">
    ///     Tiempo fuera usado como valor límite que debe tardar en ejecutar el procedimiento almacenado.
    /// </param>
    /// <returns>
    ///     Regresa los resultados del procedimiento almacenado convertidos a objetos de tipo <typeparamref name="T" />.
    /// </returns>
    public static IAsyncEnumerable<T> ExecuteStoredProcedureQueryAsync<T>(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return Store.Connection.Query<T>(Sql, Parameters, Transaction, false, CommandTimeout, CommandType.StoredProcedure).ToAsyncEnumerable();
    }

    /// <summary>
    ///     Ejecuta un procedimiento almacenado de base de datos.
    /// </summary>
    /// <param name="Store">
    ///     Store que contiene el contexto de base de datos.
    /// </param>
    /// <param name="Sql">
    ///     Sentencia SQL ha ejecutar.
    /// </param>
    /// <param name="Parameters">
    ///     Parámetros del procedimiento almacenado.
    /// </param>
    /// <param name="Transaction">
    ///     Transacción usada por sentencia de base de datos.
    /// </param>
    /// <param name="CommandTimeout">
    ///     Tiempo fuera usado como valor límite que debe tardar en ejecutar el procedimiento almacenado.
    /// </param>
    /// <returns>
    ///     Regresa el primer resultado del procedimiento almacenado convertido al objetos de tipo <typeparamref name="T" />.
    /// </returns>
    public static ValueTask<T> ExecuteStoredProcedureQueryFirstValueAsync<T>(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return new ValueTask<T>(Store.Connection.QueryFirstAsync<T>(Sql, Parameters, Transaction, CommandTimeout, CommandType.StoredProcedure));
    }

    /// <summary>
    ///     Ejecuta un procedimiento almacenado de base de datos.
    /// </summary>
    /// <param name="Store">
    ///     Store que contiene el contexto de base de datos.
    /// </param>
    /// <param name="Sql">
    ///     Sentencia SQL ha ejecutar.
    /// </param>
    /// <param name="Parameters">
    ///     Parámetros del procedimiento almacenado.
    /// </param>
    /// <param name="Transaction">
    ///     Transacción usada por sentencia de base de datos.
    /// </param>
    /// <param name="CommandTimeout">
    ///     Tiempo fuera usado como valor límite que debe tardar en ejecutar el procedimiento almacenado.
    /// </param>
    /// <returns>
    ///     Regresa el primer resultado del procedimiento almacenado convertido al objetos de tipo <typeparamref name="T" /> o null si no hay resultados.
    /// </returns>
    public static ValueTask<T> ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<T>(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return new ValueTask<T>(Store.Connection.QueryFirstOrDefaultAsync<T>(Sql, Parameters, Transaction, CommandTimeout, CommandType.StoredProcedure));
    }
}
