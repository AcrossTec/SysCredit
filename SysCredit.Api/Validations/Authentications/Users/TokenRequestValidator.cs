namespace SysCredit.Api.Validations.Authentications.Users;

using FluentValidation;

using SysCredit.Api.Requests.Authentications.Users;

/// <summary>
///     Clase Validadora de <see cref="TokenRequestValidator"/>
/// </summary>
public class TokenRequestValidator : AbstractValidator<TokenRequest>
{
    /// <summary>
    ///     Valida la contraseña y el Email.<see cref="TokenRequestValidator"/>
    /// </summary>
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