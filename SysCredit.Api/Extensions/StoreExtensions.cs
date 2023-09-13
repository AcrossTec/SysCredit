namespace SysCredit.Api.Extensions;

using Dapper;

using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Data;

/// <summary>
/// 
/// </summary>
public static class StoreExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Store"></param>
    /// <returns></returns>
    public static async ValueTask<IDbTransaction> BeginTransactionAsync(this IStore Store)
    {
        await Store.Connection.OpenAsync();
        return await Store.Connection.BeginTransactionAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Store"></param>
    /// <param name="Sql"></param>
    /// <param name="Parameters"></param>
    /// <param name="Transaction"></param>
    /// <param name="CommandTimeout"></param>
    /// <returns></returns>
    public static Task<int> ExecAsync(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return Store.Connection.ExecuteAsync(Sql, Parameters, Transaction, CommandTimeout, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Store"></param>
    /// <param name="Sql"></param>
    /// <param name="Parameters"></param>
    /// <param name="Transaction"></param>
    /// <param name="CommandTimeout"></param>
    /// <returns></returns>
    public static Task<T> ExecScalarAsync<T>(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return Store.Connection.ExecuteScalarAsync<T>(Sql, Parameters, Transaction, CommandTimeout, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Store"></param>
    /// <param name="Sql"></param>
    /// <param name="Parameters"></param>
    /// <param name="Transaction"></param>
    /// <param name="CommandTimeout"></param>
    /// <returns></returns>
    public static IEnumerable<T> ExecQuery<T>(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return Store.Connection.Query<T>(Sql, Parameters, Transaction, false, CommandTimeout, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Store"></param>
    /// <param name="Sql"></param>
    /// <param name="Parameters"></param>
    /// <param name="Transaction"></param>
    /// <param name="CommandTimeout"></param>
    /// <returns></returns>
    public static IAsyncEnumerable<T> ExecQueryAsync<T>(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return Store.Connection.Query<T>(Sql, Parameters, Transaction, false, CommandTimeout, CommandType.StoredProcedure).ToAsyncEnumerable();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Store"></param>
    /// <param name="Sql"></param>
    /// <param name="Parameters"></param>
    /// <param name="Transaction"></param>
    /// <param name="CommandTimeout"></param>
    /// <returns></returns>
    public static Task<T> ExecFirstAsync<T>(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return Store.Connection.QueryFirstAsync<T>(Sql, Parameters, Transaction, CommandTimeout, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Store"></param>
    /// <param name="Sql"></param>
    /// <param name="Parameters"></param>
    /// <param name="Transaction"></param>
    /// <param name="CommandTimeout"></param>
    /// <returns></returns>
    public static Task<T> ExecFirstOrDefaultAsync<T>(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return Store.Connection.QueryFirstOrDefaultAsync<T>(Sql, Parameters, Transaction, CommandTimeout, CommandType.StoredProcedure);
    }
}
