namespace SysCredit.DataTransferObject.Commons;

public class RoleInfo : IDataTransferObject
{
    public long RoleId { get; set; }

    public string Name { get; set; } = string.Empty;
}
