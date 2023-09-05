namespace SysCredit.Api.Validations.Auth.Users;

using FluentValidation.Validators;
using FluentValidation;
using SysCredit.Api.ViewModels.Auth;

public class UserClaimUniqueRequestValidator<T> : PropertyValidator<T, IEnumerable<CreateClaimRequest>>
{
    public override bool IsValid(ValidationContext<T> Context, IEnumerable<CreateClaimRequest> GuarantorRequests)
    {
        var Values = GuarantorRequests.DistinctBy(Request => Request.ClaimType.ToLower());
        return GuarantorRequests.Count() == Values.Count();
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Solicitud con registros duplicados: 'ClaimType' debe ser único.";
    }

    public override string Name => "UserClaimUniqueRequestValidator";
}