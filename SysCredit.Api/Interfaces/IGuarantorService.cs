namespace SysCredit.Api.Interfaces;

using SysCredit.Api.DataTransferObject;
using SysCredit.Api.Helpers;
using SysCredit.Api.Stores;
using SysCredit.Api.ViewModels.Guarantors;

public interface IGuarantorService
{
    IAsyncEnumerable<GuarantorDataTransferObject> FetchGuarantorsAsync();

    ValueTask<IServiceResult<EntityId?>> InsertGuarantorAsync(CreateGuarantorViewModel ViewModel);
}
