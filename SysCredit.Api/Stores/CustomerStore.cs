namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.Attributes;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests;
using SysCredit.Api.Requests.Customers;
using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Data;
using System.Reflection;

using static Constants.ErrorCodePrefix;
using static Constants.ErrorCodes;

/// <summary>
///     Tienda de datos para la table customer representado por el tipo <see cref="Customer" />.
/// </summary>
[Store]
[ErrorCategory(nameof(CustomerStore))]
[ErrorCodePrefix(CustomerStorePrefix)]
public static partial class CustomerStore
{
    /// <summary>
    ///     Obtiene todos los <see cref="Models.Customer"/> de base de datos.
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos.
    /// </param>
    /// <returns>
    ///     Regresa todos los <see cref="Models.Customer"/> de base de datos.
    /// </returns>
    [MethodId("44AFFF21-7AB3-44D7-9E15-4A07D4352B63")]
    public static IAsyncEnumerable<CustomerInfo> FetchCustomerAsync(this IStore<Customer> Store)
    {
        return Store.ExecuteStoredProcedureQuery<FetchCustomer>("[dbo].[FetchCustomers]").ConvertFetchCustomerToCustomerInfoAsync();
    }

    /// <summary>
    ///     Busca un cliente por su Id.
    /// </summary>
    /// <param name="Store">
    ///      Objeto usado como contexto de la base de datos.
    /// </param>
    /// <param name="CustomerId">
    ///     Id del registro del cliente que se buscará.
    /// </param>
    /// <returns>
    ///     Regresa los datos del cliente, sino <see langword="null" /> si este no existe.
    /// </returns>
    [MethodId("D767D480-09E4-4B08-BD31-11D24A599FAF")]
    public static async ValueTask<CustomerInfo?> FetchCustomerByIdAsync(this IStore<Customer> Store, long? CustomerId)
    {
        return await Store.ExecuteStoredProcedureQuery<FetchCustomer>("[dbo].[FetchCustomerById]", new { CustomerId }).ConvertFetchCustomerToCustomerInfoAsync().SingleOrDefaultAsync();
    }

    /// <summary>
    ///     Busca un cliente por su documento de identidad.
    /// </summary>
    /// <param name="Store">
    ///      Objeto usado como contexto de la base de datos.
    /// </param>
    /// <param name="Identification">
    ///     Documento de identidad del registro del cliente que se buscará.
    /// </param>
    /// <returns>
    ///     Regresa los datos del cliente, sino <see langword="null" /> si este no existe.
    /// </returns>
    [MethodId("39B222E4-EA19-4C38-9AD3-1E55843ADEDC")]
    public static async ValueTask<CustomerInfo?> FetchCustomerByIdentificationAsync(this IStore<Customer> Store, string? Identification)
    {
        return await Store.ExecuteStoredProcedureQuery<FetchCustomer>("[dbo].[FetchCustomerByIdentification]", new { Identification }).ConvertFetchCustomerToCustomerInfoAsync().SingleOrDefaultAsync();
    }

    /// <summary>
    ///     Busca un cliente por su correo.
    /// </summary>
    /// <param name="Store">
    ///      Objeto usado como contexto de la base de datos.
    /// </param>
    /// <param name="Email">
    ///     Correo del registro del cliente que se buscará.
    /// </param>
    /// <returns>
    ///     Regresa los datos del cliente, sino <see langword="null" /> si este no existe.
    /// </returns>
    [MethodId("C70ABA49-4546-481C-98F3-5C8C54D5225A")]
    public static async ValueTask<CustomerInfo?> FetchCustomerByEmailAsync(this IStore<Customer> Store, string? Email)
    {
        return await Store.ExecuteStoredProcedureQuery<FetchCustomer>("[dbo].[FetchCustomerByEmail]", new { Email }).ConvertFetchCustomerToCustomerInfoAsync().SingleOrDefaultAsync();
    }

