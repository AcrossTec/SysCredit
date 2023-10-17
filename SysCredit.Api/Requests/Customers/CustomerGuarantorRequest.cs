namespace SysCredit.Api.Requests.Customers;

/// <summary>
///     Clase que representa una solicitud de garantía de cliente.
/// </summary>
public class CustomerGuarantorRequest : IRequest
{
    /// <summary>
    ///     Obtiene o establece el ID del préstamo.
    /// </summary>
    public long? LoanId { get; set; }

    /// <summary>
    ///     Obtiene o establece el ID del garante.
    /// </summary>
    public long GuarantorId { get; set; }

    /// <summary>
    ///     Obtiene o establece el ID de la relación.
    /// </summary>
    public long RelationshipId { get; set; }
}
