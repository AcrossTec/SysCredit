namespace SysCredit.Helpers;

using System.Runtime.CompilerServices;
using System.Text.Json;

/// <summary>
///     Anula la política de nombramiento de claves de un Json.
/// </summary>
public class JsonDefaultNamingPolicy : JsonNamingPolicy
{
    /// <summary>
    ///     Objeto singletón con la política de nombramiento de clave por defecto de un Json. 
    /// </summary>
    public static JsonDefaultNamingPolicy DefaultNamingPolicy { get; } = new JsonDefaultNamingPolicy();

    /// <summary>
    ///     No realiza ninguna conversión sobre la cadena.
    /// </summary>
    /// <param name="Name">
    ///     Cadena que se ignorará la conversión.
    /// </param>
    /// <returns>
    ///     Regresa la misma cadena usada como argumento.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ConvertName(string Name)
    {
        return Name;
    }
}
