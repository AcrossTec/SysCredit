namespace SysCredit.Api;

using System.Data.Common;
using System.Data.SqlClient;

using Microsoft.Data.Sqlite;

using MySql.Data.MySqlClient;

using Npgsql;

/// <summary>
///     Configuraciones globales para SysCredi.Api
/// </summary>
public sealed class SysCreditOptions
{
    /// <summary>
    ///     Cadena de conexión al servidor.
    /// </summary>
    /// <seealso cref="ConnectionType" />
    public required string ConnectionString { get; set; }

    /// <summary>
    ///     Tipo de cadena de conexión del servidor.
    ///     Permite crear de forma correcta el proveedor de base de datos.
    /// </summary>
    /// <seealso cref="CreateDbConnection" />
    public required string ConnectionType { get; set; }

    /// <summary>
    ///     Método de fábrica que crea un proveedor de base de datos.
    /// </summary>
    /// <returns>
    ///     Regresa un proveedor de base de datos configurado:
    ///
    ///     <list type="bullet">
    ///         <item><see cref="SqlConnection" /></item>
    ///         <description>Proveedor SQL Server</description>
    ///         
    ///         <item><see cref="NpgsqlConnection" /></item>
    ///         <description>Proveedor PostgreSQL</description>
    ///         
    ///         <item><see cref="MySqlConnection" /></item>
    ///         <description>Proveedor MySQL</description>
    ///         
    ///         <item><see cref="SqliteConnection" /></item>
    ///         <description>Proveedor SQLite</description>
    ///     </list>
    /// </returns>
    public DbConnection CreateDbConnection()
    {
        return ConnectionType switch
        {
            nameof(SqlConnection) => new SqlConnection(ConnectionString),
            nameof(MySqlConnection) => new MySqlConnection(ConnectionString),
            nameof(NpgsqlConnection) => new NpgsqlConnection(ConnectionString),
            nameof(SqliteConnection) => new SqliteConnection(ConnectionString),
            _ => throw new NotImplementedException()
        };
    }
}
