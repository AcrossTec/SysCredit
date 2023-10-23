namespace SysCredit.Api.Stores;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;
using static Constants.ErrorCodePrefix;

/// <summary>
///     Repositorio del modelo <see cref="Loan"/>.
/// </summary>
[Store]
[ErrorCategory(nameof(LoanStore))]
[ErrorCodePrefix(LoanStorePrefix)]
public static partial class LoanStore
{
    /// <summary>
    ///     Recupera información sobre un préstamo basado en su frecuencia de pago.
    /// </summary>
    /// <param name="Store">
    ///     Repositorio de Loan.
    /// </param>
    /// <param name="PaymentFrequencyId">
    ///     Id de la Frequencia de pago.
    /// </param>
    /// <returns>
    ///     Retorna la Frequencia de pago.
    /// </returns>
    [MethodId("5CE05A0D-F8DE-4BC0-8963-B621C63FF1B9")]
    public static async ValueTask<LoanInfo?> FetchLoanByPaymentFrequencyIdAsync(this IStore<Loan> Store, long? PaymentFrequencyId)
    {
        return await Store.ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<LoanInfo?>("[dbo].[FetchLoanByPaymentFrequencyId]", new { PaymentFrequencyId });
    }
}
