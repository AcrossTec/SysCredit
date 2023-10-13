namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.LoanTypes;

using static Constants.ErrorCodes;
using static Constants.ErrorCodePrefix;

/// <summary>
///     Clase validadora de <see cref="UpdateLoanTypeRequest"/>
/// </summary>
public class UpdateLoanTypeValidator : AbstractValidator<UpdateLoanTypeRequest>
{
    public UpdateLoanTypeValidator()
    {
        RuleFor(L => L.Name)
            .NotEmpty()
            .NotNull()
            .LoanTypeUniqueNameAsync()
            .WithName("Nombre");
    }
}
