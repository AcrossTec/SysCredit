namespace SysCredit.Api.Requests.Customers;

/// <summary>
///     Criterio de busqueda por <see cref="CustomerId" /> y <see cref="GuarantorId" />.
/// </summary>
public record class GuarantorAndCustomerIdsRequest : IRequest
{
    /// <summary>
    ///     Id del registro del cliente que se buscará.
    /// </summary>
    public long? CustomerId { get; set; }

    /// <summary>
    ///     Id del fiador del cliente que se buscará.
    /// </summary>
    public long? GuarantorId { get; set; }
}
