namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.ViewModels.LoanTypes;

public class UpdateLoanTypeValidator : AbstractValidator<UpdateLoanTypeRequest>
{
    public UpdateLoanTypeValidator()
    {
        RuleFor(L => L.Name)
            .NotEmpty()
            .NotNull()
            .LoanTypeUniqueNameAsync()
            .WithName("Nombre");

        RuleFor(L => L.LoanTypeId)
            .NotEmpty()
            .NotNull()
            .WithName("Id del Tipo de Prestamo");
    }
}
