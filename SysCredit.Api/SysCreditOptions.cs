namespace SysCredit.Api;

using Npgsql;
using MySql.Data.MySqlClient;

using System.Data.Common;
using System.Data.SqlClient;

/// <summary>
///     Configuraciones globales para SysCredi.Api
/// </summary>
public sealed class SysCreditOptions
{
    /// <summary>
    ///     Información básica para generar un JWT Token.
    /// </summary>
    public SysCreditTokenOptions TokenInfo { get; set; } = default!;

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
    ///         <item><see cref="MySqlConnection"/></item>
    ///         <description>Proveedor MySQL</description>
    ///     </list>
    /// </returns>
    public DbConnection CreateConnection()
    {
        return ConnectionType switch
        {
            nameof(SqlConnection) => new SqlConnection(ConnectionString),
            nameof(NpgsqlConnection) => new NpgsqlConnection(ConnectionString),
            nameof(MySqlConnection) => new MySqlConnection(ConnectionString),
            _ => throw new NotSupportedException("Proveedor de base de datos no soportado.")
        };
    }
}

/// <summary>
///     Información básica para generar un JWT Token.
/// </summary>
public sealed class SysCreditTokenOptions
{
    /// <summary>
    ///     Clave privada para generar el Token.
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    ///     Agente que genera el Token.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;
}
