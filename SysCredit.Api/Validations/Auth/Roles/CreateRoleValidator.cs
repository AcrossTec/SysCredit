namespace SysCredit.Api.Validations.Auth.Roles;

using FluentValidation;
using SysCredit.Api.Extensions;
using SysCredit.Api.ViewModels.Auth.Roles;

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
