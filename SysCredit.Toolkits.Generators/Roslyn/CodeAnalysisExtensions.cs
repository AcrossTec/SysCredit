namespace Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

using Microsoft.CodeAnalysis.Operations;

/// <summary>
///     Métodos de extensión para el análisis de código.
/// </summary>
public static class CodeAnalysisExtensions
{
    /// <summary>
    ///     Verifica si el atributo <paramref name="Attribute"/> existe en <paramref name="TypeSymbol"/>.
    /// </summary>
    /// <param name="TypeSymbol"></param>
    /// <param name="Attribute"></param>
    /// <param name="Inherit"></param>
    /// <returns></returns>
    public static bool HasAttribute(this ITypeSymbol TypeSymbol, ITypeSymbol Attribute, bool Inherit)
        => GetAttributes(TypeSymbol, Attribute, Inherit).Any();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MethodSymbol"></param>
    /// <param name="Attribute"></param>
    /// <param name="Inherit"></param>
    /// <returns></returns>
    public static bool HasAttribute(this IMethodSymbol MethodSymbol, ITypeSymbol Attribute, bool Inherit)
        => GetAttributes(MethodSymbol, Attribute, Inherit).Any();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Symbol"></param>
    /// <param name="Attribute"></param>
    /// <returns></returns>
    public static IEnumerable<AttributeData> GetAttributes(this ISymbol Symbol, ITypeSymbol Attribute)
    {
        foreach (var DeclaredAttribute in Symbol.GetAttributes())
        {
            if (DeclaredAttribute.AttributeClass is not null && Attribute.IsAssignableFrom(DeclaredAttribute.AttributeClass))
            {
                yield return DeclaredAttribute;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MethodSymbol"></param>
    /// <param name="Attribute"></param>
    /// <param name="Inherit"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<AttributeData> GetAttributes(this IMethodSymbol MethodSymbol, ITypeSymbol Attribute, bool Inherit)
    {
        Debug.Assert(MethodSymbol != null);
        Attribute = Attribute ?? throw new ArgumentNullException(nameof(Attribute));

        IMethodSymbol? Current = MethodSymbol;
        while (Current != null)
        {
            foreach (var AttributeData in GetAttributes(Current, Attribute))
            {
                yield return AttributeData;
            }

            if (!Inherit)
            {
                break;
            }

            Current = Current.IsOverride ? Current.OverriddenMethod : null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TypeSymbol"></param>
    /// <param name="Attribute"></param>
    /// <param name="Inherit"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<AttributeData> GetAttributes(this ITypeSymbol TypeSymbol, ITypeSymbol Attribute, bool Inherit)
    {
        TypeSymbol = TypeSymbol ?? throw new ArgumentNullException(nameof(TypeSymbol));
        Attribute = Attribute ?? throw new ArgumentNullException(nameof(Attribute));

        foreach (var Type in GetTypeHierarchy(TypeSymbol))
        {
            foreach (var AttributeData in GetAttributes(Type, Attribute))
            {
                yield return AttributeData;
            }

            if (!Inherit)
            {
                break;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="PropertySymbol"></param>
    /// <param name="Attribute"></param>
    /// <param name="Inherit"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool HasAttribute(this IPropertySymbol PropertySymbol, ITypeSymbol Attribute, bool Inherit)
    {
        PropertySymbol = PropertySymbol ?? throw new ArgumentNullException(nameof(PropertySymbol));
        Attribute = Attribute ?? throw new ArgumentNullException(nameof(Attribute));

        if (!Inherit)
        {
            return HasAttribute(PropertySymbol, Attribute);
        }

        IPropertySymbol? Current = PropertySymbol;
        while (Current != null)
        {
            if (Current.HasAttribute(Attribute))
            {
                return true;
            }

            Current = Current.IsOverride ? Current.OverriddenProperty : null;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Target"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool IsAssignableFrom(this ITypeSymbol Source, ITypeSymbol Target)
    {
        Source = Source ?? throw new ArgumentNullException(nameof(Source));
        Target = Target ?? throw new ArgumentNullException(nameof(Target));

        if (SymbolEqualityComparer.Default.Equals(Source, Target))
        {
            return true;
        }

        if (Source.TypeKind == TypeKind.Interface)
        {
            foreach (var @interface in Target.AllInterfaces)
            {
                if (SymbolEqualityComparer.Default.Equals(Source, @interface))
                {
                    return true;
                }
            }

            return false;
        }

        foreach (var Type in Target.GetTypeHierarchy())
        {
            if (SymbolEqualityComparer.Default.Equals(Source, Type))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Symbol"></param>
    /// <param name="Attribute"></param>
    /// <returns></returns>
    public static bool HasAttribute(this ISymbol Symbol, ITypeSymbol Attribute)
    {
        foreach (var DeclaredAttribute in Symbol.GetAttributes())
        {
            if (DeclaredAttribute.AttributeClass is not null && Attribute.IsAssignableFrom(DeclaredAttribute.AttributeClass))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TypeSymbol"></param>
    /// <returns></returns>
    private static IEnumerable<ITypeSymbol> GetTypeHierarchy(this ITypeSymbol? TypeSymbol)
    {
        while (TypeSymbol != null)
        {
            yield return TypeSymbol;

            TypeSymbol = TypeSymbol.BaseType;
        }
    }

    /// <summary>
    ///     Adapted from https://github.com/dotnet/roslyn/blob/929272/src/Workspaces/Core/Portable/Shared/Extensions/IMethodSymbolExtensions.cs#L61
    /// </summary>
    /// <param name="Method"></param>
    /// <returns></returns>
    public static IEnumerable<IMethodSymbol> GetAllMethodSymbolsOfPartialParts(this IMethodSymbol Method)
    {
        if (Method.PartialDefinitionPart != null)
        {
            Debug.Assert(Method.PartialImplementationPart == null && !SymbolEqualityComparer.Default.Equals(Method.PartialDefinitionPart, Method));
            yield return Method;
            yield return Method.PartialDefinitionPart;
        }
        else if (Method.PartialImplementationPart != null)
        {
            Debug.Assert(!SymbolEqualityComparer.Default.Equals(Method.PartialImplementationPart, Method));
            yield return Method.PartialImplementationPart;
            yield return Method;
        }
        else
        {
            yield return Method;
        }
    }

    /// <summary>
    ///     Adapted from IOperationExtensions.GetReceiverType in dotnet/roslyn-analyzers.
    ///     See https://github.com/dotnet/roslyn-analyzers/blob/762b08948cdcc1d94352fba681296be7bf474dd7/src/Utilities/Compiler/Extensions/IOperationExtensions.cs#L22-L51
    /// </summary>
    /// <param name="Invocation"></param>
    /// <param name="CancellationToken"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetReceiverType(this IInvocationOperation Invocation, CancellationToken CancellationToken)
    {
        if (Invocation.Instance != null)
        {
            return GetReceiverType(Invocation.Instance.Syntax, Invocation.SemanticModel, CancellationToken);
        }
        else if (Invocation.TargetMethod.IsExtensionMethod && !Invocation.TargetMethod.Parameters.IsEmpty)
        {
            var FirstArg = Invocation.Arguments.FirstOrDefault();
            if (FirstArg != null)
            {
                return GetReceiverType(FirstArg.Value.Syntax, Invocation.SemanticModel, CancellationToken);
            }
            else if (Invocation.TargetMethod.Parameters[0].IsParams)
            {
                return Invocation.TargetMethod.Parameters[0].Type as INamedTypeSymbol;
            }
        }

        return null;

        static INamedTypeSymbol? GetReceiverType(SyntaxNode ReceiverSyntax, SemanticModel? Model, CancellationToken CancellationToken)
        {
            var TypeInfo = Model?.GetTypeInfo(ReceiverSyntax, CancellationToken);
            return TypeInfo?.Type as INamedTypeSymbol;
        }
    }
}
