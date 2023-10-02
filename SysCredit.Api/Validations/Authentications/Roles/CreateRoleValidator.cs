namespace SysCredit.Api.Validations.Authentications.Roles;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Authentications.Roles;

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
