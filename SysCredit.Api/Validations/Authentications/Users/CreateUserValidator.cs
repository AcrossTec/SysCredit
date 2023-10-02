namespace SysCredit.Api.Validations.Authentications.Users;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Authentications.Users;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(U => U.UserName)
            .NotEmpty()
            .NotNull()
            .UserUniqueUserNameInRequest()
            .WithName("Nombre de usuario");

        RuleFor(U => U.Email)
            .NotEmpty()
            .NotNull()
            .UserUniqueEmailInRequest()
            .WithName("Email");

        RuleFor(U => U.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(8)
            .WithName("Contraseña");

        RuleFor(U => U.Phone)
            .NotEmpty()
            .NotNull()
            .UserUniquePhoneInRequest()
            .WithName("Número de celular");

        RuleFor(U => U.Roles)
            .NotEmpty()
            .NotNull()
            .ExistRoleInRequest()
            .WithName("Roles de usuario");

        RuleFor(U => U.UserClaims)
            .NotEmpty()
            .NotNull()
            .WithName("Reclamos de usuario");
    }
}
