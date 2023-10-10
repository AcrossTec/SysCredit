namespace SysCredit.Toolkits.Generators.Extensions;

using Microsoft.CodeAnalysis;

using System.Diagnostics.CodeAnalysis;

/// <summary>
///     Extension methods for the <see cref="SymbolInfo"/> type.
/// </summary>
public static class SymbolInfoExtensions
{
    /// <summary>
    ///     Tries to get the resolved attribute type symbol from a given <see cref="SymbolInfo"/> value.
    /// </summary>
    /// <param name="SymbolInfo">
    ///     The <see cref="SymbolInfo"/> value to check.
    /// </param>
    /// <param name="TypeSymbol">
    ///     The resulting attribute type symbol, if correctly resolved.
    /// </param>
    /// <returns>
    ///     Whether <paramref name="SymbolInfo"/> is resolved to a symbol.
    /// </returns>
    /// <remarks>
    ///     This can be used to ensure users haven't eg. spelled names incorrecty or missed a using directive. Normally, code would just
    ///     not compile if that was the case, but that doesn't apply for attributes using invalid targets. In that case, Roslyn will ignore
    ///     any errors, meaning the generator has to validate the type symbols are correctly resolved on its own.
    /// </remarks>
    public static bool TryGetAttributeTypeSymbol(this SymbolInfo SymbolInfo, [NotNullWhen(true)] out INamedTypeSymbol? TypeSymbol)
    {
        ISymbol? AttributeSymbol = SymbolInfo.Symbol;

        // If no symbol is selected and there is a single candidate symbol, use that
        if (AttributeSymbol is null && SymbolInfo.CandidateSymbols is [ISymbol CandidateSymbol])
        {
            AttributeSymbol = CandidateSymbol;
        }

        // Extract the symbol from either the current one or the containing type
        if ((AttributeSymbol as INamedTypeSymbol ?? AttributeSymbol?.ContainingType) is not INamedTypeSymbol ResultingSymbol)
        {
            TypeSymbol = null;
            return false;
        }

        TypeSymbol = ResultingSymbol;
        return true;
    }
}
