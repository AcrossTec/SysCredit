namespace SysCredit.Api;

using Npgsql;

using System.Data.Common;
using System.Data.SqlClient;

/// <summary>
///     Configuraciones globales para SysCredi.Api
/// </summary>
public sealed class SysCreditOptions
{
    /// <summary>
    ///     Cadena de conexión al servidor.
    /// </summary>
    /// <seealso cref="ConnectionType" />
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    ///     Tipo de cadena de conexión del servidor.
    ///     Permite crear de forma correcta el proveedor de base de datos.
    /// </summary>
    /// <seealso cref="CreateConnection" />
    public string ConnectionType { get; set; } = string.Empty;

    /// <summary>
    ///     Método de fábrica que crea un proveedor de base de datos.
    /// </summary>
    /// <returns>
    ///     Regresa un proveedor de base de datos:
    ///
    ///     <list type="bullet">
    ///         <item><see cref="SqlConnection" /></item>
    ///         <description>Proveedor SQL Server</description>
    ///         <item><see cref="NpgsqlConnection" /></item>
    ///         <description>Proveedor PostgreSQL</description>
    ///     </list>
    /// </returns>
    public DbConnection CreateConnection()
    {
        return ConnectionType switch
        {
            nameof(SqlConnection) => new SqlConnection(ConnectionString),
            nameof(NpgsqlConnection) => new NpgsqlConnection(ConnectionString),
            _ => throw new NotSupportedException("Proveedor de base de datos no soportado.")
        };
    }
}
