namespace SysCredit.Api.Requests.LoanTypes;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.LoanTypes;

/// <summary>
///     Request para eliminar un tipo de préstamo que no está siendo usado.
/// </summary>
[Validator<DeleteLoanTypeValidator>]
public class DeleteLoanTypeRequest : IRequest
{
    /// <summary>
    ///     Id del tipo de préstamo que se desea eliminar.
    /// </summary>
    public long? LoanTypeId { get; set; }
}
