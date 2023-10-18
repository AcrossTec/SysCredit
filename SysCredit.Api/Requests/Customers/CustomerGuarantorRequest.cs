namespace SysCredit.Api.Requests.Customers;

/// <summary>
///     Busqueda por <see cref="LoanId"/> y <see cref="GuarantorId"/> y <see cref="RelationshipId"/>.
/// </summary>
public class CustomerGuarantorRequest : IRequest
{
    /// <summary>
    ///     Id del prestamo que se buscara.
    /// </summary>
    public long? LoanId { get; set; }

    /// <summary>
    ///     Id del Fiador que se buscara.
    /// </summary>
    public long GuarantorId { get; set; }

    /// <summary>
    ///     Id de la Relación que se buscara.
    /// </summary>
    public long RelationshipId { get; set; }
}
