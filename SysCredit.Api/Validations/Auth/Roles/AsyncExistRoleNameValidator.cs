using FluentValidation.Validators;
using FluentValidation;
using SysCredit.Api.Stores.Auth;
using SysCredit.Api.ViewModels.Auth;
using SysCredit.Models.Auth.Roles;
using SysCredit.Api.Extensions;

namespace SysCredit.Api.Validations.Auth.Roles;

public class AsyncExistRoleNameValidator<T> : AsyncPropertyValidator<T, string?>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Value, CancellationToken cancellation)
    {
        return (await Context.RootContextData[nameof(RoleStore)].AsStore<Role>().FetchRoleByName(Value)) is null;
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' 'RoleName' no debe existir. y debe ser único";
    }

    public override string Name => "AsyncExistRoleNameValidator";
}
