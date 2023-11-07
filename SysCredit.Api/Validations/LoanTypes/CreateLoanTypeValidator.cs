namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Constants;
using SysCredit.Api.Requests.LoanType;

using static SysCredit.Api.Properties.ErrorCodeMessages;
using static SysCredit.Api.Properties.SysCreditMessages;
using static SysCredit.Api.Constants.ErrorCodes;

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
            .NotEmpty().WithErrorCode(SERVLT0101).WithMessage(GetMessageFromCode(SERVLT0101)!)
            .NotNull().WithErrorCode(SERVLT0102).WithMessage(GetMessageFromCode(SERVLT0102)!)
            .LoanTypeUniqueNameAsync().WithErrorCode(SERVLT0103).WithMessage(GetMessageFromCode(SERVLT0103)!)
            .WithName(GetMessage("Name")!);
    }
}
 