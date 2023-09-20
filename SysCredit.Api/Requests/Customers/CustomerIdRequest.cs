namespace SysCredit.Api.Requests.Customers;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Customers;

/// <summary>
///     Este request recibe un id de cliente de la url
/// </summary>
[Validator<CustomerIdRequestValidator>]
public record class CustomerIdRequest : IRequest
{
    /// <summary>
    ///     Id del cliente que viene de la url
    /// </summary>
    public long? CustomerId { get; set; }

    /// <summary>
    ///     Operador para convertir un long? al tipo CustomerIdRequest
    /// </summary>
    /// <param name="CustomerId">Id del cliente</param>
    public static implicit operator CustomerIdRequest(long? CustomerId) => new() { CustomerId = CustomerId };

    /// <summary>
    ///     Operador para convertir un tipo CustomerIdRequest a long?
    /// </summary>
    /// <param name="Request">Objecto de tipo CustomerIdRequest</param>
    public static implicit operator long?(CustomerIdRequest Request) => Request.CustomerId;
}
