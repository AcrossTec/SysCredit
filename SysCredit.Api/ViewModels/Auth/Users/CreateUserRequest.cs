using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Auth.Users;

namespace SysCredit.Api.ViewModels.Auth.Users;

[Validator<CreateUserValidator>]
public class CreateUserRequest : IViewModel
{
    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;   
    
    public string Password { get; set; } = string.Empty;
    
    public string Phone { get; set; } = string.Empty;
    
    public AssingRoleRequest[] Roles { get; set; } = Array.Empty<AssingRoleRequest>();
    
    public CreateClaimRequest[] UserClaims { get; set; } = Array.Empty<CreateClaimRequest>();
}