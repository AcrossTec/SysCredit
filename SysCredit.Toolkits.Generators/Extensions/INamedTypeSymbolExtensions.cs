namespace SysCredit.Toolkits.Generators.Extensions;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>
///     Extension methods for the <see cref="INamedTypeSymbol"/> type.
/// </summary>
public static class INamedTypeSymbolExtensions
{
    /// <summary>
    ///     Gets all member symbols from a given <see cref="INamedTypeSymbol"/> instance, including inherited ones.
    /// </summary>
    /// <param name="Symbol">
    ///     The input <see cref="INamedTypeSymbol"/> instance.
    /// </param>
    /// <returns>
    ///     A sequence of all member symbols for <paramref name="Symbol"/>.
    /// </returns>
    public static IEnumerable<ISymbol> GetAllMembers(this INamedTypeSymbol Symbol)
    {
        for (INamedTypeSymbol? CurrentSymbol = Symbol; CurrentSymbol is { SpecialType: not SpecialType.System_Object }; CurrentSymbol = CurrentSymbol.BaseType)
        {
            foreach (ISymbol MemberSymbol in CurrentSymbol.GetMembers())
            {
                yield return MemberSymbol;
            }
        }
    }

    /// <summary>
    ///     Gets all member symbols from a given <see cref="INamedTypeSymbol"/> instance, including inherited ones.
    /// </summary>
    /// <param name="Symbol">
    ///     The input <see cref="INamedTypeSymbol"/> instance.
    /// </param>
    /// <param name="Name">
    ///     The name of the members to look for.</param>
    /// <returns>
    ///     A sequence of all member symbols for <paramref name="Symbol"/>.
    /// </returns>
    public static IEnumerable<ISymbol> GetAllMembers(this INamedTypeSymbol Symbol, string Name)
    {
        for (INamedTypeSymbol? CurrentSymbol = Symbol; CurrentSymbol is { SpecialType: not SpecialType.System_Object }; CurrentSymbol = CurrentSymbol.BaseType)
        {
            foreach (ISymbol MemberSymbol in CurrentSymbol.GetMembers(Name))
            {
                yield return MemberSymbol;
            }
        }
    }
}
