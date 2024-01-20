namespace Microsoft.AspNetCore.App.Analyzers.Infrastructure;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

using Microsoft.AspNetCore.Analyzers.Infrastructure;
using Microsoft.CodeAnalysis;

/// <summary>
/// 
/// </summary>
public class WellKnownTypes
{
    private static readonly BoundedCacheWithFactory<Compilation, WellKnownTypes> LazyWellKnownTypesCache = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Compilation"></param>
    /// <returns></returns>
    public static WellKnownTypes GetOrCreate(Compilation Compilation) =>
        LazyWellKnownTypesCache.GetOrCreateValue(Compilation, static C => new WellKnownTypes(C));

    private readonly INamedTypeSymbol?[] _LazyWellKnownTypes;
    private readonly Compilation _Compilation;

    static WellKnownTypes()
    {
        AssertEnumAndTableInSync();
    }

    [Conditional("DEBUG")]
    private static void AssertEnumAndTableInSync()
    {
        for (var I = 0; I < WellKnownTypeData.WellKnownTypeNames.Length; ++I)
        {
            var Name = WellKnownTypeData.WellKnownTypeNames[I];
            var TypeId = (WellKnownTypeData.WellKnownType)I;

            var TypeIdName = TypeId.ToString().Replace("__", "+").Replace('_', '.');

            var Separator = Name.IndexOf('`');
            if (Separator >= 0)
            {
                // Ignore type parameter qualifier for generic types.
                Name = Name[..Separator];
                TypeIdName = TypeIdName[..Separator];
            }

            Debug.Assert(Name == TypeIdName, $"Enum name ({TypeIdName}) and type name ({Name}) must match at {I}");
        }
    }

    private WellKnownTypes(Compilation Compilation)
    {
        _LazyWellKnownTypes = new INamedTypeSymbol?[WellKnownTypeData.WellKnownTypeNames.Length];
        _Compilation = Compilation;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public INamedTypeSymbol Get(SpecialType Type)
    {
        return _Compilation.GetSpecialType(Type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public INamedTypeSymbol Get(WellKnownTypeData.WellKnownType Type)
    {
        var Index = (int)Type;
        var Symbol = _LazyWellKnownTypes[Index];
        if (Symbol is not null)
        {
            return Symbol;
        }

        // Symbol hasn't been added to the cache yet.
        // Resolve symbol from name, cache, and return.
        return GetAndCache(Index);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Index"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private INamedTypeSymbol GetAndCache(int Index)
    {
        var Result = GetTypeByMetadataNameInTargetAssembly(WellKnownTypeData.WellKnownTypeNames[Index]);
        if (Result == null)
        {
            throw new InvalidOperationException($"Failed to resolve well-known type '{WellKnownTypeData.WellKnownTypeNames[Index]}'.");
        }

        Interlocked.CompareExchange(ref _LazyWellKnownTypes[Index], Result, null);

        // GetTypeByMetadataName should always return the same instance for a name.
        // To ensure we have a consistent value, for thread safety, return symbol set in the array.
        return _LazyWellKnownTypes[Index]!;
    }

    // Filter for types within well-known (framework-owned) assemblies only.
    private INamedTypeSymbol? GetTypeByMetadataNameInTargetAssembly(string MetadataName)
    {
        var Types = _Compilation.GetTypesByMetadataName(MetadataName);

        if (Types.Length == 0)
        {
            return null;
        }

        if (Types.Length == 1)
        {
            return Types[0];
        }

        // Multiple types match the name. This is most likely caused by someone reusing the namespace + type name in their apps or libraries.
        // Workaround this situation by prioritizing types in System and Microsoft assemblies.
        foreach (var Type in Types)
        {
            if (Type.ContainingAssembly.Identity.Name.StartsWith("System.", StringComparison.Ordinal) || Type.ContainingAssembly.Identity.Name.StartsWith("Microsoft.", StringComparison.Ordinal))
            {
                return Type;
            }
        }

        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="WellKnownTypes"></param>
    /// <returns></returns>
    public bool IsType(ITypeSymbol Type, WellKnownTypeData.WellKnownType[] WellKnownTypes) => IsType(Type, WellKnownTypes, out var _);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="WellKnownTypes"></param>
    /// <param name="Match"></param>
    /// <returns></returns>
    public bool IsType(ITypeSymbol Type, WellKnownTypeData.WellKnownType[] WellKnownTypes, [NotNullWhen(true)] out WellKnownTypeData.WellKnownType? Match)
    {
        foreach (var WellKnownType in WellKnownTypes)
        {
            if (SymbolEqualityComparer.Default.Equals(Type, Get(WellKnownType)))
            {
                Match = WellKnownType;
                return true;
            }
        }

        Match = null;
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="InterfaceWellKnownTypes"></param>
    /// <returns></returns>
    public bool Implements(ITypeSymbol Type, WellKnownTypeData.WellKnownType[] InterfaceWellKnownTypes)
    {
        foreach (var WellKnownType in InterfaceWellKnownTypes)
        {
            if (Implements(Type, Get(WellKnownType)))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="InterfaceType"></param>
    /// <returns></returns>
    public static bool Implements(ITypeSymbol? Type, ITypeSymbol InterfaceType)
    {
        if (Type is null)
        {
            return false;
        }

        foreach (var T in Type.AllInterfaces)
        {
            if (SymbolEqualityComparer.Default.Equals(T, InterfaceType))
            {
                return true;
            }
        }

        return false;
    }
}
