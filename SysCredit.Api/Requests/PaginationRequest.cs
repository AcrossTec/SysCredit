namespace SysCredit.Api.Requests;

/// <summary>
///     Record usado como <see langword="record" /> base de los resultados paginados.
/// </summary>
public record class PaginationRequest : IRequest
{
    /// <summary>
    ///     Indica cuantos registros se va ha desplazar comenzando del registro 0.
    /// </summary>
    public int? Offset { get; set; }

    /// <summary>
    ///     Indica cuantos registros seran tomados después del desplazamiento de <see cref="Offset" />.
    /// </summary>
    public int? Limit { get; set; }
}
