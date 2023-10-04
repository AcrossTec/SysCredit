namespace SysCredit.Api.Validations.Guarantors;

using SysCredit.Api.Requests.Guarantors;
using FluentValidation;
using SysCredit.Api.Extensions;

public class DeleteGuarantorValidator : AbstractValidator<DeleteGuarantorRequest>
{
    public DeleteGuarantorValidator()
    {
        RuleFor(G => G.GuarantorId)
            .VeryfyIfExistsCustomerByGuarantorIdAsync()
            .WithName("Fiador");
    }
}