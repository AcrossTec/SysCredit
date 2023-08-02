namespace SysCredit.Api.Extensions;

using FluentValidation;

using SysCredit.Api.Validations;
using SysCredit.Api.Validations.Customers;
using SysCredit.Api.Validations.Guarantors;
using SysCredit.Api.Validations.References;
using SysCredit.Api.Validations.Relationships;
using SysCredit.Api.ViewModels.Customers;
using SysCredit.Api.ViewModels.References;

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

    public static IRuleBuilderOptions<T, string?> GuarantorUniqueEmailAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncGuarantorUniqueEmailValidator<T>());

    public static IRuleBuilderOptions<T, string?> GuarantorUniqueIdentificationAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncGuarantorUniqueIdentificationValidator<T>());

    public static IRuleBuilderOptions<T, string?> GuarantorUniquePhoneAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncGuarantorUniquePhoneValidator<T>());

    public static IRuleBuilderOptions<T, long> ExistsGuarantorAsync<T>(this IRuleBuilder<T, long> RuleBuilder)
    => RuleBuilder.SetAsyncValidator(new AsyncExistsGuarantorValidator<T>());

    public static IRuleBuilderOptions<T, string?> CustomerUniqueEmailAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncCustomerUniqueEmailValidator<T>());

    public static IRuleBuilderOptions<T, string?> CustomerUniqueIdentificationAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncCustomerUniqueIdentificationValidator<T>());

    public static IRuleBuilderOptions<T, string?> CustomerUniquePhoneAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncCustomerUniquePhoneValidator<T>());

    public static IRuleBuilderOptions<T, CustomerGuarantorRequest> ExistsGuarantorAndRelationship<T>(this IRuleBuilder<T, CustomerGuarantorRequest> RuleBuilder)
        => RuleBuilder.SetValidator(new CustomerGuarantorValidator());

    public static IRuleBuilderOptions<T, CreateReferenceRequest> CreateReferenceIsValid<T>(this IRuleBuilder<T, CreateReferenceRequest> RuleBuilder)
        => RuleBuilder.SetValidator(new CreateReferenceValidator());
}
