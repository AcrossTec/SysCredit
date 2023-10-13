namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.LoanType;

/// <summary>
///     Clase validadora de <see cref="CreateLoanTypeRequest"/>
/// </summary>
public class CreateLoanTypeValidator : AbstractValidator<CreateLoanTypeRequest>
{
    /// <summary>
    ///     Valida el nombre del tipo de prestamo
    /// </summary>
    public CreateLoanTypeValidator()
    {
        RuleFor(T => T.Name)
            .NotEmpty()
            .NotNull()
            .LoanTypeUniqueNameAsync()
            .WithName("Nombre");
    }
}
