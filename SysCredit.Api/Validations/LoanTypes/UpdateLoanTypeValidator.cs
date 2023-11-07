namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.LoanTypes;

using static Constants.ErrorCodes;
using static Constants.ErrorCodePrefix;
using SysCredit.Api.Constants;

using static SysCredit.Api.Properties.ErrorCodeMessages;
using static SysCredit.Api.Properties.SysCreditMessages;
using static SysCredit.Api.Constants.ErrorCodes;

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
            .NotEmpty().WithErrorCode(SERVLT0101).WithMessage(GetMessageFromCode(SERVLT0101)!)
            .NotNull().WithErrorCode(SERVLT0102).WithMessage(GetMessageFromCode(SERVLT0102)!)
            .LoanTypeUniqueNameAsync().WithErrorCode(SERVLT0105).WithMessage(GetMessageFromCode(SERVLT0105)!)
            .WithName(GetMessage("Name")!);
    }
}
