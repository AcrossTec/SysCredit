namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.LoanTypes;

using static Constants.ErrorCodes;
using static Constants.ErrorCodePrefix;

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
            .VerifyRouteWithLoanTypeId().WithErrorCode(SERVLT0004)
            .WithName("Id del Tipo de Prestamo");
    }
}
