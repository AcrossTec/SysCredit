namespace SysCredit.Api.Extensions;

using FluentValidation;

using SysCredit.Api.Requests.Authentications;
using SysCredit.Api.Requests.Customers;
using SysCredit.Api.Requests.References;

using SysCredit.Api.Validations;
using SysCredit.Api.Validations.Authentications.Roles;
using SysCredit.Api.Validations.Authentications.Users;
using SysCredit.Api.Validations.Customers;
using SysCredit.Api.Validations.Guarantors;
using SysCredit.Api.Validations.LoanTypes;
using SysCredit.Api.Validations.PaymentFrequencies;
using SysCredit.Api.Validations.References;
using SysCredit.Api.Validations.Relationships;

public static partial class ValidatorExtensions
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

    public static IRuleBuilderOptions<T, IEnumerable<CustomerGuarantorRequest>> CustomerGuarantorsUniqueInRequest<T>(this IRuleBuilder<T, IEnumerable<CustomerGuarantorRequest>> RuleBuilder)
        => RuleBuilder.SetValidator(new CustomerGuarantorsUniqueInRequestValidator<T>());

    public static IRuleBuilderOptions<T, long?> VerifyLoanTypeReferenceAsync<T>(this IRuleBuilder<T, long?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncVerifyLoanTypeReferenceValidator<T>());

    public static IRuleBuilderOptions<T, string?> LoanTypeUniqueNameAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncLoanTypeUniqueNameValidator<T>());

    public static IRuleBuilderOptions<T, string?> PaymentFrequencyUniqueNameAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncPaymentFrequencyUniqueNameValidator<T>());

    public static IRuleBuilderOptions<T, long?> VerifyRouteWithPaymentFrequencyId<T>(this IRuleBuilder<T, long?> RuleBuilder)
        => RuleBuilder.SetValidator(new VerifyRouteWithPaymentFrequencyIdValidator<T>());

    public static IRuleBuilderOptions<T, IEnumerable<AssignTypeRequest>> ExistRoleInRequest<T>(this IRuleBuilder<T, IEnumerable<AssignTypeRequest>> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncExistRoleValidator<T>());

    public static IRuleBuilderOptions<T, string?> UserUniqueEmailInRequest<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncUserUniqueEmailValidator<T>());

    public static IRuleBuilderOptions<T, string?> UserUniquePhoneInRequest<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncUserUniquePhoneValidator<T>());

    public static IRuleBuilderOptions<T, string?> UserUniqueUserNameInRequest<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncUserUniqueUserNameValidator<T>());

    public static IRuleBuilderOptions<T, string?> UniqueRoleNameAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncExistRoleNameValidator<T>());

    public static IRuleBuilderOptions<T, long?> VerifyIfCustomerExistsByIdAsync<T>(this IRuleBuilder<T, long?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncVerifyIfCustomerExistsByIdValidator<T>());

    public static IRuleBuilderOptions<T, long?> VerifyRouteWithLoanTypeId<T>(this IRuleBuilder<T, long?> RuleBuilder)
        => RuleBuilder.SetValidator(new VerifyRouteWithLoanTypeIdValidator<T>());

    public static IRuleBuilderOptions<T, string?> RelationshipUniqueNameAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncRelationshipUniqueNameValidator<T>());
}
