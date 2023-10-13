namespace SysCredit.Api.Requests.LoanTypes;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.LoanTypes;

/// <summary>
///     Request del actualizador de tipo de prestamo
/// </summary>
[Validator<UpdateLoanTypeValidator>]
public class UpdateLoanTypeRequest : IRequest
{
    [FromRoute]
    public long LoanTypeId { get; set; }

    [FromBody]
    public string Name { get; set; } = String.Empty;
}
