namespace SysCredit.Toolkits.Generators.Extensions;

using Microsoft.CodeAnalysis;

/// <summary>
///     Extension methods for the <see cref="SyntaxNode"/> type.
/// </summary>
public static class SyntaxNodeExtensions
{
    /// <summary>
    ///     Checks whether a given <see cref="SyntaxNode"/> represents the first (partial) declaration of a given symbol.
    /// </summary>
    /// <param name="SyntaxNode">
    ///     The input <see cref="SyntaxNode"/> instance.
    /// </param>
    /// <param name="Symbol">
    ///     The target <see cref="ISymbol"/> instance to check the syntax declaration for.
    /// </param>
    /// <returns>
    ///     Whether <paramref name="SyntaxNode"/> is the first (partial) declaration for <paramref name="Symbol"/>.
    /// </returns>
    /// <remarks>
    ///     This extension can be used to avoid accidentally generating repeated members for types that have multiple partial declarations.
    ///     In order to keep this check efficient and without the need to collect all items and build some sort of hashset from them to
    ///     remove duplicates, each syntax node is symply compared against the available declaring syntax references for the target symbol.
    ///     If the syntax node matches the first syntax reference for the symbol, it is kept, otherwise it is considered a duplicate.
    /// </remarks>
    public static bool IsFirstSyntaxDeclarationForSymbol(this SyntaxNode SyntaxNode, ISymbol Symbol)
    {
        return Symbol.DeclaringSyntaxReferences is [SyntaxReference SyntaxReference, ..] &&
               SyntaxReference.SyntaxTree == SyntaxNode.SyntaxTree &&
               SyntaxReference.Span == SyntaxNode.Span;
    }
}
