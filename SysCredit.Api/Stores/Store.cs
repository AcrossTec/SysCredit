namespace SysCredit.Api.Stores;

using Microsoft.Extensions.Options;

using System.Data.SqlClient;

public interface IStore<TModel> : IDisposable
{
    SqlConnection Connection { get; }

    IStore<T> Store<T>();

    void IDisposable.Dispose()
    {
        Connection?.Dispose();
    }
}

public readonly struct Store<TModel> : IStore<TModel>
{
    public Store(IOptions<SysCreditOptions> Options)
        => Connection = new SqlConnection(Options.Value.ConnectionString);

    public SqlConnection Connection { get; }

    IStore<T> IStore<TModel>.Store<T>()
    {
        return new Store<T>(Options.Create(new SysCreditOptions
        {
            ConnectionString = Connection.ConnectionString
        }));
    }
}