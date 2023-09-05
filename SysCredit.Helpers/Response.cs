namespace SysCredit.Helpers;

using System.Text.Json.Serialization;

public interface IResponse
{
    ErrorStatus Status { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    object? Data { get; }
}

public interface IResponse<out T> : IResponse
{
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    T? Data { get; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
}

public record class Response<T> : IResponse<T>
{
    public ErrorStatus Status { get; set; } = new();

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    object? IResponse.Data => Data;
}

public record class Response : Response<object?>
{
}

public interface IServiceResult<out T> : IResponse<T>
{
}

public record class ServiceResult<T> : Response<T>, IServiceResult<T>
{
}