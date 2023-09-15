namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.LoanTypes;

public class DeleteLoanTypeValidator : AbstractValidator<DeleteLoanTypeRequest>
{
    public DeleteLoanTypeValidator()
    {
        RuleFor(L => L.LoanTypeId)
            .NotNull()
            .VerifyLoanTypeReferenceAsync()
            .WithName("Tipo de Prestamo");
    }
}
