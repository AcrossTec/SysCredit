namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.LoanTypes;

using static Constants.ErrorCodes;
using static Constants.ErrorCodePrefix;
using SysCredit.Api.Constants;

/// <summary>
///     Clase validadora de <see cref="UpdateLoanTypeRequest"/>.
/// </summary>
public class UpdateLoanTypeValidator : AbstractValidator<UpdateLoanTypeRequest>
{
    /// <summary>
    ///     Valida el nombre del tipo de préstamo.
    /// </summary>
    public UpdateLoanTypeValidator()
    {
        RuleFor(L => L.Name)
            .NotEmpty()
            .NotNull()
            .LoanTypeUniqueNameAsync().WithErrorCode(ErrorCodes.SERVLT0101)
            .WithName("Nombre");
    }
}
