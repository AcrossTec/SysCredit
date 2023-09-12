using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Auth.Roles;

namespace SysCredit.Api.ViewModels.Auth.Roles;

[Validator<CreateRoleValidator>]
public class CreateRoleRequest : IViewModel
{
    public string Name { get; set; } = string.Empty;

    public CreateClaimRequest[] RoleClaims { get; set; } = Array.Empty<CreateClaimRequest>();
}
