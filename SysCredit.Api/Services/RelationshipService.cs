namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.Relationships;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

using static Constants.ErrorCodePrefix;
using static SysCredit.Helpers.ContextData;

/// <summary>
///     Realiza distintas operacinones sobre <see cref="Relationship"/> como: Crear, Borrar, Buscar, Editar, etc...
/// </summary>
/// <param name="RelationshipStore">
///     Tienda de datos para <see cref="Relationship"/>.
/// </param>
[Service<IRelationshipService>]
[ServiceModel<Relationship>]
[ErrorCategory(nameof(RelationshipService))]
[ErrorCodePrefix(RelationshipServicePrefix)]
public partial class RelationshipService(IStore<Relationship> RelationshipStore)
{
    /// <summary>
    ///     Obtiene todos los <see cref="Relationship"/>.
    /// </summary>
    /// <returns>
    ///     Regresa todos los <see cref="Relationship"/>.
    /// </returns>
    [MethodId("51EC53A1-3F6E-4BBB-BC32-909862FD119B")]
    public IAsyncEnumerable<RelationshipInfo> FetchRelationshipAsync()
    {
        return RelationshipStore.FetchRelationshipAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="RelationshipId"></param>
    /// <returns></returns>
    [MethodId("17B4C153-16C2-4331-9AFF-C8F18350EAC6")]
    public ValueTask<RelationshipInfo?> FetchRelationshipByIdAsync(long? RelationshipId)
    {
        return RelationshipStore.FetchRelationshipByIdAsync(RelationshipId);
    }

    /// <summary>
    ///     Verifica si el <paramref name="RelationshipId"/> está registrado en base de datos.
    /// </summary>
    /// <param name="RelationshipId">
    ///     Id del parentesco ha buscar.
    /// </param>
    /// <returns>
    ///     Regresa True si existe <paramref name="RelationshipId"/> en caso contrario False.
    /// </returns>
    public async ValueTask<bool> ExistsRelationshipAsync(long? RelationshipId)
    {
        var Relationship = await RelationshipStore.FetchRelationshipByIdAsync(RelationshipId);
        return Relationship is not null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("E11EAEEB-5A72-40DE-BBEF-4AC44BCC2FB5")]
    public async ValueTask<bool> UpdateRelationshipAsync(UpdateRelationshipRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(Key(nameof(RelationshipStore)).Value(RelationshipStore));
        return await RelationshipStore.UpdateRelationshipAsync(Request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("D74F0DFE-4F39-4E24-A871-355CCD96A377")]
    public ValueTask<long> InsertRelationshipAsync(CreateRelationshipRequest Request)
    {
        return RelationshipStore.InsertRelationshipAsync(Request);
    }
}
