using SysCredit.Api.DataTransferObject;
using SysCredit.Api.ViewModels.Guarantor;

namespace SysCredit.Api.Helpers.Extensions;

public static class GuarantorDataTransferObjectExtensions
{
    public static IAsyncEnumerable<GuarantorDataTransferObject> AsListDto(
        this IAsyncEnumerable<GuarantorOption> Guarantors)
    {
        return Guarantors.Select(item => item.AsDto());
    }

    public static GuarantorDataTransferObject AsDto(this GuarantorOption Guarantor)
    => new()
    {
        GuarantorId = Guarantor.GuarantorId,
        Identification = Guarantor.Identification,
        Name = Guarantor.Name,
        LastName = Guarantor.LastName,
        Address = Guarantor.Address,
        Neighborhood = Guarantor.Neighborhood,
        BussinessType = Guarantor.BussinessType,
        BussinessAddress = Guarantor.BussinessAddress,
        Phone = Guarantor.Phone,
        Relationship = Guarantor.Relationship
    };
}
