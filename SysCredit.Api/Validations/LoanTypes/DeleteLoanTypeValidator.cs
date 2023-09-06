namespace SysCredit.Api.Validations.LoanTypes;

using SysCredit.Api.Extensions;
using FluentValidation;
using SysCredit.Api.ViewModels.LoanTypes;

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
