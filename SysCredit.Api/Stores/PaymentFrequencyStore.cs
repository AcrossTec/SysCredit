namespace SysCredit.Api.Stores;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(nameof(PaymentFrequencyStore))]
[ErrorCodePrefix(PaymentFrequencyStorePrefix)]
public static class PaymentFrequencyStore
{
    /// <summary>
    /// Este método ejecuta una consulta en una base de datos y 
    /// devuelve los resultados como un flujo de elementos de 
    /// tipo PaymentFrequencyInfo (DTO) a través de IAsyncEnumerable
    /// </summary>
    /// <param name="Store"></param>
    /// <returns></returns>
    [MethodId("2EF5FEB6-201C-4FF5-A70C-8D338B7241BD")]
    public static IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync(this IStore<PaymentFrequency> Store)
    {
        return Store.ExecQueryAsync<PaymentFrequencyInfo>("[dbo].[FetchPaymentFrequency]");
    }

    /// <summary>
    /// Este método ejecuta una consulta en una base de datos y 
    /// devuelve los resultados como un flujo de elementos de 
    /// tipo PaymentFrequency a través de IAsyncEnumerable
    /// </summary>
    /// <param name="Store"></param>
    /// <returns></returns>
    [MethodId("2944DF4F-F5C7-41AC-B041-6BDF4CB7C443")]
    public static IAsyncEnumerable<PaymentFrequency> FetchPaymentFrequencyCompleteAsync(this IStore<PaymentFrequency> Store) 
    {
        return Store.ExecQueryAsync<PaymentFrequency>("[dbo].[FetchPaymentFrequency]");
    }
}
