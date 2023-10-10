namespace SysCredit.Toolkits.Generators.Extensions;

using Microsoft.CodeAnalysis;

using System.Diagnostics.CodeAnalysis;

/// <summary>
///     Extension methods for the <see cref="ISymbol"/> type.
/// </summary>
public static class ISymbolExtensions
{
    /// <summary>
    ///     Gets the fully qualified name for a given symbol.
    /// </summary>
    /// <param name="Symbol">
    ///     The input <see cref="ISymbol"/> instance.
    /// </param>
    /// <returns>
    ///     The fully qualified name for <paramref name="Symbol"/>.
    /// </returns>
    public static string GetFullyQualifiedName(this ISymbol Symbol)
    {
        return Symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    }

    /// <summary>
    ///     Gets the fully qualified name for a given symbol, including nullability annotations.
    /// </summary>
    /// <param name="Symbol">
    ///     The input <see cref="ISymbol"/> instance.
    /// </param>
    /// <returns>
    ///     The fully qualified name for <paramref name="Symbol"/>.
    /// </returns>
    public static string GetFullyQualifiedNameWithNullabilityAnnotations(this ISymbol Symbol)
    {
        return Symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat.AddMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier));
    }

    /// <summary>
    ///     Checks whether or not a given type symbol has a specified full name.
    /// </summary>
    /// <param name="Symbol">
    ///     The input <see cref="ISymbol"/> instance to check.
    /// </param>
    /// <param name="Name">
    ///     The full name to check.
    /// </param>
    /// <returns>
    ///     Whether <paramref name="Symbol"/> has a full name equals to <paramref name="Name"/>.
    /// </returns>
    public static bool HasFullyQualifiedName(this ISymbol Symbol, string Name)
    {
        return Symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) == Name;
    }

    /// <summary>
    ///     Checks whether or not a given symbol has an attribute with the specified fully qualified metadata name.
    /// </summary>
    /// <param name="Symbol">
    ///     The input <see cref="ISymbol"/> instance to check.
    /// </param>
    /// <param name="Name">
    ///     The attribute name to look for.
    /// </param>
    /// <returns>
    ///     Whether or not <paramref name="Symbol"/> has an attribute with the specified name.
    /// </returns>
    public static bool HasAttributeWithFullyQualifiedMetadataName(this ISymbol Symbol, string Name)
    {
        foreach (AttributeData Attribute in Symbol.GetAttributes())
        {
            if (Attribute.AttributeClass?.HasFullyQualifiedMetadataName(Name) == true)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks whether or not a given symbol has an attribute with the specified type.
    /// </summary>
    /// <param name="Symbol">
    ///     The input <see cref="ISymbol"/> instance to check.
    /// </param>
    /// <param name="TypeSymbol">
    ///     The <see cref="ITypeSymbol"/> instance for the attribute type to look for.
    /// </param>
    /// <returns>
    ///     Whether or not <paramref name="Symbol"/> has an attribute with the specified type.
    /// </returns>
    public static bool HasAttributeWithType(this ISymbol Symbol, ITypeSymbol TypeSymbol)
    {
        return TryGetAttributeWithType(Symbol, TypeSymbol, out _);
    }

    /// <summary>
    ///     Tries to get an attribute with the specified type.
    /// </summary>
    /// <param name="Symbol">
    ///     The input <see cref="ISymbol"/> instance to check.
    /// </param>
    /// <param name="TypeSymbol">
    ///     The <see cref="ITypeSymbol"/> instance for the attribute type to look for.
    /// </param>
    /// <param name="AttributeData">
    ///     The resulting attribute, if it was found.
    /// </param>
    /// <returns>
    ///     Whether or not <paramref name="Symbol"/> has an attribute with the specified type.
    /// </returns>
    public static bool TryGetAttributeWithType(this ISymbol Symbol, ITypeSymbol TypeSymbol, [NotNullWhen(true)] out AttributeData? AttributeData)
    {
        foreach (AttributeData Attribute in Symbol.GetAttributes())
        {
            if (SymbolEqualityComparer.Default.Equals(Attribute.AttributeClass, TypeSymbol))
            {
                AttributeData = Attribute;
                return true;
            }
        }

        AttributeData = null;
        return false;
    }

#if !ROSLYN_4_3_1_OR_GREATER
    /// <summary>
    ///     Tries to get an attribute with the specified fully qualified metadata name.
    /// </summary>
    /// <param name="Symbol">
    ///     The input <see cref="ISymbol"/> instance to check.
    /// </param>
    /// <param name="Name">
    ///     The attribute name to look for.
    /// </param>
    /// <param name="AttributeData">
    ///     The resulting attribute, if it was found.
    /// </param>
    /// <returns>
    ///     Whether or not <paramref name="Symbol"/> has an attribute with the specified name.
    /// </returns>
    public static bool TryGetAttributeWithFullyQualifiedMetadataName(this ISymbol Symbol, string Name, [NotNullWhen(true)] out AttributeData? AttributeData)
    {
        foreach (AttributeData Attribute in Symbol.GetAttributes())
        {
            if (Attribute.AttributeClass?.HasFullyQualifiedMetadataName(Name) == true)
            {
                AttributeData = Attribute;
                return true;
            }
        }

        AttributeData = null;
        return false;
    }
#endif

    /// <summary>
    ///     Calculates the effective accessibility for a given symbol.
    /// </summary>
    /// <param name="Symbol">
    ///     The <see cref="ISymbol"/> instance to check.
    /// </param>
    /// <returns>
    ///     The effective accessibility for <paramref name="Symbol"/>.
    /// </returns>
    public static Accessibility GetEffectiveAccessibility(this ISymbol Symbol)
    {
        // Start by assuming it's visible
        Accessibility Visibility = Accessibility.Public;

        // Handle special cases
        switch (Symbol.Kind)
        {
            case SymbolKind.Alias: return Accessibility.Private;
            case SymbolKind.Parameter: return GetEffectiveAccessibility(Symbol.ContainingSymbol);
            case SymbolKind.TypeParameter: return Accessibility.Private;
        }

        // Traverse the symbol hierarchy to determine the effective accessibility
        while (Symbol is not null && Symbol.Kind != SymbolKind.Namespace)
        {
            switch (Symbol.DeclaredAccessibility)
            {
                case Accessibility.NotApplicable:
                case Accessibility.Private:
                    return Accessibility.Private;
                case Accessibility.Internal:
                case Accessibility.ProtectedAndInternal:
                    Visibility = Accessibility.Internal;
                    break;
            }

            Symbol = Symbol.ContainingSymbol;
        }

        return Visibility;
    }

    /// <summary>
    ///     Checks whether or not a given symbol can be accessed from a specified assembly.
    /// </summary>
    /// <param name="Symbol">
    ///     The input <see cref="ISymbol"/> instance to check.
    /// </param>
    /// <param name="Assembly">
    ///     The assembly to check the accessibility of <paramref name="Symbol"/> for.
    /// </param>
    /// <returns>
    ///     Whether <paramref name="Assembly"/> can access <paramref name="Symbol"/>.
    /// </returns>
    public static bool CanBeAccessedFrom(this ISymbol Symbol, IAssemblySymbol Assembly)
    {
        Accessibility Accessibility = Symbol.GetEffectiveAccessibility();
        return Accessibility == Accessibility.Public ||
               Accessibility == Accessibility.Internal && Symbol.ContainingAssembly.GivesAccessTo(Assembly);
    }
}