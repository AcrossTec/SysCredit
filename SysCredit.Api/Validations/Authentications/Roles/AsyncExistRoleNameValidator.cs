namespace SysCredit.Api.Validations.Authentications.Roles;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;
using SysCredit.Models;


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
