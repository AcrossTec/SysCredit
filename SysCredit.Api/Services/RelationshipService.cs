namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.Relationships;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    ///     Obtiene un registro de la tabla <see cref="Models.Relationship"/>
    /// </summary>
    /// <param name="RelationshipId">
    ///     Id obtenido de la ruta
    /// </param>
    /// <returns>
    ///     Regresa un registro de la tabla <see cref="Models.Relationship"/>
    /// </returns>
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
    ///     Valida e invoca al Store para modificar <see cref="Models.Relationship"/>
    /// </summary>
    /// <param name="Request">
    ///     Datos que se van a actualizar del <see cref="Models.Relationship"/> 
    /// </param>
    /// <returns>
    ///     Retorna bool
    /// </returns>
    [MethodId("E11EAEEB-5A72-40DE-BBEF-4AC44BCC2FB5")]
    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "<Pending>")]
    public async ValueTask<bool> UpdateRelationshipAsync(UpdateRelationshipRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(Key(nameof(RelationshipStore)).Value(RelationshipStore));
        return await RelationshipStore.UpdateRelationshipAsync(Request);
    }

    /// <summary>
    ///     Valida y crea un nuevo <see cref="Models.Relationship"/> en la base de datos
    /// </summary>
    /// <param name="Request">
    ///     Datos usado para crear un <see cref="Models.Relationship"/>
    /// </param>
    /// <returns>
    ///     Regresa el nuevo Id del <see cref="Models.Relationship"/>
    /// </returns>
    [MethodId("D74F0DFE-4F39-4E24-A871-355CCD96A377")]
    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "<Pending>")]
    public async ValueTask<EntityId> InsertRelationshipAsync(CreateRelationshipRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(Key(nameof(RelationshipStore)).Value(RelationshipStore));
        return await RelationshipStore.InsertRelationshipAsync(Request);
    }

    /// <summary>
    ///     Valida e invoca al Store para eliminar el <see cref="Models.Relationship"/>
    /// </summary>
    /// <param name="Request">
    ///     Id del <see cref="Models.Relationship"/> a eliminar
    /// </param>
    /// <returns>
    ///     Retorna un bool
    /// </returns>
    [MethodId("CCA4A939-6D07-4FFF-B8A8-D3337A8B1C23")]
    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "<Pending>")]
    public async ValueTask<bool> DeleteRelationshipAsync(DeleteRelationshipRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(Key(nameof(RelationshipStore)).Value(RelationshipStore));
        return await RelationshipStore.DeleteRelationshipAsync(Request);
    }
}
