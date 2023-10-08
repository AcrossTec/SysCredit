namespace SysCredit.Api.Interfaces.Services;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

/// <summary>
///     Servicio para las distintas operaciones de los parentesco de los fiadores.
/// </summary>
public partial interface IRelationshipService : IService<Relationship>
{
    /// <summary>
    ///     Obtiene todos los parentesco de la base de datos.
    /// </summary>
    /// <returns>
    ///     Regresa todos los parentescos registrados en base de datos.
    /// </returns>
    IAsyncEnumerable<RelationshipInfo> FetchRelationshipAsync();

    /// <summary>
    ///     Verifica si el parentesco con este Id existe.
    /// </summary>
    /// <param name="RelationshipId"></param>
    /// <returns>
    ///     Regresa <see langword="true" /> si exist el parentesco con el <paramref name="RelationshipId" />
    ///     ó <see langword="false" /> en caso contrario.
    /// </returns>
    ValueTask<bool> ExistsRelationshipAsync(long RelationshipId);
}
