namespace SysCredit.Mobile.Services.Https;

using DynamicData.Binding;

using SysCredit.Helpers;
using SysCredit.Mobile.Models;
using SysCredit.Mobile.Models.Customers.Creates;

using System.Threading.Tasks;

public interface ISysCreditApiService
{
    ValueTask<IResponse<EntityId?>> InsertCustomerAsync(CreateCustomer Model);

    ValueTask<IResponse<EntityId?>> InsertGuarantorAsync(CreateGuarantor Model);

    ValueTask<IResponse<IObservableCollection<Relationship>>> FetchRelationshipsAsync();

    ValueTask<IResponse<IObservableCollection<Customer>>> SearchCustomersAsync(string? Query = null, int? Offset = null, int? Limit = null);

    ValueTask<IResponse<IObservableCollection<Guarantor>>> SearchGuarantorsAsync(string? Query = null, int? Offset = null, int? Limit = null);
}
