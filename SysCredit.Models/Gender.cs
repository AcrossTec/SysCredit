namespace SysCredit.Models;

using System.Text.Json.Serialization;

/// <summary>
///     Género de las personas.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<Gender>))]
public enum Gender
{
    /// <summary>
    ///     Mujer.
    /// </summary>
    Female,

    /// <summary>
    ///     Hombre.
    /// </summary>
    Male
}
