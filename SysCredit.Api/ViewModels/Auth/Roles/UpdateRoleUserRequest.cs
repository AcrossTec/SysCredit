namespace SysCredit.Api.ViewModels.Auth.Roles;

public class UpdateRoleUserRequest : IViewModel
{
    public long UserId { get; set; }
    public AssignRequestType[] RoleId { get; set; } = Array.Empty<AssignRequestType>();
}
