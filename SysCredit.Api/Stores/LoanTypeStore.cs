namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.Attributes;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.LoanType;
using SysCredit.Api.Requests.LoanTypes;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Data;
using System.Reflection;

using static Constants.ErrorCodePrefix;
using static Constants.ErrorCodes;

/// <summary>
///     Repositorio del modelo <see cref="LoanType"/>
/// </summary>
[Store]
[ErrorCategory(nameof(LoanTypeStore))]
[ErrorCodePrefix(LoanTypeStorePrefix)]
public static partial class LoanTypeStore
{
    /// <summary>
    ///     Metodo para crear un tipo de prestamo
    /// </summary>
    /// <param name="Store">Repositorio del modelo LoanType</param>
    /// <param name="Request">Recibe el nombre del tipo de prestamo</param>
    /// <returns>Retorna el id del tipo de prestamo</returns>
    [MethodId("84370664-26c5-45ed-9590-6df60a9efac4")]
    public static async ValueTask<EntityId> InsertLoanTypeAsync(this IStore<LoanType> Store, CreateLoanTypeRequest Request)
    {
        DynamicParameters Parameters = Request.ToDynamicParameters();
        Parameters.Add(nameof(LoanType.LoanTypeId), default, DbType.Int64, ParameterDirection.Output);

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            await Store.ExecuteStoredProcedureAsync("[dbo].[InsertLoanType]", Parameters, SqlTransaction);
            SqlTransaction.Commit();

            return Parameters.Get<long?>(nameof(LoanType.LoanTypeId));
        }
        catch (Exception SqlEx)
        {
            SysCreditException SysCreditEx = SqlEx.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATALT0001*/);

            try
            {
                SqlTransaction.Rollback();
            }
            catch (Exception Ex)
            {
                throw Ex.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATALT0002*/);
            }

            throw SysCreditEx;
        }
    }

    /// <summary>
    ///     Elimina un tipo de prestamo
    /// </summary>
    /// <param name="Store">
    ///     Repositorio del LoanType
    /// </param>
    /// <param name="Request">
    ///     Id del <see cref="Models.LoanType"/> a eliminar
    /// </param>
    /// <returns></returns>
    [MethodId("FC5C3FA3-E112-49D7-B5EE-37181239695C")]
    public static async ValueTask<bool> DeleteLoanTypeAsync(this IStore<LoanType> Store, DeleteLoanTypeRequest Request)
    {
        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            int Result = await Store.ExecuteStoredProcedureAsync("[dbo].[DeleteLoanType]", Request, SqlTransaction);
            SqlTransaction.Commit();

            return Result > 0;
        }
        catch (Exception SqlEx)
        {
            SysCreditException SysCreditEx = SqlEx.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATALT0003*/);

            try
            {
                SqlTransaction.Rollback();
            }
            catch (Exception Ex)
            {
                throw Ex.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATALT0004*/);
            }

            throw SysCreditEx;
        }
    }

    /// <summary>
    ///     Metodo de actualizacion de LoanType
    /// </summary>
    /// <param name="Store">Repositorio del LoanType</param>
    /// <param name="Request">Objeto que contiene los parametros necesarios para actualizar</param>
    /// <returns>Retorna un numero mayor a 0 indicando que se ha actualizado el tipo de prestamo</returns>
    [MethodId("C367398E-F4F3-4350-86A5-AE2B3DBEBED7")]
    public static async ValueTask<bool> UpdateLoanTypeAsync(this IStore<LoanType> Store, UpdateLoanTypeRequest Request)
    {
        // Se hace una transacción en caso de error
        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            int Result = await Store.ExecuteStoredProcedureAsync("[dbo].[UpdateLoanType]", Request, SqlTransaction);
            SqlTransaction.Commit();

            return Result > 0;
        }
        catch (Exception SqlEx)
        {
            SysCreditException SysCreditEx = SqlEx.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATALT0005*/);

            try
            {
                SqlTransaction.Rollback();
            }
            catch (Exception Ex)
            {
                throw Ex.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATALT0006*/);
            }

            throw SysCreditEx;
        }
    }

    /// <summary>
    ///     Obtiene los tipos de prestamo usando un procedimiento almacenado
    /// </summary>
    /// <param name="Store">Repositorio del modelo LoanType</param>
    /// <returns>Retorna una lista de tipos de prestamo</returns>
    [MethodId("9D9648AF-EE89-4B08-9B6E-96016C086D3F")]
    public static IAsyncEnumerable<LoanTypeInfo> FetchLoanTypeAsync(this IStore<LoanType> Store)
    {
        return Store.ExecuteStoredProcedureQueryAsync<LoanTypeInfo>("[dbo].[FetchLoanType]");
    }

    /// <summary>
    ///     Metodo para verificar que ningun prestamo usa el tipo de prestamo
    /// </summary>
    /// <param name="Store">Repositorio del modelo LoanType</param>
    /// <param name="LoanTypeId">Id del tipo de prestamo</param>
    /// <returns>Retorna un booleano indicando el estado de la validación</returns>
    [MethodId("313F2142-7FF6-493D-89B8-EDB56579C70C")]
    public static async ValueTask<bool> VerifyLoanTypeReference(this IStore<LoanType> Store, long? LoanTypeId)
    {
        return await Store.Connection.ExecuteScalarAsync<bool>("SELECT [dbo].[VerifyLoanTypeReference](@LoanTypeId)", new { LoanTypeId });
    }

    /// <summary>
    ///     Metodo que obtiene el tipo de prestamo por su nombre unico
    /// </summary>
    /// <param name="Store">Repositorio del modelo LoanType</param>
    /// <param name="Name">Nombre del tipo de prestamo</param>
    /// <returns>Retorna el tipo de prestamo</returns>
    [MethodId("54B586C2-74AC-4971-AAD1-854D6CA36AD4")]
    public static async ValueTask<LoanTypeInfo?> FetchLoanTypeByName(this IStore<LoanType> Store, string? Name)
    {
        return await Store.ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<LoanTypeInfo?>("[dbo].[FetchLoanTypeByName]", new { Name });
    }

    /// <summary>
    ///     Obtiene los tipos de prestamo sin procesar la información
    /// </summary>
    /// <param name="Store">Repositorio del modelo LoanType</param>
    /// <returns>Retorna una lista de tipos de prestamo sin procesar</returns>
    [MethodId("16F6B292-7DEB-422D-90AC-7E46B49E296B")]
    public static IAsyncEnumerable<LoanType> FetchLoanTypeCompleteAsync(this IStore<LoanType> Store)
    {
        return Store.ExecuteStoredProcedureQueryAsync<LoanType>("[dbo].[FetchLoanType]");
    }

    /// <summary>
    ///     Obtiene el registo de tipo de prestamo que coincida con el identificador
    /// </summary>
    /// <param name="Store">Repositorio del modelo LoanType</param>
    /// <param name="LoanTypeId">Identificador del tipo de prestamo</param>
    /// <returns>Retorna el registro del tipo de prestamo</returns>
    [MethodId("B7FFCBE6-9430-425D-846C-E0EA17AF48CD")]
    public static async ValueTask<LoanTypeInfo?> FetchLoanTypeByIdAsync(this IStore<LoanType> Store, long? LoanTypeId)
    {
        return await Store.ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<LoanTypeInfo?>("[dbo].[FetchLoanTypeById]", new { LoanTypeId });
    }
}
