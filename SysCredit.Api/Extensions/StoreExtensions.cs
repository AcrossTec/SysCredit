namespace SysCredit.Api.Extensions;

using Dapper;

using SysCredit.Api.Models;
using SysCredit.Api.Stores;

using System.Data;

public static class StoreExtensions
{
    public static IStore AsStore(this object @object)
    {
        return (IStore)@object;
    }

    public static IStore<T> AsStore<T>(this object @object) where T : Entity
    {
        return (IStore<T>)@object;
    }

    public static Task<int> ExecAsync(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return Store.Connection.ExecuteAsync(Sql, Parameters, Transaction, CommandTimeout, CommandType.StoredProcedure);
    }

    public static Task<T> ExecScalarAsync<T>(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return Store.Connection.ExecuteScalarAsync<T>(Sql, Parameters, Transaction, CommandTimeout, CommandType.StoredProcedure);
    }

    public static IAsyncEnumerable<T> ExecQueryAsync<T>(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return Store.Connection.Query<T>(Sql, Parameters, Transaction, false, CommandTimeout, CommandType.StoredProcedure).ToAsyncEnumerable();
    }

    public static Task<T> ExecFirstAsync<T>(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return Store.Connection.QueryFirstAsync<T>(Sql, Parameters, Transaction, CommandTimeout, CommandType.StoredProcedure);
    }

    public static Task<T> ExecFirstOrDefaultAsync<T>(this IStore Store, string Sql, object? Parameters = null, IDbTransaction? Transaction = null, int? CommandTimeout = null)
    {
        return Store.Connection.QueryFirstOrDefaultAsync<T>(Sql, Parameters, Transaction, CommandTimeout, CommandType.StoredProcedure);
    }
}
