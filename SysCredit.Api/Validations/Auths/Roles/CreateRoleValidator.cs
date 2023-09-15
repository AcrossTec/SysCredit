namespace SysCredit.Api.Validations.Auths.Roles;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Auths.Roles;

public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
{
    public CreateRoleValidator()
    {
        RuleFor(R => R.Name)
            .NotEmpty()
            .NotNull()
            .UniqueRoleNameAsync()
            .WithName("Nombre de rol");           
    }
}
