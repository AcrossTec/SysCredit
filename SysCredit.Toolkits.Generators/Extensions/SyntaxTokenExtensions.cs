namespace SysCredit.Toolkits.Generators.Extensions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

/// <summary>
///     Extension methods for the <see cref="SyntaxToken"/> type.
/// </summary>
public static class SyntaxTokenExtensions
{
    /// <summary>
    ///     Deconstructs a <see cref="SyntaxToken"/> into its <see cref="SyntaxKind"/> value.
    /// </summary>
    /// <param name="SyntaxToken">
    ///     The input <see cref="SyntaxToken"/> value.
    /// </param>
    /// <param name="SyntaxKind">
    ///     The resulting <see cref="SyntaxKind"/> value for <paramref name="SyntaxToken"/>.
    /// </param>
    public static void Deconstruct(this SyntaxToken SyntaxToken, out SyntaxKind SyntaxKind)
    {
        SyntaxKind = SyntaxToken.Kind();
    }
}
