namespace SysCredit.DataTransferObject.Commons;

public class AuthInfo : IDataTransferObject
{
    public long UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty; 

    public string Token { get; set; } = string.Empty;
}
