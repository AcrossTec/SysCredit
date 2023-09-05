namespace SysCredit.DataTransferObject.StoredProcedures.Auth;

public record class FetchUser : IDataTransferObject
{
    public long UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
