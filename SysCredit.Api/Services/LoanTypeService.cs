namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Collections.Generic;

[Service<ILoanTypeService>]
public class LoanTypeService : ILoanTypeService
{
    private readonly IStore<LoanType> LoanTypeStore;

    public LoanTypeService(IStore<LoanType> LoanTypeStore)
    {
        this.LoanTypeStore = LoanTypeStore;
    }

    [MethodId("F0D2105D-BDEB-4CC9-BD61-3CA6DF382C03")]
    public IAsyncEnumerable<LoanTypeInfo> FetchLoanTypeAsync()
    {
        return LoanTypeStore.FetchLoanTypeAsync();
    }
}
