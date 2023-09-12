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
    [MethodId("2EF5FEB6-201C-4FF5-A70C-8D338B7241BD")]
    public static IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync(this IStore<PaymentFrequency> Store)
    {
        return Store.ExecQueryAsync<PaymentFrequencyInfo>("[dbo].[FetchPaymentFrequency]");
    }
}
