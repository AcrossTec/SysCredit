namespace SysCredit.Api.Requests.LoanTypes;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.LoanTypes;

/// <summary>
///     Request para crear un nuevo tipo de préstamo unico.
/// </summary>
[Validator<CreateLoanTypeValidator>]
public class CreateLoanTypeRequest : IRequest
{
    /// <summary>
    ///     Nombre del tipo de préstamo.
    /// </summary>
    public string Name { get; set; } = String.Empty;
}
