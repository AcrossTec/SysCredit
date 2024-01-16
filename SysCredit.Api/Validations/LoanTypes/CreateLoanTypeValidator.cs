namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;

using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.LoanTypes;

/// <summary>
///     Clase validadora de <see cref="CreateLoanTypeRequest"/>.
/// </summary>
public class CreateLoanTypeValidator : AbstractValidator<CreateLoanTypeRequest>
{
    /// <summary>
    ///     Valida el nombre del tipo de préstamo.
    /// </summary>
    public CreateLoanTypeValidator()
    {
        RuleFor(T => T.Name)
            .NotEmpty()
            .NotNull()
            .LoanTypeUniqueNameAsync().WithErrorCode(ErrorCodes.SERVLT0101)
            .WithName("Nombre");
    }
}
