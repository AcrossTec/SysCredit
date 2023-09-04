namespace SysCredit.Api.Interfaces;

public interface ILoanTypeService
{
    IAsyncEnumerable<LoanTypeInfo> FetchLoanTypeAsync();
}
