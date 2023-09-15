namespace SysCredit.Api.Validations.Auths.Users;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Requests.Auths;

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