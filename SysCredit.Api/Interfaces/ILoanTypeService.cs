namespace SysCredit.Api.Interfaces;

using SysCredit.Models;

public interface ILoanTypeService
{
    IAsyncEnumerable<LoanTypeInfo> FetchLoanTypeAsync();

    IAsyncEnumerable<LoanType> FetchLoanTypeCompleteAsync();
}
