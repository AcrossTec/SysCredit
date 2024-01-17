namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.Attributes;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests;
using SysCredit.Api.Requests.Guarantors;
using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Data;
using System.Reflection;

using static Constants.ErrorCodePrefix;

/// <summary>
///     Repositorio de <see cref="Guarantor"/>
/// </summary>
[Store]
[ErrorCategory(nameof(GuarantorStore))]
[ErrorCodePrefix(GuarantorStorePrefix)]
public static partial class GuarantorStore
{
    /// <summary>
    ///     Realiza una búsqueda de Guarantors, según el criterio especificado.
    /// </summary>
    /// <param name="Store">Repositorio de <see cref="Guarantor"/>.</param>
    /// <param name="Request">Recibe el valor del Guarantor a Buscar</param>
    /// <returns>Retorna una lista de Guarantors que coincida con el valor</returns>
    [MethodId("419DA003-2593-488F-ADE9-08C2E21122F9")]
    public static IAsyncEnumerable<GuarantorInfo> SearchGuarantorAsync(this IStore<Guarantor> Store, SearchRequest Request)
    {
        return Store.ExecuteStoredProcedureQueryAsync<GuarantorInfo>("[dbo].[SearchGuarantor]", Request with { Value = Request.Value.EscapedLike() });
    }

    /// <summary>
    ///     Comprueba si existe un Guarantor por su identificador de forma asíncrona.
    /// </summary>
    /// <param name="Store">Repositorio de <see cref="Guarantor"/>.</param>
    /// <param name="GuarantorId">Recibe el id del Guarantor a buscar</param>
    /// <returns>Retorna un booleano</returns>
    [MethodId("D094D436-1107-4455-9D8D-EA82683A319F")]
    public static async ValueTask<bool> ExistsGuarantorAsync(this IStore<Guarantor> Store, long GuarantorId)
    {
        var Guarantor = await Store.FetchGuarantorByIdAsync(GuarantorId);
        return Guarantor is not null;
    }

    /// <summary>
    ///     Obtiene información del Guarantor por su identificador de forma asíncrona.
    /// </summary>
    /// <param name="Store">
    ///     Repositorio de <see cref="Guarantor"/>.
    /// </param>
    /// <param name="GuarantorId">El identificador del garante.</param>
    /// <returns>Información del Guarantor o nulo si no se encuentra.</returns>
    [MethodId("867C7D0F-6191-41B5-B154-C9400FE14395")]
    public static async ValueTask<GuarantorInfo?> FetchGuarantorByIdAsync(this IStore<Guarantor> Store, long? GuarantorId)
    {
        return await Store.ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<GuarantorInfo?>("[dbo].[FetchGuarantorById]", new { GuarantorId });
    }

    /// <summary>
    ///     Obtiene información del garante por su número de identificación de forma asíncrona.
    /// </summary>
    /// <param name="Store">
    ///     Repositorio de <see cref="Guarantor"/>.
    /// </param>
    /// <param name="Identification">El número de identificación del garante.</param>
    /// <returns>Información del garante o nulo si no se encuentra.</returns>
    [MethodId("564BA87E-6767-4EA7-86F6-924EDDE109DE")]
    public static async ValueTask<FetchGuarantor?> FetchGuarantorByIdentificationAsync(this IStore<Guarantor> Store, string? Identification)
    {
        return await Store.ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<FetchGuarantor?>("[dbo].[FetchGuarantorByIdentification]", new { Identification });
    }

    /// <summary>
    ///     Obtiene información del Guarantor por su correo electrónico de forma asíncrona.
    /// </summary>
    /// <param name="Store">
    ///     Repositorio de <see cref="Guarantor"/>.
    /// </param>
    /// <param name="Email">El correo electrónico del garante.</param>
    /// <returns>Información del Guarantor o nulo si no se encuentra.</returns>
    [MethodId("B147024A-54AF-4B79-91D5-9D7E8C7336E6")]
    public static async ValueTask<FetchGuarantor?> FetchGuarantorByEmailAsync(this IStore<Guarantor> Store, string? Email)
    {
        return await Store.ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<FetchGuarantor?>("[dbo].[FetchGuarantorByEmail]", new { Email });
    }