    /// <summary>
    ///     Busca todos los préstamos de un cliente.
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos.
    /// </param>
    /// <param name="Request">
    ///     Se obtiene el Id del cliente.
    /// </param>
    /// <returns>
    ///     Regresa la lista de préstamos del cliente, sino <see langword="null" /> si este no existe.
    /// </returns>
    [MethodId("C152882B-11BA-4035-8B61-E421FB5D547C")]
    public static IAsyncEnumerable<LoanInfo> FetchLoansByCustomerIdAsync(this IStore<Customer> Store, CustomerIdRequest Request)
    {
        return Store.ExecuteStoredProcedureQueryAsync<LoanInfo>("[dbo].[FetchLoansByCustomerId]", Request);
    }

    /// <summary>
    ///     Busca el cliente con respecto al fiador.
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos.
    /// </param>
    /// <param name="GuarantorId">
    ///     Se obtiene el id del fiador.
    /// </param>
    /// <returns>
    ///     Regresa los datos del cliente.
    /// </returns>
    [MethodId("7CC4F348-1634-492E-952E-15F34A20FE49")]
    public static async ValueTask<CustomerInfo?> FetchCustomerByGuarantorIdAsync(this IStore<Customer> Store, long? GuarantorId)
    {
        return await Store.ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<CustomerInfo?>("[dbo].[FetchCustomerByGuarantorId]", new { GuarantorId });
    }


    /// <summary>
    ///     Busca un cliente por su teléfono.
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos.
    /// </param>
    /// <param name="Phone">
    ///     Teléfono del registro de cliente que se buscará.
    /// </param>
    /// <returns>
    ///     Regresa los datos del cliente, sino <see langword="null" /> si este no existe.
    /// </returns>
    [MethodId("7FC0C0C0-58AA-4724-97B9-FA96288688B6")]
    public static async ValueTask<CustomerInfo?> FetchCustomerByPhoneAsync(this IStore<Customer> Store, string? Phone)
    {
        return await Store.ExecuteStoredProcedureQuery<FetchCustomer>("[dbo].[FetchCustomerByPhone]", new { Phone }).ConvertFetchCustomerToCustomerInfoAsync().SingleOrDefaultAsync();
    }

    /// <summary>
    ///     Busca todos los préstamos de un cliente.
    /// </summary>
    /// <param name="Store">
    ///      Objeto usado como contexto de la base de datos.
    /// </param>
    /// <param name="CustomerId">
    ///     Id del registro del cliente que se utilizará para buscar todos sus prestamos.
    /// </param>
    /// <returns>
    ///     Regresa todos los prestamos del cliente.
    /// </returns>
    [MethodId("C152882B-11BA-4035-8B61-E421FB5D547C")]
    public static IAsyncEnumerable<LoanInfo> FetchLoanByCustomerIdAsync(this IStore<Customer> Store, long? CustomerId)
    {
        return Store.ExecuteStoredProcedureQueryAsync<LoanInfo>("[dbo].[FetchLoansByCustomerId]", new { CustomerId });
    }

    /// <summary>
    ///     Busca todas las referencias de un cliente.
    /// </summary>
    /// <param name="Store">
    ///      Objeto usado como contexto de la base de datos.
    /// </param>
    /// <param name="CustomerId">
    ///     Id del registro del cliente que se utilizará para buscar todas sus referencias.
    /// </param>
    /// <returns>
    ///     Regresa todas las referencias del cliente.
    /// </returns>
    [MethodId("4AE3CB7A-B76A-4210-B6B0-C1B8CD5797B7")]
    public static IAsyncEnumerable<ReferenceInfo> FetchReferenceByCustomerIdAsync(this IStore<Customer> Store, long? CustomerId)
    {
        return Store.ExecuteStoredProcedureQueryAsync<ReferenceInfo>("[dbo].[FetchReferencesByCustomerId]", new { CustomerId });
    }

