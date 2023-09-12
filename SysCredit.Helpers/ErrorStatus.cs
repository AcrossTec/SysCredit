namespace SysCredit.Helpers;

using System.Text.Json.Serialization;

using static System.Text.Json.Serialization.JsonIgnoreCondition;

public record class ErrorStatus
{
    public bool HasError { get; set; }

    [JsonIgnore(Condition = Always)]
    public bool IsSuccess => HasError is false;

    [JsonIgnore(Condition = WhenWritingNull)]
    public string? MethodId { get; set; }

    [JsonIgnore(Condition = WhenWritingNull)]
    public string? ErrorCode { get; set; }

    [JsonIgnore(Condition = WhenWritingNull)]
    public string? ErrorCategory { get; set; }

    [JsonIgnore(Condition = WhenWritingNull)]
    public string? ErrorMessage { get; set; }

    [JsonIgnore(Condition = WhenWritingNull)]
    public IDictionary<string, object?>? Errors { get; set; }

    [JsonExtensionData]
    public IDictionary<string, object?> Extensions { get; } = new Dictionary<string, object?>(StringComparer.Ordinal);
}
