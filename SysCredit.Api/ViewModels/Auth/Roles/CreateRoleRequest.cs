namespace SysCredit.Api.ViewModels.Auth.Roles;

public class CreateRoleRequest : IViewModel
{
    public string Name { get; set; } = string.Empty;
    public CreateClaimRequest[] RoleClaims { get; set; } = Array.Empty<CreateClaimRequest>();
}
