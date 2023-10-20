namespace SysCredit.Api.Validations.Authentications.Roles;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Authentications.Roles;

/// <summary>
///     Clase Validadora de <see cref="CreateRoleRequest"/>.
/// </summary>
public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
{
    /// <summary>
    ///     Valida el Nombre del Rol a Crear.
    /// </summary>
    public CreateRoleValidator()
    {
        RuleFor(R => R.Name)
            .NotEmpty()
            .NotNull()
            .UniqueRoleNameAsync()
            .WithName("Nombre de rol");           
    }
}
