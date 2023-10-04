using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;

namespace SysCredit.Api.Interfaces.Services;

public interface ILoanService
{
    ValueTask<IServiceResult<LoanInfo?>> FetchLoanByLoanIdAsync(long? LoanId);
}
