namespace Microsoft.AspNetCore.Analyzers.Infrastructure;

using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Analyzers.RouteEmbeddedLanguage.Infrastructure;
using Microsoft.AspNetCore.App.Analyzers.Infrastructure;
using Microsoft.CodeAnalysis;

using WellKnownType = App.Analyzers.Infrastructure.WellKnownTypeData.WellKnownType;

/// <summary>
/// 
/// </summary>
public static class ParsabilityHelper
{
    private static readonly BoundedCacheWithFactory<ITypeSymbol, (BindabilityMethod?, IMethodSymbol?)> BindabilityCache = new();
    private static readonly BoundedCacheWithFactory<ITypeSymbol, (Parsability, ParsabilityMethod?)> ParsabilityCache = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TypeSymbol"></param>
    /// <param name="WellKnownTypes"></param>
    /// <param name="ParsabilityMethod"></param>
    /// <returns></returns>
    public static bool IsTypeAlwaysParsable(ITypeSymbol TypeSymbol, WellKnownTypes WellKnownTypes, [NotNullWhen(true)] out ParsabilityMethod? ParsabilityMethod)
    {
        // Any enum is valid.
        if (TypeSymbol.TypeKind == TypeKind.Enum)
        {
            ParsabilityMethod = Infrastructure.ParsabilityMethod.Enum;
            return true;
        }

        // Uri is valid.
        if (SymbolEqualityComparer.Default.Equals(TypeSymbol, WellKnownTypes.Get(WellKnownType.System_Uri)))
        {
            ParsabilityMethod = Infrastructure.ParsabilityMethod.Uri;
            return true;
        }

        // Strings are valid.
        if (TypeSymbol.SpecialType == SpecialType.System_String)
        {
            ParsabilityMethod = Infrastructure.ParsabilityMethod.String;
            return true;
        }

        ParsabilityMethod = null;
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TypeSymbol"></param>
    /// <param name="WellKnownTypes"></param>
    /// <returns></returns>
    public static Parsability GetParsability(ITypeSymbol TypeSymbol, WellKnownTypes WellKnownTypes)
    {
        return GetParsability(TypeSymbol, WellKnownTypes, out var _);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TypeSymbol"></param>
    /// <param name="WellKnownTypes"></param>
    /// <param name="ParsabilityMethod"></param>
    /// <returns></returns>
    public static Parsability GetParsability(ITypeSymbol TypeSymbol, WellKnownTypes WellKnownTypes, [NotNullWhen(false)] out ParsabilityMethod? ParsabilityMethod)
    {
        var Parsability = Infrastructure.Parsability.NotParsable;
        ParsabilityMethod = null;

        (Parsability, ParsabilityMethod) = ParsabilityCache.GetOrCreateValue(TypeSymbol, (TypeSymbol) =>
        {
            if (IsTypeAlwaysParsable(TypeSymbol, WellKnownTypes, out var ParsabilityMethod))
            {
                return (Infrastructure.Parsability.Parsable, ParsabilityMethod);
            }

            // MyType : IParsable<MyType>()
            if (IsParsableViaIParsable(TypeSymbol, WellKnownTypes))
            {
                return (Infrastructure.Parsability.Parsable, Infrastructure.ParsabilityMethod.IParsable);
            }

            // Check if the parameter type has a public static TryParse method.
            var TryParseMethods = TypeSymbol.GetThisAndBaseTypes()
                .SelectMany(T => T.GetMembers("TryParse"))
                .OfType<IMethodSymbol>();

            if (TryParseMethods.Any(m => IsTryParseWithFormat(m, WellKnownTypes)))
            {
                return (Infrastructure.Parsability.Parsable, Infrastructure.ParsabilityMethod.TryParseWithFormatProvider);
            }

            if (TryParseMethods.Any(IsTryParse))
            {
                return (Infrastructure.Parsability.Parsable, Infrastructure.ParsabilityMethod.TryParse);
            }

            return ((Parsability, ParsabilityMethod?))(Infrastructure.Parsability.NotParsable, null);
        });

        return Parsability;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MethodSymbol"></param>
    /// <returns></returns>
    public static bool IsTryParse(IMethodSymbol MethodSymbol)
    {
        return MethodSymbol.DeclaredAccessibility == Accessibility.Public
            && MethodSymbol.IsStatic
            && MethodSymbol.ReturnType.SpecialType == SpecialType.System_Boolean
            && MethodSymbol.Parameters.Length == 2
            && MethodSymbol.Parameters[0].Type.SpecialType == SpecialType.System_String
            && MethodSymbol.Parameters[1].RefKind == RefKind.Out;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MethodSymbol"></param>
    /// <param name="WellKnownTypes"></param>
    /// <returns></returns>
    public static bool IsTryParseWithFormat(IMethodSymbol MethodSymbol, WellKnownTypes WellKnownTypes)
    {
        return MethodSymbol.DeclaredAccessibility == Accessibility.Public
            && MethodSymbol.IsStatic
            && MethodSymbol.ReturnType.SpecialType == SpecialType.System_Boolean
            && MethodSymbol.Parameters.Length == 3
            && MethodSymbol.Parameters[0].Type.SpecialType == SpecialType.System_String
            && SymbolEqualityComparer.Default.Equals(MethodSymbol.Parameters[1].Type, WellKnownTypes.Get(WellKnownType.System_IFormatProvider))
            && MethodSymbol.Parameters[2].RefKind == RefKind.Out;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TypeSymbol"></param>
    /// <param name="WellKnownTypes"></param>
    /// <returns></returns>
    public static bool IsParsableViaIParsable(ITypeSymbol TypeSymbol, WellKnownTypes WellKnownTypes)
    {
        var IParsableTypeSymbol = WellKnownTypes.Get(WellKnownType.System_IParsable_T);
        var ImplementsIParsable = TypeSymbol.AllInterfaces.Any(I => SymbolEqualityComparer.Default.Equals(I.ConstructedFrom, IParsableTypeSymbol));
        return ImplementsIParsable;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TypeSymbol"></param>
    /// <param name="WellKnownTypes"></param>
    /// <returns></returns>
    public static bool IsBindableViaIBindableFromHttpContext(ITypeSymbol TypeSymbol, WellKnownTypes WellKnownTypes)
    {
        var IBindableFromHttpContextTypeSymbol = WellKnownTypes.Get(WellKnownType.Microsoft_AspNetCore_Http_IBindableFromHttpContext_T);
        var ConstructedTypeSymbol = TypeSymbol.AllInterfaces.FirstOrDefault(I => SymbolEqualityComparer.Default.Equals(I.ConstructedFrom, IBindableFromHttpContextTypeSymbol));
        return ConstructedTypeSymbol != null && SymbolEqualityComparer.Default.Equals(ConstructedTypeSymbol.TypeArguments[0].UnwrapTypeSymbol(UnwrapNullable: true), TypeSymbol);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MethodSymbol"></param>
    /// <param name="TypeSymbol"></param>
    /// <param name="WellKnownTypes"></param>
    /// <returns></returns>
    public static bool IsBindAsync(IMethodSymbol MethodSymbol, ITypeSymbol TypeSymbol, WellKnownTypes WellKnownTypes)
    {
        return MethodSymbol.DeclaredAccessibility == Accessibility.Public
            && MethodSymbol.IsStatic
            && MethodSymbol.Parameters.Length == 1
            && SymbolEqualityComparer.Default.Equals(MethodSymbol.Parameters[0].Type, WellKnownTypes.Get(WellKnownType.Microsoft_AspNetCore_Http_HttpContext))
            && MethodSymbol.ReturnType is INamedTypeSymbol ReturnType
            && SymbolEqualityComparer.Default.Equals(ReturnType.ConstructedFrom, WellKnownTypes.Get(WellKnownType.System_Threading_Tasks_ValueTask_T))
            && SymbolEqualityComparer.Default.Equals(ReturnType.TypeArguments[0], TypeSymbol);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MethodSymbol"></param>
    /// <param name="TypeSymbol"></param>
    /// <param name="WellKnownTypes"></param>
    /// <returns></returns>
    public static bool IsBindAsyncWithParameter(IMethodSymbol MethodSymbol, ITypeSymbol TypeSymbol, WellKnownTypes WellKnownTypes)
    {
        return MethodSymbol.DeclaredAccessibility == Accessibility.Public
            && MethodSymbol.IsStatic
            && MethodSymbol.Parameters.Length == 2
            && SymbolEqualityComparer.Default.Equals(MethodSymbol.Parameters[0].Type, WellKnownTypes.Get(WellKnownType.Microsoft_AspNetCore_Http_HttpContext))
            && SymbolEqualityComparer.Default.Equals(MethodSymbol.Parameters[1].Type, WellKnownTypes.Get(WellKnownType.System_Reflection_ParameterInfo))
            && MethodSymbol.ReturnType is INamedTypeSymbol ReturnType
            && IsReturningValueTaskOfTOrNullableT(ReturnType, TypeSymbol, WellKnownTypes);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ReturnType"></param>
    /// <param name="ContainingType"></param>
    /// <param name="WellKnownTypes"></param>
    /// <returns></returns>
    public static bool IsReturningValueTaskOfTOrNullableT(INamedTypeSymbol ReturnType, ITypeSymbol ContainingType, WellKnownTypes WellKnownTypes)
    {
        return SymbolEqualityComparer.Default.Equals(ReturnType.ConstructedFrom, WellKnownTypes.Get(WellKnownType.System_Threading_Tasks_ValueTask_T))
            && SymbolEqualityComparer.Default.Equals(ReturnType.TypeArguments[0].UnwrapTypeSymbol(UnwrapNullable: true), ContainingType);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TypeSymbol"></param>
    /// <param name="WellKnownTypes"></param>
    /// <param name="BindabilityMethod"></param>
    /// <param name="BindMethodSymbol"></param>
    /// <returns></returns>
    public static Bindability GetBindability(ITypeSymbol TypeSymbol, WellKnownTypes WellKnownTypes, out BindabilityMethod? BindabilityMethod, out IMethodSymbol? BindMethodSymbol)
    {
        BindMethodSymbol = null;
        BindabilityMethod = null;
        IMethodSymbol? BindAsyncMethod = null;

        (BindabilityMethod, BindMethodSymbol) = BindabilityCache.GetOrCreateValue(TypeSymbol, (TypeSymbol) =>
        {
            IMethodSymbol? BindMethodSymbol = null;
            BindabilityMethod? BindabilityMethod = null;

            if (IsBindableViaIBindableFromHttpContext(TypeSymbol, WellKnownTypes))
            {
                return (Infrastructure.BindabilityMethod.IBindableFromHttpContext, null);
            }

            var SearchCandidates = TypeSymbol.GetThisAndBaseTypes().Concat(TypeSymbol.AllInterfaces);

            foreach (var Candidate in SearchCandidates)
            {
                var BaseTypeBindAsyncMethods = Candidate.GetMembers("BindAsync");

                foreach (var MethodSymbolCandidate in BaseTypeBindAsyncMethods)
                {
                    if (MethodSymbolCandidate is IMethodSymbol MethodSymbol)
                    {
                        BindAsyncMethod ??= MethodSymbol;

                        if (IsBindAsyncWithParameter(MethodSymbol, TypeSymbol, WellKnownTypes))
                        {
                            BindabilityMethod = Infrastructure.BindabilityMethod.BindAsyncWithParameter;
                            BindMethodSymbol = MethodSymbol;
                            break;
                        }
                        if (IsBindAsync(MethodSymbol, TypeSymbol, WellKnownTypes))
                        {
                            BindabilityMethod = Infrastructure.BindabilityMethod.BindAsync;
                            BindMethodSymbol = MethodSymbol;
                        }
                    }
                }
            }

            return (BindabilityMethod, BindAsyncMethod);
        });

        if (BindabilityMethod is not null)
        {
            return Bindability.Bindable;
        }

        // See if we can give better guidance on why the BindAsync method is no good.
        if (BindAsyncMethod is not null)
        {
            if (BindAsyncMethod.ReturnType is INamedTypeSymbol returnType && !IsReturningValueTaskOfTOrNullableT(returnType, TypeSymbol, WellKnownTypes))
            {
                return Bindability.InvalidReturnType;
            }
        }

        return Bindability.NotBindable;
    }
}


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public enum Parsability
{
    Parsable,
    NotParsable,
}

public enum ParsabilityMethod
{
    String,
    IParsable,
    Enum,
    TryParse,
    TryParseWithFormatProvider,
    Uri,
}

public enum Bindability
{
    Bindable,
    NotBindable,
    InvalidReturnType,
}

public enum BindabilityMethod
{
    IBindableFromHttpContext,
    BindAsync,
    BindAsyncWithParameter,
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
