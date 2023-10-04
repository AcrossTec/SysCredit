namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Threading.Tasks;

using static Constants.ErrorCodePrefix;

[Service<ILoanService>]
[ErrorCategory(nameof(LoanService))]
[ErrorCodePrefix(LoanServicePrefix)]
public class LoanService(IStore<Loan> LoanStore, ILogger<LoanService> Logger) : ILoanService
{
    /// <summary>
    ///     Obtiene información de un préstamo por su identificador de préstamo de forma asíncrona.
    /// </summary>
    /// <param name="LoanId">El identificador del préstamo.</param>
    /// <returns>La información del préstamo o nulo si no se encuentra.</returns>
    public ValueTask<IServiceResult<LoanInfo?>> FetchLoanByLoanIdAsync(long? LoanId)
    {
        return LoanStore.FetchLoanByIdAsync(LoanId).CreateServiceResultAsync();
    }
}
