namespace SysCredit.Api.ViewModels.Auth.Roles;

public class UpdateRoleUserRequest : IViewModel
{
    public long UserId { get; set; }
    public AssingRoleRequest[] RoleId { get; set; } = Array.Empty<AssingRoleRequest>();
}
