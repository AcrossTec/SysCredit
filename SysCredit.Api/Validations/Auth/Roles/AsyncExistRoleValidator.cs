namespace SysCredit.Api.Validations.Auth.Roles;

using FluentValidation.Validators;
using SysCredit.Api.ViewModels.Auth;
using System.Threading.Tasks;
using FluentValidation;
using System.Threading;
using SysCredit.Api.Stores.Auth;
using SysCredit.Api.Extensions;
using SysCredit.Models.Auth.Roles;

public class AsyncExistRoleValidator<T> : AsyncPropertyValidator<T, IEnumerable<AssingRoleRequest>>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, IEnumerable<AssingRoleRequest> Value, CancellationToken cancellation)
    {
        return await Context.RootContextData[nameof(RoleStore)].AsStore<Role>().ExistAndDuplicatedRolesAsync(Value);
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Solicitud con registros inexistentes o duplicados: 'RoleId' debe existir. y debe ser único";
    }

    public override string Name => "UserRolesUniqueRequestValidator";
}