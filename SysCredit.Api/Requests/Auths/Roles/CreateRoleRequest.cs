namespace SysCredit.Api.Requests.Auths.Roles;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Auths.Roles;

[Validator<CreateRoleValidator>]
public class CreateRoleRequest : IRequest
{
    public string Name { get; set; } = string.Empty;

    public CreateClaimRequest[] RoleClaims { get; set; } = Array.Empty<CreateClaimRequest>();
}
