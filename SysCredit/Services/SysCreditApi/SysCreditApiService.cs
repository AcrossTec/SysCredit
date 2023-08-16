namespace SysCredit.Mobile.Services.Https;

using SysCredit.Mobile.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SysCreditApiService : ISysCreditApiService
{
    public ValueTask<Guarantor?> InsertGuarantorAsync(Guarantor Model)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<Guarantor> SearchGuarantorsAsync(string? Query = null, int? Offset = null, int? Limit = null)
    {
        throw new NotImplementedException();
    }
}
