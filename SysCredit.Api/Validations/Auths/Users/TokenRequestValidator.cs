namespace SysCredit.Api.Validations.Auths.Users;

using FluentValidation;

using SysCredit.Api.Requests.Auths.Users;

public class TokenRequestValidator : AbstractValidator<TokenRequest>
{
    public TokenRequestValidator()
    {
        RuleFor(U => U.Password)
            .MaximumLength(20)
            .MinimumLength(5)
            .WithName("Contraseña");

        RuleFor(U => U.Email)
           .NotEmpty()
           .NotNull();
    }
}
