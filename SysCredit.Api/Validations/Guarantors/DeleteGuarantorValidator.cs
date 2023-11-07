namespace SysCredit.Api.Validations.Guarantors;

using SysCredit.Api.Requests.Guarantors;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;

using FluentValidation;

using static SysCredit.Api.Properties.ErrorCodeMessages;
using static SysCredit.Api.Properties.SysCreditMessages;
using static SysCredit.Api.Constants.ErrorCodes;

public class DeleteGuarantorValidator : AbstractValidator<DeleteGuarantorRequest>
{
    public DeleteGuarantorValidator()
    {
        RuleFor(G => G.GuarantorId)
            .VeryfyIfExistsCustomerByGuarantorIdAsync().WithErrorCode(SERVG0105).WithMessage(GetMessageFromCode(SERVG0105)!)
            .WithName(GetMessage("Guarantor Id")!);
    }
}