namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Constants;
using SysCredit.Api.Requests.LoanTypes;

using static SysCredit.Api.Properties.ErrorCodeMessages;
using static SysCredit.Api.Properties.SysCreditMessages;
using static SysCredit.Api.Constants.ErrorCodes;
/// <summary>
///     Clase validadora de <see cref="DeleteLoanTypeRequest"/>.
/// </summary>
public class DeleteLoanTypeValidator : AbstractValidator<DeleteLoanTypeRequest>
{
    /// <summary>
    ///     Valida el Id del tipo de préstamo.
    /// </summary>
    public DeleteLoanTypeValidator()
    {
        RuleFor(L => L.LoanTypeId)
            .NotNull().WithErrorCode(SERVLT0101).WithMessage(GetMessageFromCode(SERVLT0101)!)
            .VerifyLoanTypeReferenceAsync().WithErrorCode(SERVLT0104).WithMessage(GetMessageFromCode(SERVLT0104)!)
            .WithName(GetMessage("LoanType Id")!);
    }
}