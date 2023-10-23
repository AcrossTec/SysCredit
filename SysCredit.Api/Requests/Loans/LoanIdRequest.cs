namespace SysCredit.Api.Requests.Loans;
/// <summary>
///     Representa una petición de busqueda por <see cref="LoanId"/> y <see cref="PaymentFrequencyId"/>.
/// </summary>
public class LoanIdRequest
{
    /// <summary>
    ///     Id de registro del Prestamo que se buscara.
    /// </summary>
    public long? LoanId { get; set; }   

    /// <summary>
    ///     Id de la Frequency de pago que se buscara.
    /// </summary>
    public long? PaymentFrequencyId { get; set;}
}