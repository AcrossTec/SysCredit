namespace SysCredit.Api.Extensions;

using FluentValidation;

using SysCredit.Api.Validations;
using SysCredit.Api.Validations.Relationships;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<TObject, TEnum> Enum<TObject, TEnum>(this IRuleBuilder<TObject, TEnum> RuleBuilder) where TEnum : Enum
        => RuleBuilder.SetValidator(new EnumValidator<TObject, TEnum>());

    public static IRuleBuilderOptions<T, string?> Identification<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetValidator(new IdentificationValidator<T>());

    public static IRuleBuilderOptions<T, string?> Phone<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetValidator(new PhoneValidator<T>());

    public static IRuleBuilderOptions<T, long> ExistsRelationshipAsync<T>(this IRuleBuilder<T, long> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncExistsRelationshipValidator<T>());
}
