namespace SysCredit.Api.Stores;

using Dapper;

using SysCredit.Api.Attributes;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.ViewModels;
using SysCredit.Api.ViewModels.Customers;
using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Data;
using System.Reflection;

using static Constants.ErrorCodePrefix;
using static Constants.ErrorCodes;

[Store]
[ErrorCategory(nameof(CustomerStore))]
[ErrorCodePrefix(CustomerStorePrefix)]
public static class CustomerStore
{
    [MethodId("49740285-1AB2-47EC-98C0-B20E9D457DCE")]
    public static IAsyncEnumerable<SearchCustomer> SearchCustomerAsync(this IStore<Customer> Store, SearchRequest Request)
    {
        return Store.ExecQueryAsync<SearchCustomer>("[dbo].[SearchCustomer]", Request with { Value = Request.Value.EscapedLike() });
    }

    [MethodId("D767D480-09E4-4B08-BD31-11D24A599FAF")]
    public static async ValueTask<CustomerInfo?> FetchCustomerByIdAsync(this IStore<Customer> Store, long? CustomerId)
    {
        return await Store.ExecQuery<FetchCustomer>("[dbo].[FetchCustomerById]", new { CustomerId }).ConvertFetchCustomerToCustomerInfoAsync().SingleOrDefaultAsync();
    }

    [MethodId("39B222E4-EA19-4C38-9AD3-1E55843ADEDC")]
    public static async ValueTask<CustomerInfo?> FetchCustomerByIdentificationAsync(this IStore<Customer> Store, string? Identification)
    {
        return await Store.ExecQuery<FetchCustomer>("[dbo].[FetchCustomerByIdentification]", new { Identification }).ConvertFetchCustomerToCustomerInfoAsync().SingleOrDefaultAsync();
    }

    [MethodId("C70ABA49-4546-481C-98F3-5C8C54D5225A")]
    public static async ValueTask<CustomerInfo?> FetchCustomerByEmailAsync(this IStore<Customer> Store, string? Email)
    {
        return await Store.ExecQuery<FetchCustomer>("[dbo].[FetchCustomerByEmail]", new { Email }).ConvertFetchCustomerToCustomerInfoAsync().SingleOrDefaultAsync();
    }

    [MethodId("7FC0C0C0-58AA-4724-97B9-FA96288688B6")]
    public static async ValueTask<CustomerInfo?> FetchCustomerByPhoneAsync(this IStore<Customer> Store, string? Phone)
    {
        return await Store.ExecQuery<FetchCustomer>("[dbo].[FetchCustomerByPhone]", new { Phone }).ConvertFetchCustomerToCustomerInfoAsync().SingleOrDefaultAsync();
    }

    [MethodId("44AFFF21-7AB3-44D7-9E15-4A07D4352B63")]
    public static IAsyncEnumerable<CustomerInfo> FetchCustomersAsync(this IStore<Customer> Store)
    {
        return Store.ExecQuery<FetchCustomer>("[dbo].[FetchCustomers]").ConvertFetchCustomerToCustomerInfoAsync();
    }

    /// <summary>
    ///     How To Pass Array Or List To Stored Procedure
    ///     https://www.mytecbits.com/microsoft/sql-server/pass-array-or-list-to-stored-procedure
    /// </summary>
    /// <param name="Store"></param>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("5B53C4A1-4033-4778-A1A7-CB8144B52065")]
    public static async ValueTask<EntityId> InsertCustomerAsync(this IStore<Customer> Store, CreateCustomerRequest Request)
    {
        DynamicParameters Parameters = Request.ToDynamicParameters();
        Parameters.Add(nameof(Customer.CustomerId), default, DbType.Int64, ParameterDirection.Output);

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            await Store.ExecAsync("[dbo].[InsertCustomer]", Parameters, SqlTransaction);
            SqlTransaction.Commit();

            return Parameters.Get<long?>(nameof(Customer.CustomerId));
        }
        catch (Exception SqlEx)
        {
            // Handle the exception if the transaction fails to commit.
            SysCreditException SysCreditEx = SqlEx.ToSysCreditException(MethodInfo.GetCurrentMethod(), DATAC0001);

            try
            {
                // Attempt to roll back the transaction.
                SqlTransaction.Rollback();
            }
            catch (Exception Ex)
            {
                // Throws an InvalidOperationException if the connection is closed or the transaction has already been rolled back on the server.
                throw Ex.ToSysCreditException(MethodInfo.GetCurrentMethod(), DATAC0002);
            }

            throw SysCreditEx;
        }
    }

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
