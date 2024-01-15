namespace SysCredit.Toolkits.Generators.Models;

using Microsoft.CodeAnalysis;

using SysCredit.Toolkits.Generators.Extensions;
using SysCredit.Toolkits.Generators.Helpers;

using static Microsoft.CodeAnalysis.SymbolDisplayTypeQualificationStyle;

/// <summary>
///     A model describing the hierarchy info for a specific type.
/// </summary>
/// <param name="FileNameHint">
///     The filename hint for the current type.
/// </param>
/// <param name="MetadataName">
///     The metadata name for the current type.
/// </param>
/// <param name="Namespace">
///     Gets the namespace for the current type.
/// </param>
/// <param name="Hierarchy">
///     Gets the sequence of type definitions containing the current type.
/// </param>
public sealed partial record HierarchyInfo(string FileNameHint, string MetadataName, string Namespace, EquatableArray<TypeInfo> Hierarchy)
{
    /// <summary>
    ///     Creates a new <see cref="HierarchyInfo"/> instance from a given <see cref="INamedTypeSymbol"/>.
    /// </summary>
    /// <param name="TypeSymbol">
    ///     The input <see cref="INamedTypeSymbol"/> instance to gather info for.
    /// </param>
    /// <returns>
    ///     A <see cref="HierarchyInfo"/> instance describing <paramref name="TypeSymbol"/>.
    /// </returns>
    public static HierarchyInfo From(INamedTypeSymbol TypeSymbol)
    {
        using ImmutableArrayBuilder<TypeInfo> Hierarchy = ImmutableArrayBuilder<TypeInfo>.Rent();

        for (INamedTypeSymbol? Parent = TypeSymbol; Parent is not null; Parent = Parent.ContainingType)
        {
            Hierarchy.Add(new TypeInfo(
                Parent.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
                Parent.TypeKind,
                Parent.IsRecord));
        }

        return new(
            TypeSymbol.GetFullyQualifiedMetadataName(),
            TypeSymbol.MetadataName,
            TypeSymbol.ContainingNamespace.ToDisplayString(new(typeQualificationStyle: NameAndContainingTypesAndNamespaces)),
            Hierarchy.ToImmutable());
    }
}
