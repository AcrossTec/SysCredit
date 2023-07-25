namespace SysCredit.Api.Stores;

using Microsoft.Extensions.Options;

using SysCredit.Api.Models;

using System.Data.SqlClient;

public interface IStore : IDisposable
{
    SqlConnection Connection { get; }

    IStore<T> Store<T>() where T : Entity
    {
        return new Store<T>(Options.Create<SysCreditOptions>(new()
        {
            ConnectionString = Connection.ConnectionString
        }));
    }

    void IDisposable.Dispose()
    {
        Connection?.Dispose();
    }
}

public interface IStore<TModel> : IStore where TModel : Entity
{
    TModel? ToModel<TViewModel>(TViewModel ViewModel)
    {
        var Json = System.Text.Json.JsonSerializer.Serialize(ViewModel);
        return System.Text.Json.JsonSerializer.Deserialize<TModel>(Json);
    }
}

public class Store<TModel> : IStore<TModel> where TModel : Entity
{
    public Store(IOptions<SysCreditOptions> Options)
        => Connection = new SqlConnection(Options.Value.ConnectionString);

    public SqlConnection Connection { get; }
}
