namespace SysCredit.Api.Patchers.Stores;

using HarmonyLib;

using SysCredit.Api.Stores;
using SysCredit.Models;

using System.Reflection;

/// <summary>
///     Annotations
///     https://harmony.pardeike.net/articles/annotations.html
/// </summary>
/// <remarks>
///     Patching: Auxilary patch methods
///     https://harmony.pardeike.net/articles/patching-auxilary.html
///
///     Patching: Common injected values
///     https://harmony.pardeike.net/articles/patching-injections.html
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
    public static bool Prefix(ref IAsyncEnumerable<LoanTypeInfo>? __result, object[] __args, MethodInfo __originalMethod, ref IStore<LoanType> Store)
    {
        // __result = PrefixFetchLoanTypeAsync?.Invoke();
        // return false; // Skips the original method
        return true; // Execute Current Method
    }

    /// <summary>
    ///     Patching: Postfix
    ///     https://harmony.pardeike.net/articles/patching-prefix.html
    /// </summary>
    public static void Postfix(ref IAsyncEnumerable<LoanTypeInfo> __result, ref IStore<LoanType> Store)
    {
        // El primer argumento puede ser el valor de retorno sin el uso de: ref
        // Se puede extender el valor de retorno regresando el mismo valor con las modificaciones aplicadas
    }

    /// <summary>
    ///     Observing exceptions.
    ///
    ///     Patching: Finalizer
    ///     https://harmony.pardeike.net/articles/patching-finalizer.html
    /// </summary>
    /// <param name="__exception"></param>
    public static void Finalizer(Exception? __exception)
    {
    }

    /// <summary>
    ///     Suppressing any exceptions
    /// </summary>
    /// <returns></returns>
    /// public static Exception? Finalizer()
    /// {
    ///     return null;
    /// }

    /// <summary>
    ///     Changing and rethrowing exceptions
    /// </summary>
    /// <param name="__exception"></param>
    /// <returns></returns>
    /// public static Exception Finalizer(Exception? __exception)
    /// {
    ///     return new SysCreditException(new(), __exception);
    /// }
}
