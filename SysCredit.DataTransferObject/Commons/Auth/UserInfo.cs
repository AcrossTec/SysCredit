namespace SysCredit.DataTransferObject.Commons.Auth;

public record class UserInfo : IDataTransferObject
{
    public long UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
}