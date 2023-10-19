namespace SysCredit.Api.Requests.Guarantors;

using SysCredit.Api.Validations.Guarantors;
using SysCredit.Api.Attributes;

/// <summary>
///     Request para eliminar un fiador si no está siendo usado.
/// </summary>
[Validator<DeleteGuarantorValidator>]
public class DeleteGuarantorRequest : IRequest
{
    /// <summary>
    ///     Id del fiador que se desea eliminar.
    /// </summary>
    public long? GuarantorId { get; set; }
}