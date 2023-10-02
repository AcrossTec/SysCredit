namespace SysCredit.Api.Requests.Authentications.Roles;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Authentications.Roles;

[Validator<CreateRoleValidator>]
public class CreateRoleRequest : IRequest
{
    public string Name { get; set; } = string.Empty;

    public CreateClaimRequest[] RoleClaims { get; set; } = Array.Empty<CreateClaimRequest>();
}