    /// <summary>
    ///     Busca todos los fiadores de un cliente.
    /// </summary>
    /// <param name="Store">
    ///      Objeto usado como contexto de la base de datos.
    /// </param>
    /// <param name="CustomerId">
    ///     Id del registro del cliente que se utilizará para buscar todos sus fiadores.
    /// </param>
    /// <returns>
    ///     Regresa todos los fiadores del cliente.
    /// </returns>
    [MethodId("01942152-9183-4DB7-AD3C-6712BA53023D")]
    public static IAsyncEnumerable<GuarantorInfo> FetchGuarantorByCustomerIdAsync(this IStore<Customer> Store, long? CustomerId)
    {
        return Store.ExecuteStoredProcedureQueryAsync<GuarantorInfo>("[dbo].[FetchGuarantorsByCustomerId]", new { CustomerId });
    }

    /// <summary>
    ///     Obtiene un fiador según el CustomerId y GuarantorId.
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos.
    /// </param>
    /// <param name="Request">
    ///     Criterios de busquedas que serán usados por el procedimiento almacenado.
    /// </param>
    /// <returns>
    ///     Regresa el registro del fiador si este existe, sino <see langword="null" />.
    /// </returns>
    [MethodId("16C3706A-740F-405B-9FB3-8C273513B2FC")]
    public static ValueTask<GuarantorInfo?> FetchGuarantorByCustomerIdAndGuarantorIdAsync(this IStore<Customer> Store, GuarantorAndCustomerIdsRequest Request)
    {
        return Store.ExecuteStoredProcedureQueryAsync<GuarantorInfo?>("[dbo].[FetchGuarantorByCustomerIdAndGuarantorId]", Request).SingleOrDefaultAsync();
    }

    /// <summary>
    ///     <para>
    ///         Regresa todos los clientes en base ha algún criterio de busqueda.
    ///     </para>
    ///     <para>
    ///         Cuando se usa <see cref="SearchRequest.Value" /> los campos de base de datos usados son:
    ///         <list type="table">
    ///             <listheader>
    ///                 <term>Propiedad</term>
    ///                 <description>Descripción</description>
    ///             </listheader>
    ///             <item>
    ///                 <term><see cref="Customer.Identification" /></term>
    ///                 <description>Documento de identidad del cliente.</description>
    ///             </item>
    ///             <item>
    ///                 <term><see cref="Customer.Name" /></term>
    ///                 <description>Nombres del cliente.</description>
    ///             </item>
    ///             <item>
    ///                 <term><see cref="Customer.LastName" /></term>
    ///                 <description>Apellidos del cliente.</description>
    ///             </item>
    ///             <item>
    ///                 <term><see cref="Customer.Phone" /></term>
    ///                 <description>Teléfono del cliente.</description>
    ///             </item>
    ///             <item>
    ///                 <term><see cref="Customer.Email" /></term>
    ///                 <description>Correo del cliente.</description>
    ///             </item>
    ///         </list>
    ///     </para>
    /// </summary>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos.
    /// </param>
    /// <param name="Request">
    ///     Petición de busqueda.
    /// </param>
    /// <seealso cref="SearchRequest" />
    /// <seealso cref="PaginationRequest" />
    /// <seealso href="https://www.sqlshack.com/pagination-in-sql-server/" />
    /// <returns>
    ///     Regresa una lista con todos los clintes encontrados.
    /// </returns>
    [MethodId("49740285-1AB2-47EC-98C0-B20E9D457DCE")]
    public static IAsyncEnumerable<SearchCustomer> SearchCustomerAsync(this IStore<Customer> Store, SearchRequest Request)
    {
        return Store.ExecuteStoredProcedureQueryAsync<SearchCustomer>("[dbo].[SearchCustomer]", Request with { Value = Request.Value.EscapedLike() });
    }

