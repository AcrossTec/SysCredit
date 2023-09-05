namespace SysCredit.Api.Stores;

using Microsoft.Extensions.Options;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Data.SqlClient;
using System.Text.Json;
using System.Threading.Tasks;

public interface IStore : IDisposable
{
    SqlConnection Connection { get; }

    IStore<TModel> GetStore<TModel>() where TModel : Entity
    {
        return new Store<TModel>(Options.Create(new SysCreditOptions
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
        var Json = JsonSerializer.Serialize(ViewModel,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonDefaultNamingPolicy.DefaultNamingPolicy });

        return JsonSerializer.Deserialize<TModel>(Json);
    }
}

public class Store<TModel> : IStore<TModel> where TModel : Entity
{
    public Store(IOptions<SysCreditOptions> Options)
        => Connection = new SqlConnection(Options.Value.ConnectionString);

    public SqlConnection Connection { get; }
}
