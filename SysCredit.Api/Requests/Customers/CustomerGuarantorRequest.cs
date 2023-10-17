namespace SysCredit.Api.Requests.Customers;

/// <summary>
///     Busqueda por <see cref="LoanId"/> y <see cref="GuarantorId"/> y <see cref="RelationshipId"/>.
/// </summary>
public class CustomerGuarantorRequest : IRequest
{
    /// <summary>
    ///     Obtiene o establece el Id del préstamo.
    /// </summary>
    public long? LoanId { get; set; }

    /// <summary>
    ///     Obtiene o establece el Id del fiador respecto al cliente.
    /// </summary>
    public long GuarantorId { get; set; }

    /// <summary>
    ///     Obtiene o establece el Id del parentesco que tiene respecto al cliente.
    /// </summary>
    public long RelationshipId { get; set; }
}
