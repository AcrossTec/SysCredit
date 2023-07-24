namespace SysCredit.Api.DataTransferObject;

using System.Text.Json.Serialization;

public record class Response<T>
{
    public ErrorStatus Status { get; set; } = new();

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }
}

public record class Response : Response<object?>
{
}
