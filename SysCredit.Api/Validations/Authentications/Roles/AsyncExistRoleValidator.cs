namespace SysCredit.Api.Validations.Authentications.Roles;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Authentications;
using SysCredit.Api.Stores;
using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

public class AsyncExistRoleValidator<T> : AsyncPropertyValidator<T, IEnumerable<AssignTypeRequest>>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, IEnumerable<AssignTypeRequest> Value, CancellationToken cancellation)
    {
        var Result = await Context.RootContextData[nameof(RoleStore)].AsStore<Role>().ExistAndDuplicatedRolesAsync(Value);
        return Result == false ? true : false;
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Solicitud con registros inexistentes o duplicados: 'RoleId' debe existir. y debe ser único";
    }

    public override string Name => "UserRolesUniqueRequestValidator";
}