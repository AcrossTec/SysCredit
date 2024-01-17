namespace SysCredit.Api.Services;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests;
using SysCredit.Api.Requests.Guarantors;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using static Constants.ErrorCodePrefix;
using static SysCredit.Helpers.ContextData;

/// <summary>
///     Servicio del modelo <see cref="Guarantor"/>
/// </summary>
/// <param name="Store">Repositorio del modelo <see cref="Guarantor"/></param>
[Service<IGuarantorService>]
[ServiceModel<Guarantor>]
[ErrorCategory(nameof(GuarantorService))]
[ErrorCodePrefix(GuarantorServicePrefix)]
public partial class GuarantorService(IStore Store)
{
    private readonly IStore<Customer> CustomerStore = Store.GetStore<Customer>();
    private readonly IStore<Guarantor> GuarantorStore = Store.GetStore<Guarantor>();
    private readonly IStore<Relationship> RelationshipStore = Store.GetStore<Relationship>();

    /// <summary>
    ///     Servicio para validar y crear un nuevo Guarantor
    /// </summary>
    /// <param name="Request">
    ///     Recibe los datos necesarios para crear un Guarantor
    /// </param>
    /// <returns>Retorna el id del Guarantor</returns>
    [MethodId("9FE9602F-7011-435F-83BE-F573704A932D")]
    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "<Pending>")]
    public async ValueTask<EntityId> InsertGuarantorAsync(CreateGuarantorRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(
            Key(nameof(RelationshipStore)).Value(RelationshipStore)
           .Key(nameof(GuarantorStore)).Value(GuarantorStore));

        return await GuarantorStore.InsertGuarantorAsync(Request);
    }

    /// <summary>
    ///     Servicio para eliminar un Guarantor.
    /// </summary>
    /// <param name="Request">Recibe el id del Guarantor.</param>
    /// <returns></returns>
    [MethodId("6C5CFD99-1940-487F-8637-88B6033A6216")]
    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "<Pending>")]
    public async ValueTask<bool> DeleteGuarantorAsync(DeleteGuarantorRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(Key(nameof(CustomerStore)).Value(CustomerStore));
        return await GuarantorStore.DeleteGuarantorAsync(Request);
    }

    /// <summary>
    ///     Obtiene todos los Guarantor
    /// </summary>
    /// <returns>Retorna una lista de Guarantors</returns>
    [MethodId("F156EE14-0CB8-477E-B9BF-0B864E26BF25")]
    public IAsyncEnumerable<FetchGuarantor> FetchGuarantorsAsync()
    {
        return GuarantorStore.FetchGuarantorsAsync();
    }

    /// <summary>
    ///     Obtiene el Guarantor por su id
    /// </summary>
    /// <param name="GuarantorId">Id del Guarantor</param>
    /// <returns>Retorna el Guarantor</returns>
    [MethodId("E1C42C3D-681C-4AEA-A412-68D02482DC6D")]
    public ValueTask<GuarantorInfo?> FetchGuarantorByIdAsync(long? GuarantorId)
    {
        return GuarantorStore.FetchGuarantorByIdAsync(GuarantorId);
    }

    /// <summary>
    ///     Obtiene todos los Guarantor limitando el alcance
    /// </summary>
    /// <param name="Request">Recibe el inicio de la busqueda y cuantos registros buscará</param>
    /// <returns>Retorna una lista de Guarantors</returns>
    [MethodId("4007331B-2C71-4DAF-8B00-15CBB3B3328C")]
    public IAsyncEnumerable<FetchGuarantor> FetchGuarantorsAsync(PaginationRequest Request)
    {
        return GuarantorStore.FetchGuarantorsAsync(Request);
    }

    /// <summary>
    ///     Busca Guarantors por su valor
    /// </summary>
    /// <param name="Request">Recibe el valor del Guarantor a Buscar</param>
    /// <returns>Retorna una lista de Guarantors que coincida con el valor</returns>
    [MethodId("543DDE99-6927-4D4D-928F-A47CD6695114")]
    public IAsyncEnumerable<GuarantorInfo> SearchGuarantorAsync(SearchRequest Request)
    {
        return GuarantorStore.SearchGuarantorAsync(Request);
    }
}
