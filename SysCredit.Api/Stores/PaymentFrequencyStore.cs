namespace SysCredit.Api.Stores;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;

using SysCredit.DataTransferObject.Commons;

using SysCredit.Models;

[Store]
[ErrorCategory(ErrorCategories.PaymentFrequencyStore)]
public static class PaymentFrequencyStore
{
    [MethodId("2EF5FEB6-201C-4FF5-A70C-8D338B7241BD")]
    public static IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync(this IStore<PaymentFrequency> Store)
    {
        return Store.ExecQueryAsync<PaymentFrequencyInfo>("[dbo].[FetchPaymentFrequency]");
    }

    [MethodId("1A320F97-0E0C-4833-87B8-C35D546A8C4B")]
    public static async ValueTask<PaymentFrequencyInfo?> FetchPaymentFrequencyByIdAsync(this IStore<PaymentFrequency> Store, long PaymentFrequencyId)
    {
        return await Store.ExecFirstOrDefaultAsync<PaymentFrequencyInfo?>("[dbo].[FetchPaymentFrequencyById]", new { PaymentFrequencyId });
    }
}
