namespace SysCredit.Api.Requests.Guarantors;

/// <summary>
///     Este request recibe un id del fiador de la url
/// </summary>
public record class GuarantorIdRequest : IRequest
{
    /// <summary>
    ///     Id del fiador que vienen de la URL
    /// </summary>
    public long? GuarantorId { get; set; }
    
    /// <summary>
    ///     Operador para convertir un long? al tipo GuarantorIdRequest
    /// </summary>
    /// <param name="GuarantorId">Id del fiador</param>
    public static implicit operator GuarantorIdRequest(long? GuarantorId) => new() { GuarantorId = GuarantorId };

    /// <summary>
    ///     Operador para convertir un tipo GuarantorIdRequest a long?
    /// </summary>
    /// <param name="Request">Objeto de tipo GuarantorIdRequest</param>
    public static implicit operator long?(GuarantorIdRequest Request) => Request.GuarantorId; 
}
