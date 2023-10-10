namespace SysCredit.Toolkits.Generators.Extensions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

/// <summary>
///     Extension methods for the <see cref="MemberDeclarationSyntax"/> type.
/// </summary>
public static class MemberDeclarationSyntaxExtensions
{
    /// <summary>
    ///     Replaces a specific modifier.
    /// </summary>
    /// <param name="MemberDeclaration">
    ///     The input <see cref="MemberDeclarationSyntax"/> instance.
    /// </param>
    /// <param name="OldKind">
    ///     The target modifier kind to replace.
    /// </param>
    /// <param name="NewKind">
    ///     The new modifier kind to add or replace.
    /// </param>
    /// <returns>
    ///     A <see cref="MemberDeclarationSyntax"/> instance with the target modifier.
    /// </returns>
    public static MemberDeclarationSyntax ReplaceModifier(this MemberDeclarationSyntax MemberDeclaration, SyntaxKind OldKind, SyntaxKind NewKind)
    {
        int Index = MemberDeclaration.Modifiers.IndexOf(OldKind);

        if (Index != -1)
        {
            return MemberDeclaration.WithModifiers(MemberDeclaration.Modifiers.Replace(MemberDeclaration.Modifiers[Index], Token(NewKind)));
        }

        return MemberDeclaration;
    }

    /// <summary>
    ///     Removes a specific modifier.
    /// </summary>
    /// <param name="MemberDeclaration">
    ///     The input <see cref="MemberDeclarationSyntax"/> instance.
    /// </param>
    /// <param name="Kind">
    ///     The modifier kind to remove.
    /// </param>
    /// <returns>
    ///     A <see cref="MemberDeclarationSyntax"/> instance without the specified modifier.
    /// </returns>
    public static MemberDeclarationSyntax RemoveModifier(this MemberDeclarationSyntax MemberDeclaration, SyntaxKind Kind)
    {
        int Index = MemberDeclaration.Modifiers.IndexOf(Kind);

        if (Index != -1)
        {
            return MemberDeclaration.WithModifiers(MemberDeclaration.Modifiers.RemoveAt(Index));
        }

        return MemberDeclaration;
    }
}
