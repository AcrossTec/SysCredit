namespace SysCredit.Toolkits.Generators.Extensions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

/// <summary>
///     Extension methods for the <see cref="Compilation"/> type.
/// </summary>
public static class CompilationExtensions
{
    /// <summary>
    ///     Checks whether a given compilation (assumed to be for C#) is using at least a given language version.
    /// </summary>
    /// <param name="Compilation">
    ///     The <see cref="Compilation"/> to consider for analysis.
    /// </param>
    /// <param name="LanguageVersion">
    ///     The minimum language version to check.
    /// </param>
    /// <returns>
    ///     Whether <paramref name="Compilation"/> is using at least the specified language version.
    /// </returns>
    public static bool HasLanguageVersionAtLeastEqualTo(this Compilation Compilation, LanguageVersion LanguageVersion)
    {
        return ((CSharpCompilation)Compilation).LanguageVersion >= LanguageVersion;
    }

    /// <summary>
    ///     <para>
    ///         Checks whether or not a type with a specified metadata name is accessible from a given <see cref="Compilation"/> instance.
    ///     </para>
    ///     <para>
    ///         This method enumerates candidate type symbols to find a match in the following order:
    ///         <list type="number">
    ///             <item>
    ///                 <description>
    ///                     If only one type with the given name is found within the compilation and its referenced assemblies, check its accessibility.
    ///                 </description>
    ///             </item>
    ///             <item>
    ///                 <description>
    ///                     If the current <paramref name="Compilation"/> defines the symbol, check its accessibility.
    ///                 </description>
    ///             </item>
    ///             <item>
    ///                 <description>
    ///                     Otherwise, check whether the type exists and is accessible from any of the referenced assemblies.
    ///                 </description>
    ///             </item>
    ///         </list>
    ///     </para>
    /// </summary>
    /// <param name="Compilation">
    ///     The <see cref="Compilation"/> to consider for analysis.
    /// </param>
    /// <param name="FullyQualifiedMetadataName">
    ///     The fully-qualified metadata type name to find.
    /// </param>
    /// <returns>
    ///     Whether a type with the specified metadata name can be accessed from the given compilation.
    /// </returns>
    public static bool HasAccessibleTypeWithMetadataName(this Compilation Compilation, string FullyQualifiedMetadataName)
    {
        // Try to get the unique type with this name
        INamedTypeSymbol? Type = Compilation.GetTypeByMetadataName(FullyQualifiedMetadataName);

        // If there is only a single matching symbol, check its accessibility
        if (Type is not null)
        {
            return Type.CanBeAccessedFrom(Compilation.Assembly);
        }

        // Otherwise, try to get the unique type with this name originally defined in 'compilation'
        Type ??= Compilation.Assembly.GetTypeByMetadataName(FullyQualifiedMetadataName);

        if (Type is not null)
        {
            return Type.CanBeAccessedFrom(Compilation.Assembly);
        }

        // Otherwise, check whether the type is defined and accessible from any of the referenced assemblies
        foreach (IModuleSymbol Module in Compilation.Assembly.Modules)
        {
            foreach (IAssemblySymbol ReferencedAssembly in Module.ReferencedAssemblySymbols)
            {
                if (ReferencedAssembly.GetTypeByMetadataName(FullyQualifiedMetadataName) is not INamedTypeSymbol CurrentType)
                {
                    continue;
                }

                switch (CurrentType.GetEffectiveAccessibility())
                {
                    case Accessibility.Public:
                    case Accessibility.Internal when ReferencedAssembly.GivesAccessTo(Compilation.Assembly):
                        return true;
                    default:
                        continue;
                }
            }
        }

        return false;
    }

    /// <summary>
    ///     Tries to build a map of <see cref="INamedTypeSymbol"/> instances form the input mapping of names.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of keys for each symbol.
    /// </typeparam>
    /// <param name="Compilation">
    ///     The <see cref="Compilation"/> to consider for analysis.
    /// </param>
    /// <param name="TypeNames">
    ///     The input mapping of <typeparamref name="T"/> keys to fully qualified type names.
    /// </param>
    /// <param name="TypeSymbols">
    ///     The resulting mapping of <typeparamref name="T"/> keys to resolved <see cref="INamedTypeSymbol"/> instances.
    /// </param>
    /// <returns>
    ///     Whether all requested <see cref="INamedTypeSymbol"/> instances could be resolved.
    /// </returns>
    public static bool TryBuildNamedTypeSymbolMap<T>(this Compilation Compilation, IEnumerable<KeyValuePair<T, string>> TypeNames, [NotNullWhen(true)] out ImmutableDictionary<T, INamedTypeSymbol>? TypeSymbols) where T : IEquatable<T>
    {
        ImmutableDictionary<T, INamedTypeSymbol>.Builder Builder = ImmutableDictionary.CreateBuilder<T, INamedTypeSymbol>();

        foreach (KeyValuePair<T, string> Pair in TypeNames)
        {
            if (Compilation.GetTypeByMetadataName(Pair.Value) is not INamedTypeSymbol AttributeSymbol)
            {
                TypeSymbols = null;
                return false;
            }

            Builder.Add(Pair.Key, AttributeSymbol);
        }

        TypeSymbols = Builder.ToImmutable();
        return true;
    }
}
