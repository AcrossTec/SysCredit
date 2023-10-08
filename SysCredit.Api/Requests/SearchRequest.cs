namespace SysCredit.Api.Requests;

/// <summary>
///     <see langword="record" /> como criterio de busqueda en los registros de base de datos. <br />
///     Los Endpoints que usen este request deben regresar sus datos paginados.
/// </summary>
public record class SearchRequest : PaginationRequest
{
    /// <summary>
    ///     Representa el valor que se buscará en los registros de base de datos.
    /// </summary>
    public string? Value { get; set; }
}
