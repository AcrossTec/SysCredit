namespace SysCredit.Api.Stores;

using Microsoft.Extensions.Options;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Data.SqlClient;
using System.Text.Json;

/// <summary>
/// 
/// </summary>
public interface IStore : IDisposable
{
    /// <summary>
    /// 
    /// </summary>
    SqlConnection Connection { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    IStore<TModel> GetStore<TModel>() where TModel : Entity
    {
        return new Store<TModel>(Options.Create(new SysCreditOptions
        {
            ConnectionString = Connection.ConnectionString
        }));
    }

    /// <summary>
    /// 
    /// </summary>
    void IDisposable.Dispose()
    {
        Connection?.Dispose();
    }
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IStore<TModel> : IStore where TModel : Entity
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    /// <param name="ViewModel"></param>
    /// <returns></returns>
    TModel? ToModel<TViewModel>(TViewModel ViewModel)
    {
        var Json = JsonSerializer.Serialize(ViewModel,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonDefaultNamingPolicy.DefaultNamingPolicy });

        return JsonSerializer.Deserialize<TModel>(Json);
    }
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <param name="Options"></param>
public class Store<TModel>(IOptions<SysCreditOptions> Options) : IStore<TModel> where TModel : Entity
{
    /// <summary>
    /// 
    /// </summary>
    public SqlConnection Connection { get; } = new SqlConnection(Options.Value.ConnectionString);
}
