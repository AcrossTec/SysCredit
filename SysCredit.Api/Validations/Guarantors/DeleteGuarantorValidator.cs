namespace SysCredit.Api.Validations.Guarantors;

using SysCredit.Api.Requests.Guarantors;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;

using FluentValidation;

public class DeleteGuarantorValidator : AbstractValidator<DeleteGuarantorRequest>
{
    public DeleteGuarantorValidator()
    {
        RuleFor(G => G.GuarantorId)
            .VeryfyIfExistsCustomerByGuarantorIdAsync().WithErrorCode(ErrorCodes.SERVG0105)
            .WithName("Fiador");
    }
}