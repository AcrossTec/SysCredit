namespace SysCredit.Toolkits.Generators.Extensions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>
///     Extension methods for the <see cref="MethodDeclarationSyntax"/> type.
/// </summary>
public static class MethodDeclarationSyntaxExtensions
{
    /// <summary>
    ///     Checks whether a given <see cref="MethodDeclarationSyntax"/> has or could potentially have any attribute lists.
    /// </summary>
    /// <param name="MethodDeclaration">
    ///     The input <see cref="MethodDeclarationSyntax"/> to check.
    /// </param>
    /// <returns>
    ///     Whether <paramref name="MethodDeclaration"/> has or potentially has any attribute lists.
    /// </returns>
    public static bool HasOrPotentiallyHasAttributeLists(this MethodDeclarationSyntax MethodDeclaration)
    {
        // If the declaration has any attribute lists, there's nothing left to do
        if (MethodDeclaration.AttributeLists.Count > 0)
        {
            return true;
        }

        // If there are no attributes, check whether the method declaration has the partial keyword. If it
        // does, there could potentially be attribute lists on the other partial definition/implementation.
        return MethodDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword);
    }
}
