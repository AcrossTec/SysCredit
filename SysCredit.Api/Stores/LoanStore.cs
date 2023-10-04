using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

namespace SysCredit.Api.Stores;

using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(nameof(LoanTypeStore))]
[ErrorCodePrefix(LoanStorePrefix)]
public static class LoanStore
{
    /// <summary>
    ///     Obtiene información de un préstamo por su identificador de préstamo de forma asíncrona.
    /// </summary>
    /// <param name="Store">La instancia del almacén que contiene los préstamos.</param>
    /// <param name="LoanId">El identificador del préstamo.</param>
    /// <returns>La información del préstamo o nulo si no se encuentra.</returns>
    [MethodId("6B50A40D-4F92-4D51-BF8B-9E43853947A4")]
    public static async ValueTask<LoanInfo?> FetchLoanByIdAsync(this IStore<Loan> Store, long? LoanId)
    { 
        return await Store.ExecFirstOrDefaultAsync<LoanInfo?>("[dbo].[FetchLoanByLoanId]", new { LoanId });
    }
}
