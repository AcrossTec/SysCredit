namespace SysCredit.Api.DataTransferObject;

public record class ErrorStatus
{
    public bool HasError { get; set; }

    public long ErrorCode { get; set; }

    public string? ErrorCategory { get; set; }

    public string? Message { get; set; }

    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}