    /// <summary>
    ///     Obtiene información del Guarantor por su número de teléfono de forma asíncrona.
    /// </summary>
    /// <param name="Store">
    ///     Repositorio de <see cref="Guarantor"/>.
    /// </param>
    /// <param name="Phone">El número de teléfono del Guarantor</param>
    /// <returns></returns>
    [MethodId("23168FC2-1967-4FC3-AD88-0A6FC2B29827")]
    public static async ValueTask<FetchGuarantor?> FetchGuarantorByPhoneAsync(this IStore<Guarantor> Store, string? Phone)
    {
        return await Store.ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<FetchGuarantor?>("[dbo].[FetchGuarantorByPhone]", new { Phone });
    }

    /// <summary>
    ///     Obtiene una secuencia de Guarantor de forma asíncrona.
    /// </summary>
    /// <param name="Store">
    ///     Repositorio de <see cref="Guarantor"/>
    /// </param>
    /// <returns>Una lista de Guarantors</returns>
    [MethodId("190F125C-971A-4C67-A4FD-0C6187246707")]
    public static IAsyncEnumerable<FetchGuarantor> FetchGuarantorsAsync(this IStore<Guarantor> Store)
    {
        return Store.ExecuteStoredProcedureQueryAsync<FetchGuarantor>("[dbo].[FetchGuarantors]");
    }

    /// <summary>
    ///     Obtiene una secuencia paginada de Guarantor de forma asíncrona.
    /// </summary>
    /// <param name="Store">
    ///     Repositorio de <see cref="Guarantor"/>
    /// </param>
    /// <param name="Request">La solicitud de paginación.</param>
    /// <returns>Una lista paginada de Guarantors.</returns>
    [MethodId("FEC37F36-AA92-4ECE-A302-466DF3122A4A")]
    public static IAsyncEnumerable<FetchGuarantor> FetchGuarantorsAsync(this IStore<Guarantor> Store, PaginationRequest Request)
    {
        return Store.ExecuteStoredProcedureQueryAsync<FetchGuarantor>("[dbo].[FetchGuarantorsTop]", Request);
    }

    /// <summary>
    ///     Metodo para crear un Guarantor
    /// </summary>
    /// <param name="Store">
    ///     Repositorio del modelo <see cref="Guarantor"/>
    /// </param>
    /// <param name="Request">
    ///     Recibe los datos necesarios para crear un Guarantor 
    /// </param>
    /// <returns>Retorna el id del Guarantor</returns>
    [MethodId("BAEC4217-08E5-4714-BD80-D2C37696BB45")]
    public static async ValueTask<EntityId> InsertGuarantorAsync(this IStore<Guarantor> Store, CreateGuarantorRequest Request)
    {
        DynamicParameters Parameters = new DynamicParameters(Request);
        Parameters.Add(nameof(Guarantor.GuarantorId), default, DbType.Int64, ParameterDirection.Output);

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            await Store.ExecuteStoredProcedureAsync("[dbo].[InsertGuarantor]", Parameters, SqlTransaction);
            SqlTransaction.Commit();

            return Parameters.Get<long?>(nameof(Guarantor.GuarantorId));
        }
        catch
        {
            SqlTransaction.Rollback();
            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Store"></param>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("B9BAEE11-E36E-4BE4-844A-646A72AE9BC2")]
    public static async ValueTask<bool> DeleteGuarantorAsync(this IStore<Guarantor> Store, DeleteGuarantorRequest Request)
    {
        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            int Result = await Store.ExecuteStoredProcedureAsync("[dbo].[DeleteGuarantor]", Request, SqlTransaction);
            SqlTransaction.Commit();

            return Result > 0;
        }
        catch
        {
            SqlTransaction.Rollback();
            throw;
        }
    }
}
