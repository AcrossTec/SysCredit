namespace SysCredit.Api.Validations.Authentications.Users;

using FluentValidation;

using SysCredit.Api.Requests.Authentications.Users;

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