    /// <summary>
    ///     Crea un nuevo cliente para SysCredit.
    /// </summary>
    /// <remarks>
    ///     How To Pass Array Or List To Stored Procedure
    ///     https://www.mytecbits.com/microsoft/sql-server/pass-array-or-list-to-stored-procedure
    /// </remarks>
    /// <param name="Store">
    ///     Objeto usado como contexto de la base de datos.
    /// </param>
    /// <param name="Request">
    ///     Objeto que contiene toda la información del nuevo cliente a crear.
    /// </param>
    /// <returns>
    ///     Regresa el Id del nuevo cliente que fue creado.
    /// </returns>
    [MethodId("5B53C4A1-4033-4778-A1A7-CB8144B52065")]
    public static async ValueTask<EntityId> InsertCustomerAsync(this IStore<Customer> Store, CreateCustomerRequest Request)
    {
        DynamicParameters Parameters = Request.ToDynamicParameters();
        Parameters.Add(nameof(Customer.CustomerId), default, DbType.Int64, ParameterDirection.Output);

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            await Store.ExecuteStoredProcedureAsync("[dbo].[InsertCustomer]", Parameters, SqlTransaction);
            SqlTransaction.Commit();

            return Parameters.Get<long?>(nameof(Customer.CustomerId));
        }
        catch
        {
            SqlTransaction.Rollback();
            throw;
        }
    }

    /// <summary>
    ///     Agrupa todos los <paramref name="FetchCustomers" /> en un array sin duplicados de tipo <see cref="CustomerInfo" />.
    /// </summary>
    /// <param name="FetchCustomers">
    ///     Lista de <see cref="FetchCustomer" /> que se va a procesar.
    /// </param>
    /// <returns>
    ///     Regresa una lista de <see cref="CustomerInfo" />.
    /// </returns>
    private static IAsyncEnumerable<CustomerInfo> ConvertFetchCustomerToCustomerInfoAsync(this IEnumerable<FetchCustomer> FetchCustomers)
    {
        var Query =
            from Customer in FetchCustomers
            group Customer by Customer.CustomerId into Customers
            let Customer = Customers.First()
            select new CustomerInfo
            {
                CustomerId = Customer.CustomerId,
                Identification = Customer.Identification,
                Name = Customer.Name,
                LastName = Customer.LastName,
                Gender = Customer.Gender,
                Email = Customer.Email,
                Address = Customer.Address,
                Neighborhood = Customer.Neighborhood,
                BussinessType = Customer.BussinessType,
                BussinessAddress = Customer.BussinessAddress,
                Phone = Customer.Phone,

                References = from Reference in Customers
                             group Reference by Reference.ReferenceId into References
                             let Reference = References.First()
                             select new ReferenceInfo
                             {
                                 ReferenceId = Reference.ReferenceId,
                                 Identification = Reference.ReferenceIdentification,
                                 Name = Reference.ReferenceName,
                                 LastName = Reference.ReferenceLastName,
                                 Gender = Reference.ReferenceGender,
                                 Phone = Reference.ReferencePhone,
                                 Email = Reference.ReferenceEmail,
                                 Address = Reference.ReferenceAddress
                             },

                Guarantors = from Guarantor in Customers
                             group Guarantor by Guarantor.GuarantorId into Guarantors
                             let Guarantor = Guarantors.First()
                             select new CustomerGuarantorInfo
                             {
                                 Guarantor = new GuarantorInfo
                                 {
                                     GuarantorId = Guarantor.GuarantorId,
                                     Identification = Guarantor.GuarantorIdentification,
                                     Name = Guarantor.GuarantorName,
                                     LastName = Guarantor.GuarantorLastName,
                                     Gender = Guarantor.GuarantorGender,
                                     Email = Guarantor.GuarantorEmail,
                                     Address = Guarantor.GuarantorAddress,
                                     Neighborhood = Guarantor.GuarantorNeighborhood,
                                     BussinessType = Guarantor.GuarantorBussinessType,
                                     BussinessAddress = Guarantor.GuarantorBussinessAddress,
                                     Phone = Guarantor.GuarantorPhone,
                                 },
                                 Relationship = new RelationshipInfo
                                 {
                                     RelationshipId = Guarantor.GuarantorRelationshipId,
                                     Name = Guarantor.GuarantorRelationshipName
                                 }
                             }
            };

        return Query.ToAsyncEnumerable();
    }
}
