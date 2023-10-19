namespace SysCredit.Api.Requests.LoanTypes;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.LoanTypes;

/// <summary>
///     Request para actualizar el tipo de préstamo con un nombre único.
/// </summary>
[Validator<UpdateLoanTypeValidator>]
public class UpdateLoanTypeRequest : IRequest
{
    /// <summary>
    ///     Id del tipo de préstamo que se desea actualizar proveniente de la ruta.
    /// </summary>
    [FromRoute]
    public long LoanTypeId { get; set; }

    /// <summary>
    ///     Nuevo nombre del tipo de préstamo obtenido del cuerpo de la solicitud.
    /// </summary>
    [FromBody]
    public string Name { get; set; } = String.Empty;
}
