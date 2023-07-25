using SysCredit.Api.DataTransferObject;
using SysCredit.Api.Models;
using SysCredit.Api.ViewModels.Customer;
using SysCredit.Api.ViewModels.Guarantor;

namespace SysCredit.Api.Helpers.Extensions;

public static class CustomerDataTransferObjectExtensions
{
    public static IAsyncEnumerable<CustomerDataTransferObject> AsListDto(this IAsyncEnumerable<CustomerOptions> Items)
    {
        return Items.Select(C => C.AsDto());
    }

    public static CustomerDataTransferObject AsDto(this CustomerOptions Customer)
        => new()
        {
            CustomerId = Customer.CustomerId,
            Identification = Customer.Identification,
            Name = Customer.Name,
            LastName = Customer.LastName,
            Address = Customer.Address,
            Neighborhood = Customer.Neighborhood, 
            BussinessType = Customer.BussinessType,
            BussinessAddress = Customer.BussinessAddress,
            Phone = Customer.Phone,
        };

    public static CustomerGuarantorDataTransferObject AsDto(this ViewModels.Guarantor.CustomerGuarantor CustomerGuarantor)
        => new CustomerGuarantorDataTransferObject { GuarantorId = CustomerGuarantor.GuarantorId };

    public static CustomerReferenceDataTransferObject AsDto(this ViewModels.Reference.CustomerReference CustomerReference)
        => new CustomerReferenceDataTransferObject { ReferenceId = CustomerReference.ReferenceId};
}
