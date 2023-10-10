namespace SysCredit.Toolkits.Generators.Extensions;

using Microsoft.CodeAnalysis;

using SysCredit.Toolkits.Generators.Helpers;

using System;
using System.Linq;

/// <summary>
///     Extension methods for the <see cref="ITypeSymbol"/> type.
/// </summary>
public static class ITypeSymbolExtensions
{
    /// <summary>
    ///     Checks whether or not a given <see cref="ITypeSymbol"/> has or inherits from a specified type.
    /// </summary>
    /// <param name="TypeSymbol">
    ///     The target <see cref="ITypeSymbol"/> instance to check.
    /// </param>
    /// <param name="Name">
    ///     The full name of the type to check for inheritance.
    /// </param>
    /// <returns>
    ///     Whether or not <paramref name="TypeSymbol"/> is or inherits from <paramref name="Name"/>.
    /// </returns>
    public static bool HasOrInheritsFromFullyQualifiedMetadataName(this ITypeSymbol TypeSymbol, string Name)
    {
        for (ITypeSymbol? CurrentType = TypeSymbol; CurrentType is not null; CurrentType = CurrentType.BaseType)
        {
            if (CurrentType.HasFullyQualifiedMetadataName(Name))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks whether or not a given <see cref="ITypeSymbol"/> inherits from a specified type.
    /// </summary>
    /// <param name="TypeSymbol">
    ///     The target <see cref="ITypeSymbol"/> instance to check.
    /// </param>
    /// <param name="Name">
    ///     The full name of the type to check for inheritance.
    /// </param>
    /// <returns>
    ///     Whether or not <paramref name="TypeSymbol"/> inherits from <paramref name="Name"/>.
    /// </returns>
    public static bool InheritsFromFullyQualifiedMetadataName(this ITypeSymbol TypeSymbol, string Name)
    {
        INamedTypeSymbol? BaseType = TypeSymbol.BaseType;

        while (BaseType is not null)
        {
            if (BaseType.HasFullyQualifiedMetadataName(Name))
            {
                return true;
            }

            BaseType = BaseType.BaseType;
        }

        return false;
    }

    /// <summary>
    ///     Checks whether or not a given <see cref="ITypeSymbol"/> inherits from a specified type.
    /// </summary>
    /// <param name="TypeSymbol">
    ///     The target <see cref="ITypeSymbol"/> instance to check.
    /// </param>
    /// <param name="BaseTypeSymbol">
    ///     The <see cref="ITypeSymbol"/> instane to check for inheritance from.
    /// </param>
    /// <returns>
    ///     Whether or not <paramref name="TypeSymbol"/> inherits from <paramref name="BaseTypeSymbol"/>.
    /// </returns>
    public static bool InheritsFromType(this ITypeSymbol TypeSymbol, ITypeSymbol BaseTypeSymbol)
    {
        INamedTypeSymbol? CurrentBaseTypeSymbol = TypeSymbol.BaseType;

        while (CurrentBaseTypeSymbol is not null)
        {
            if (SymbolEqualityComparer.Default.Equals(CurrentBaseTypeSymbol, BaseTypeSymbol))
            {
                return true;
            }

            CurrentBaseTypeSymbol = CurrentBaseTypeSymbol.BaseType;
        }

        return false;
    }

    /// <summary>
    ///     Checks whether or not a given <see cref="ITypeSymbol"/> implements an interface with a specified name.
    /// </summary>
    /// <param name="TypeSymbol">
    ///     The target <see cref="ITypeSymbol"/> instance to check.
    /// </param>
    /// <param name="Name">
    ///     The full name of the type to check for interface implementation.
    /// </param>
    /// <returns>
    ///     Whether or not <paramref name="TypeSymbol"/> has an interface with the specified name.
    /// </returns>
    public static bool HasInterfaceWithFullyQualifiedMetadataName(this ITypeSymbol TypeSymbol, string Name)
    {
        foreach (INamedTypeSymbol InterfaceType in TypeSymbol.AllInterfaces)
        {
            if (InterfaceType.HasFullyQualifiedMetadataName(Name))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks whether or not a given <see cref="ITypeSymbol"/> has or inherits a specified attribute.
    /// </summary>
    /// <param name="TypeSymbol">
    ///     The target <see cref="ITypeSymbol"/> instance to check.
    /// </param>
    /// <param name="Predicate">
    ///     The predicate used to match available attributes.
    /// </param>
    /// <returns>
    ///     Whether or not <paramref name="TypeSymbol"/> has an attribute matching <paramref name="Predicate"/>.
    /// </returns>
    public static bool HasOrInheritsAttribute(this ITypeSymbol TypeSymbol, Func<AttributeData, bool> Predicate)
    {
        for (ITypeSymbol? CurrentType = TypeSymbol; CurrentType is not null; CurrentType = CurrentType.BaseType)
        {
            if (CurrentType.GetAttributes().Any(Predicate))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks whether or not a given <see cref="ITypeSymbol"/> has or inherits a specified attribute.
    /// </summary>
    /// <param name="TypeSymbol">
    ///     The target <see cref="ITypeSymbol"/> instance to check.
    /// </param>
    /// <param name="Name">
    ///     The name of the attribute to look for.</param>
    /// <returns>
    ///     Whether or not <paramref name="TypeSymbol"/> has an attribute with the specified type name.
    /// </returns>
    public static bool HasOrInheritsAttributeWithFullyQualifiedMetadataName(this ITypeSymbol TypeSymbol, string Name)
    {
        for (ITypeSymbol? CurrentType = TypeSymbol; CurrentType is not null; CurrentType = CurrentType.BaseType)
        {
            if (CurrentType.HasAttributeWithFullyQualifiedMetadataName(Name))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks whether or not a given <see cref="ITypeSymbol"/> has or inherits a specified attribute.
    /// </summary>
    /// <param name="TypeSymbol">
    ///     The target <see cref="ITypeSymbol"/> instance to check.
    /// </param>
    /// <param name="BaseTypeSymbol">
    ///     The <see cref="ITypeSymbol"/> instane to check for inheritance from.
    /// </param>
    /// <returns>
    ///     Whether or not <paramref name="TypeSymbol"/> has or inherits an attribute of type <paramref name="BaseTypeSymbol"/>.
    /// </returns>
    public static bool HasOrInheritsAttributeWithType(this ITypeSymbol TypeSymbol, ITypeSymbol BaseTypeSymbol)
    {
        for (ITypeSymbol? CurrentType = TypeSymbol; CurrentType is not null; CurrentType = CurrentType.BaseType)
        {
            if (CurrentType.HasAttributeWithType(BaseTypeSymbol))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks whether or not a given <see cref="ITypeSymbol"/> inherits a specified attribute.
    ///     If the type has no base type, this method will automatically handle that and return <see langword="false"/>.
    /// </summary>
    /// <param name="TypeSymbol">
    ///     The target <see cref="ITypeSymbol"/> instance to check.
    /// </param>
    /// <param name="Name">
    ///     The name of the attribute to look for.
    /// </param>
    /// <returns>
    ///     Whether or not <paramref name="TypeSymbol"/> has an attribute with the specified type name.
    /// </returns>
    public static bool InheritsAttributeWithFullyQualifiedMetadataName(this ITypeSymbol TypeSymbol, string Name)
    {
        if (TypeSymbol.BaseType is INamedTypeSymbol BaseTypeSymbol)
        {
            return HasOrInheritsAttributeWithFullyQualifiedMetadataName(BaseTypeSymbol, Name);
        }

        return false;
    }

    /// <summary>
    ///     Checks whether or not a given type symbol has a specified fully qualified metadata name.
    /// </summary>
    /// <param name="Symbol">
    ///     The input <see cref="ITypeSymbol"/> instance to check.
    /// </param>
    /// <param name="Name">
    ///     The full name to check.
    /// </param>
    /// <returns>
    ///     Whether <paramref name="Symbol"/> has a full name equals to <paramref name="Name"/>.
    /// </returns>
    public static bool HasFullyQualifiedMetadataName(this ITypeSymbol Symbol, string Name)
    {
        using ImmutableArrayBuilder<char> Builder = ImmutableArrayBuilder<char>.Rent();
        Symbol.AppendFullyQualifiedMetadataName(in Builder);
        return Builder.WrittenSpan.SequenceEqual(Name.AsSpan());
    }

    /// <summary>
    ///     Gets the fully qualified metadata name for a given <see cref="ITypeSymbol"/> instance.
    /// </summary>
    /// <param name="Symbol">
    ///     The input <see cref="ITypeSymbol"/> instance.
    /// </param>
    /// <returns>
    ///     The fully qualified metadata name for <paramref name="Symbol"/>.
    /// </returns>
    public static string GetFullyQualifiedMetadataName(this ITypeSymbol Symbol)
    {
        using ImmutableArrayBuilder<char> Builder = ImmutableArrayBuilder<char>.Rent();
        Symbol.AppendFullyQualifiedMetadataName(in Builder);
        return Builder.ToString();
    }

    /// <summary>
    ///     Appends the fully qualified metadata name for a given symbol to a target builder.
    /// </summary>
    /// <param name="Symbol">
    ///     The input <see cref="ITypeSymbol"/> instance.
    /// </param>
    /// <param name="Builder">
    ///     The target <see cref="ImmutableArrayBuilder{T}"/> instance.
    /// </param>
    private static void AppendFullyQualifiedMetadataName(this ITypeSymbol Symbol, in ImmutableArrayBuilder<char> Builder)
    {
        static void BuildFrom(ISymbol? Symbol, in ImmutableArrayBuilder<char> Builder)
        {
            switch (Symbol)
            {
                // Namespaces that are nested also append a leading '.'
                case INamespaceSymbol { ContainingNamespace.IsGlobalNamespace: false }:
                    BuildFrom(Symbol.ContainingNamespace, in Builder);
                    Builder.Add('.');
                    Builder.AddRange(Symbol.MetadataName.AsSpan());
                    break;

                // Other namespaces (ie. the one right before global) skip the leading '.'
                case INamespaceSymbol { IsGlobalNamespace: false }:
                    Builder.AddRange(Symbol.MetadataName.AsSpan());
                    break;

                // Types with no namespace just have their metadata name directly written
                case ITypeSymbol { ContainingSymbol: INamespaceSymbol { IsGlobalNamespace: true } }:
                    Builder.AddRange(Symbol.MetadataName.AsSpan());
                    break;

                // Types with a containing non-global namespace also append a leading '.'
                case ITypeSymbol { ContainingSymbol: INamespaceSymbol namespaceSymbol }:
                    BuildFrom(namespaceSymbol, in Builder);
                    Builder.Add('.');
                    Builder.AddRange(Symbol.MetadataName.AsSpan());
                    break;

                // Nested types append a leading '+'
                case ITypeSymbol { ContainingSymbol: ITypeSymbol typeSymbol }:
                    BuildFrom(typeSymbol, in Builder);
                    Builder.Add('+');
                    Builder.AddRange(Symbol.MetadataName.AsSpan());
                    break;
                default:
                    break;
            }
        }

        BuildFrom(Symbol, in Builder);
    }
}
