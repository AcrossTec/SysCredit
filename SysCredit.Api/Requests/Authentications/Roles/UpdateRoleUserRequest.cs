namespace SysCredit.Api.Requests.Authentications.Roles;

public class UpdateRoleUserRequest : IRequest
{
    public long UserId { get; set; }

    public AssignTypeRequest[] RoleId { get; set; } = Array.Empty<AssignTypeRequest>();
}
