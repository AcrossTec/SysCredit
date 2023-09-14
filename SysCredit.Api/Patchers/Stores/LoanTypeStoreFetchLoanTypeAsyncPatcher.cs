namespace SysCredit.Api.Patchers.Stores;

using HarmonyLib;

using SysCredit.Api.Stores;
using SysCredit.Models;

/// <summary>
///     Annotations
///     https://harmony.pardeike.net/articles/annotations.html
/// </summary>
/// <remarks>
///     Patching: Auxilary patch methods
///     https://harmony.pardeike.net/articles/patching-auxilary.html
/// </remarks>
[HarmonyPatch(typeof(LoanTypeStore))]
[HarmonyPatch(nameof(LoanTypeStore.FetchLoanTypeAsync))]
[HarmonyPatch(new Type[] { typeof(IStore<LoanType>) })]
public class LoanTypeStoreFetchLoanTypeAsyncPatcher
{
    private static Func<IAsyncEnumerable<LoanTypeInfo>>? PrefixFetchLoanTypeAsync;

    internal static void SetFetchLoanTypeAsync(Func<IAsyncEnumerable<LoanTypeInfo>> Func)
    {
        PrefixFetchLoanTypeAsync = Func;
    }

    /// <summary>
    ///     Patching: Prefix
    ///     https://harmony.pardeike.net/articles/patching-prefix.html
    /// </summary>
    public static bool Prefix(ref IAsyncEnumerable<LoanTypeInfo>? __result)
    {
        __result = PrefixFetchLoanTypeAsync?.Invoke();
        return false; // Skips the original method
    }

    /// <summary>
    ///     Patching: Postfix
    ///     https://harmony.pardeike.net/articles/patching-prefix.html
    /// </summary>
    public static void Postfix(ref IAsyncEnumerable<LoanTypeInfo> __result)
    {
    }
}
