namespace SysCredit.Helpers;

using System.Text.Json.Serialization;

/// <summary>
///     Tipo de retorno usando para todos los Endpoints.
/// </summary>
public interface IResponse
{
    /// <summary>
    ///     Objeto de estado con la información sobre alguna operación de un Endpoint.
    /// </summary>
    ErrorStatus Status { get; }

    /// <summary>
    ///     Respuesta de algún request de un Endpoint.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    object? Data { get; }
}

/// <inheritdoc />
public interface IResponse<out T> : IResponse
{
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    /// <inheritdoc cref="IResponse.Data" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    T? Data { get; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
}

/// <summary>
///     Tipo de retorno usando para todos los Endpoints.
/// </summary>
/// <typeparam name="T">
///     Tipo de dato usando para <see cref="Data" />.
/// </typeparam>
public record class Response<T> : IResponse<T>
{
    /// <inheritdoc />
    public ErrorStatus Status { get; set; } = new();

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    object? IResponse.Data => Data;
}

/// <inheritdoc />
public record class Response : Response<object?>
{
}

/// <summary>
///     Resultado de alguna operación de algún servicio.
/// </summary>
/// <typeparam name="T">
///     Tipo para <see cref="IResponse{T}.Data" />.
/// </typeparam>
public interface IServiceResult<out T> : IResponse<T>
{
}

/// <summary>
///     Resultado de alguna operación de algún servicio.
/// </summary>
/// <typeparam name="T">
///     Tipo para <see cref="IResponse{T}.Data" />.
/// </typeparam>
public record class ServiceResult<T> : Response<T>, IServiceResult<T>
{
}
