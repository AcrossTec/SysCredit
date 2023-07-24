namespace SysCredit.Api.Validations;

using FluentValidation;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string?> Identification<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetValidator(new IdentificationValidator<T>());

    public static IRuleBuilderOptions<T, string?> Phone<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetValidator(new PhoneValidator<T>());
}
