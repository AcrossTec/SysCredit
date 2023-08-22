namespace SysCredit.Mobile.Services.Https;

using DynamicData.Binding;

using SysCredit.Helpers;
using SysCredit.Mobile.Models;
using SysCredit.Mobile.Models.Customers.Creates;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISysCreditApiService
{
    ValueTask<IResponse<IObservableCollection<Relationship>>> FetchRelationshipsAsync();

    ValueTask<EntityId?> InsertGuarantorAsync(CreateGuarantor Model);

    IAsyncEnumerable<Guarantor> SearchGuarantorsAsync(string? Query = null, int? Offset = null, int? Limit = null);
}
