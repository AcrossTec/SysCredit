namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.LoanTypes;

/// <summary>
///     Clase validadora de <see cref="DeleteLoanTypeRequest"/>
/// </summary>
public class DeleteLoanTypeValidator : AbstractValidator<DeleteLoanTypeRequest>
{
    /// <summary>
    ///     Valida el Id del tipo de prestamo
    /// </summary>
    public DeleteLoanTypeValidator()
    {
        RuleFor(L => L.LoanTypeId)
            .NotNull()
            .VerifyLoanTypeReferenceAsync()
            .WithName("Tipo de Prestamo");
    }
}