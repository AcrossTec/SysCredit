namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Constants;
using SysCredit.Api.Requests.LoanTypes;

public class DeleteLoanTypeValidator : AbstractValidator<DeleteLoanTypeRequest>
{
    public DeleteLoanTypeValidator()
    {
        RuleFor(L => L.LoanTypeId)
            .NotNull()
            .VerifyLoanTypeReferenceAsync().WithErrorCode(ErrorCodes.SERVLT0102)
            .WithName("Tipo de Prestamo");
    }
}
