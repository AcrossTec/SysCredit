namespace SysCredit.Mobile.Services.Https;

using SysCredit.Mobile.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISysCreditApiService
{
    ValueTask<Guarantor?> InsertGuarantorAsync(Guarantor Model);

    IAsyncEnumerable<Guarantor> SearchGuarantorsAsync(string? Query = null, int? Offset = null, int? Limit = null);
}
