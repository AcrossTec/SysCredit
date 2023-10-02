namespace SysCredit.Helpers;

using System.Text.Json.Serialization;

using static System.Text.Json.Serialization.JsonIgnoreCondition;

/// <summary>
///     Clase usada para obtener el estado de error de una ejecución de algún Endpoint.
/// </summary>
public record class ErrorStatus
{
    /// <summary>
    ///     Indica si alguna ejecución de algún Endpoint tiene error o no.
    /// </summary>
    public bool HasError { get; set; }

    /// <summary>
    ///     Indica si alguna ejecución de algun Endpoint está correcta o no.
    /// </summary>
    [JsonIgnore(Condition = Always)]
    public bool IsSuccess => HasError is false;

    /// <summary>
    ///     Código único que identifica al método que provoco el error.
    /// </summary>
    [JsonIgnore(Condition = WhenWritingNull)]
    public string? MethodId { get; set; }

    /// <summary>
    ///     Código de error único del error que se ha producido.
    /// </summary>
    [JsonIgnore(Condition = WhenWritingNull)]
    public string? ErrorCode { get; set; }

    /// <summary>
    ///     Categoría a la que pertenece el código de error.
    /// </summary>
    [JsonIgnore(Condition = WhenWritingNull)]
    public string? ErrorCategory { get; set; }

    /// <summary>
    ///     Mensaje asociado al código de error.
    /// </summary>
    [JsonIgnore(Condition = WhenWritingNull)]
    public string? ErrorMessage { get; set; }

    /// <summary>
    ///     Descripción detallada del error.
    /// </summary>
    [JsonIgnore(Condition = WhenWritingNull)]
    public IDictionary<string, object?>? Errors { get; set; }

    /// <summary>
    ///     Representa propiedades adicionales que puedan surgir para dar más información sobre el error.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, object?> Extensions { get; } = new Dictionary<string, object?>(StringComparer.Ordinal);
}
