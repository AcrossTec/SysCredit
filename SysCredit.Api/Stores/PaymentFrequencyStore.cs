namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.PaymentFrequencies;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Data;
using System.Reflection;

using static Constants.ErrorCodePrefix;
using static Constants.ErrorCodes;

/// <summary>
///     Tienda de datos para la tabla PaymentFrequency representado por el tipo <see cref="PaymentFrequency"/>
/// </summary>
[Store]
[ErrorCategory(nameof(PaymentFrequencyStore))]
[ErrorCodePrefix(PaymentFrequencyStorePrefix)]
public static partial class PaymentFrequencyStore
{
    /// <summary>
    ///     Regresa todas las frecuencias de pago de la base de datos
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos
    /// </param>
    /// <returns>
    ///     Regresa todas las frecuencias de pago
    /// </returns>
    [MethodId("2EF5FEB6-201C-4FF5-A70C-8D338B7241BD")]
    public static IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync(this IStore<PaymentFrequency> Store)
    {
        return Store.ExecuteStoredProcedureQueryAsync<PaymentFrequencyInfo>("[dbo].[FetchPaymentFrequency]");
    }

    /// <summary>
    ///     Regresa un registro de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos
    /// </param>
    /// <param name="PaymentFrequencyId">
    ///     Id obtenido de la ruta
    /// </param>
    /// <returns>
    ///     Regresa un registro de la tabla <see cref="Models.PaymentFrequency"/>
    /// </returns>
    [MethodId("1A320F97-0E0C-4833-87B8-C35D546A8C4B")]
    public static async ValueTask<PaymentFrequencyInfo?> FetchPaymentFrequencyByIdAsync(this IStore<PaymentFrequency> Store, long? PaymentFrequencyId)
    {
        return await Store.ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<PaymentFrequencyInfo?>("[dbo].[FetchPaymentFrequencyById]", new { PaymentFrequencyId });
    }

    /// <summary>
    ///     Realiza la modificacion del <see cref="Models.PaymentFrequency"/> en la base de datos
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos
    /// </param>
    /// <param name="Request">
    ///     Datos que se van a actualizar del <see cref="Models.PaymentFrequency"/>
    /// </param>
    /// <returns>
    ///     Regresa <see langword="true"/> si se actualizo <paramref name="Request"/>, en caso contrario <see langword="false"/>.
    /// </returns>
    [MethodId("4CEA02D0-B5BF-4922-AA24-342F32386095")]
    public static async ValueTask<bool> UpdatePaymentFrequencyAsync(this IStore<PaymentFrequency> Store, UpdatePaymentFrequencyRequest Request)
    {
        using IDbTransaction SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            int Result = await Store.ExecuteStoredProcedureAsync("[dbo].[UpdatePaymentFrequency]", Request, SqlTransaction);
            SqlTransaction.Commit();

            return Result > 0;

        }
        catch (Exception SqlEx)
        {
            SysCreditException SysCreditEx = SqlEx.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATAPF0005*/);

            try
            {
                SqlTransaction.Rollback();
            }
            catch (Exception Ex)
            {

                throw Ex.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATAPF0006*/);
            }

            throw SysCreditEx;
        }
    }

    /// <summary>
    ///     Realiza la eliminacion del <see cref="Models.PaymentFrequency"/> de la base de datos
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos
    /// </param>
    /// <param name="Request">
    ///     Id del <see cref="Models.PaymentFrequency"/> a eliminar
    /// </param>
    /// <returns>
    ///     Mayor que cero si se realizo la modificacion, en caso contrario, no se realizo
    /// </returns>
    [MethodId("80E55584-24BE-47B4-BB38-C79FD7116BC7")]
    public static async ValueTask<bool> DeletePaymentFrequencyAsync(this IStore<PaymentFrequency> Store, DeletePaymentFrequencyRequest Request)
    {
        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            int Result = await Store.ExecuteStoredProcedureAsync("[dbo].[DeletePaymentFrequency]", Request, SqlTransaction);
            SqlTransaction.Commit();

            return Result > 0;
        }
        catch (Exception SqlEx)
        {
            SysCreditException SysCreditEx = SqlEx.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATAPF0003*/);

            try
            {
                SqlTransaction.Rollback();
            }
            catch (Exception Ex)
            {
                throw Ex.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATAPF0004*/);
            }

            throw SysCreditEx;
        }
    }


    [MethodId("016C0C42-BEB3-4821-A1B1-11E91C03BB27")]
    public static async ValueTask<PaymentFrequencyInfo?> FetchPaymentFrequencyByNameAsync(this IStore<PaymentFrequency> Store, string? Name)
    {
        return await Store.ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<PaymentFrequencyInfo?>("[dbo].[FetchPaymentFrequencyByName]", new { Name });
    }

    /// <summary>
    ///      Regresa todos los <see cref="Models.PaymentFrequency"/> completos de la base de datos
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos
    /// </param>
    /// <returns>
    ///     Regresa todos los <see cref="Models.PaymentFrequency"/> completos
    /// </returns>
    [MethodId("2944DF4F-F5C7-41AC-B041-6BDF4CB7C443")]
    public static IAsyncEnumerable<PaymentFrequency> FetchPaymentFrequencyCompleteAsync(this IStore<PaymentFrequency> Store)
    {
        return Store.ExecuteStoredProcedureQueryAsync<PaymentFrequency>("[dbo].[FetchPaymentFrequency]");
    }

    /// <summary>
    ///     Invoca el store para crear una nueva frecuencia de pago en la base de datos
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos
    /// </param>
    /// <param name="Request">
    ///     Datos usado para crear la frecuencia de pago
    /// </param>
    /// <returns>
    ///     Regresa el nuevo Id de la frecuencia de pago creado
    /// </returns>
    [MethodId("80E4ED8C-FBA5-4BC8-B62B-6A5EC3A1355F")]
    public static async ValueTask<EntityId> InsertPaymentFrequencyAsync(this IStore<PaymentFrequency> Store, CreatePaymentFrequencyRequest Request)
    {
        DynamicParameters Parameters = Request.ToDynamicParameters();
        Parameters.Add(nameof(PaymentFrequency.PaymentFrequencyId), default, DbType.Int64, ParameterDirection.Output);

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            // Handle the exception if the transaction fails to commit.
            await Store.ExecuteStoredProcedureAsync("[dbo].[InsertPaymentFrequency]", Parameters, SqlTransaction);
            SqlTransaction.Commit();

            return Parameters.Get<long?>(nameof(PaymentFrequency.PaymentFrequencyId));
        }
        catch (Exception SqlEx)
        {
            SysCreditException SysCreditEx = SqlEx.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATALT0001*/);

            try
            {
                // Attempt to roll back the transaction.
                SqlTransaction.Rollback();
            }
            catch (Exception Ex)
            {
                // Throws an InvalidOperationException if the connection is closed or the transaction has already been rolled back on the server.
                throw Ex.ToSysCreditException(MethodInfo.GetCurrentMethod(), ""/*DATALT0002*/);
            }

            throw SysCreditEx;
        }
    }
}