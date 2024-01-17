namespace SysCredit.Api.Extensions;

using FluentValidation;

using SysCredit.Api.Requests.Customers;
using SysCredit.Api.Requests.References;

using SysCredit.Api.Validations;
using SysCredit.Api.Validations.Customers;
using SysCredit.Api.Validations.Guarantors;
using SysCredit.Api.Validations.LoanTypes;
using SysCredit.Api.Validations.PaymentFrequencies;
using SysCredit.Api.Validations.References;
using SysCredit.Api.Validations.Relationships;

/// <summary>
/// 
/// </summary>
public static partial class ValidatorExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<TObject, TEnum> Enum<TObject, TEnum>(this IRuleBuilder<TObject, TEnum> RuleBuilder) where TEnum : Enum
        => RuleBuilder.SetValidator(new EnumValidator<TObject, TEnum>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string?> Identification<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetValidator(new IdentificationValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string?> Phone<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetValidator(new PhoneValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, long> ExistsRelationshipAsync<T>(this IRuleBuilder<T, long> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncExistsRelationshipValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string?> GuarantorUniqueEmailAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncGuarantorUniqueEmailValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string?> GuarantorUniqueIdentificationAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncGuarantorUniqueIdentificationValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string?> GuarantorUniquePhoneAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncGuarantorUniquePhoneValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, long> ExistsGuarantorAsync<T>(this IRuleBuilder<T, long> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncExistsGuarantorValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string?> CustomerUniqueEmailAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncCustomerUniqueEmailValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string?> CustomerUniqueIdentificationAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncCustomerUniqueIdentificationValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string?> CustomerUniquePhoneAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncCustomerUniquePhoneValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, CustomerGuarantorRequest> ExistsGuarantorAndRelationship<T>(this IRuleBuilder<T, CustomerGuarantorRequest> RuleBuilder)
        => RuleBuilder.SetValidator(new CustomerGuarantorValidator());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, CreateReferenceRequest> CreateReferenceIsValid<T>(this IRuleBuilder<T, CreateReferenceRequest> RuleBuilder)
        => RuleBuilder.SetValidator(new CreateReferenceValidator());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, IEnumerable<CustomerGuarantorRequest>> CustomerGuarantorsUniqueInRequest<T>(this IRuleBuilder<T, IEnumerable<CustomerGuarantorRequest>> RuleBuilder)
        => RuleBuilder.SetValidator(new CustomerGuarantorsUniqueInRequestValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, long?> VerifyLoanTypeReferenceAsync<T>(this IRuleBuilder<T, long?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncVerifyLoanTypeReferenceValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string?> LoanTypeUniqueNameAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncLoanTypeUniqueNameValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string?> PaymentFrequencyUniqueNameAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncPaymentFrequencyUniqueNameValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, long?> VerifyRouteWithPaymentFrequencyId<T>(this IRuleBuilder<T, long?> RuleBuilder)
        => RuleBuilder.SetValidator(new VerifyRouteWithPaymentFrequencyIdValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, long?> VerifyIfCustomerExistsByIdAsync<T>(this IRuleBuilder<T, long?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncVerifyIfCustomerExistsByIdValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string?> RelationshipUniqueNameAsync<T>(this IRuleBuilder<T, string?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncRelationshipUniqueNameValidator<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RuleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, long?> VeryfyIfExistsCustomerByGuarantorIdAsync<T>(this IRuleBuilder<T, long?> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncVerifyifCustomerByGuarantorIdValidator<T>());
}
