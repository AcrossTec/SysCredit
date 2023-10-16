namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.LoanType;
using SysCredit.Api.Requests.LoanTypes;
using SysCredit.Api.Stores;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Collections.Generic;

using static Constants.ErrorCodePrefix;
using static SysCredit.Helpers.ContextData;

/// <summary>
///     Servicio del modelo <see cref="LoanType"/>
/// </summary>
/// <param name="LoanTypeStore">repositorio del modelo <see cref="LoanType"/></param>
[Service<ILoanTypeService>]
[ServiceModel<LoanType>]
[ErrorCategory(nameof(LoanTypeService))]
[ErrorCodePrefix(LoanTypeServicePrefix)]
public partial class LoanTypeService(IStore<LoanType> LoanTypeStore)
{
    /// <summary>
    ///     Obtiene todos los tipos de prestamo
    /// </summary>
    /// <returns>Retorna una lista de tipos de prestamo</returns>
    [MethodId("F0D2105D-BDEB-4CC9-BD61-3CA6DF382C03")]
    public IAsyncEnumerable<LoanTypeInfo> FetchLoanTypeAsync()
    {
        return LoanTypeStore.FetchLoanTypeAsync();
    }

    /// <summary>
    ///     Obtiene todos los registro de tipo de prestamo sin procesar su información
    /// </summary>
    /// <returns>Retorna una lista de tipos de prestamo</returns>
    [MethodId("702F277C-9B52-4CD2-84E2-85B9B8352E36")]
    public IAsyncEnumerable<LoanType> FetchLoanTypeCompleteAsync()
    {
        return LoanTypeStore.FetchLoanTypeCompleteAsync();
    }

    /// <summary>
    ///     Obtiene un tipo de prestamo por su id
    /// </summary>
    /// <param name="LoanTypeId">Id del prestamo</param>
    /// <returns>Retorna el tipo de prestamo</returns>
    [MethodId("53143E7A-CB8B-45ED-AE3A-F7DD55AD907E")]
    public ValueTask<LoanTypeInfo?> FetchLoanTypeByIdAsync(long? LoanTypeId)
    {
        return LoanTypeStore.FetchLoanTypeByIdAsync(LoanTypeId);
    }

    /// <summary>
    ///     Elimina un tipo de prestamo
    /// </summary>
    /// <param name="Request">Recibe el id del tipo de prestamo</param>
    /// <returns></returns>
    [MethodId("B4850869-6F13-4BAB-87C6-FF8F08B31A95")]
    public async ValueTask<bool> DeleteLoanTypeAsync(DeleteLoanTypeRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(Key(nameof(LoanTypeStore)).Value(LoanTypeStore));
        return await LoanTypeStore.DeleteLoanTypeAsync(Request);
    }

    /// <summary>
    ///     Servicio para validar y crear el nuevo tipo de prestamo
    /// </summary>
    /// <param name="Request">Recibe el nombre para el tipo de prestamo</param>
    /// <returns>Retorna el id del tipo de prestamo</returns>
    [MethodId("09F1FC4B-5456-47CF-9F46-41F96683E7E1")]
    public async ValueTask<EntityId> InsertLoanTypeAsync(CreateLoanTypeRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(Key(nameof(LoanTypeStore)).Value(LoanTypeStore));
        return await LoanTypeStore.InsertLoanTypeAsync(Request);
    }

    /// <summary>
    ///     Servicio para validar y actualizar el tipo de prestamo
    /// </summary>
    /// <param name="Request">Contiene información del LoanType que se va modificar</param>
    /// <returns></returns>
    [MethodId("11531707-8C0B-45FC-B9F1-4418897AC8A7")]
    public async ValueTask<bool> UpdateLoanTypeAsync(UpdateLoanTypeRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(Key(nameof(LoanTypeStore)).Value(LoanTypeStore));
        return await LoanTypeStore.UpdateLoanTypeAsync(Request);
    }
}
