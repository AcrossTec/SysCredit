namespace Microsoft.AspNetCore.Analyzers.RouteEmbeddedLanguage.Infrastructure;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

/// <summary>
/// 
/// </summary>
public static class SymbolExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="TypeSymbol"></param>
    /// <param name="UnwrapArray"></param>
    /// <param name="UnwrapNullable"></param>
    /// <returns></returns>
    public static ITypeSymbol UnwrapTypeSymbol(this ITypeSymbol TypeSymbol, bool UnwrapArray = false, bool UnwrapNullable = false)
    {
        INamedTypeSymbol? UnwrappedTypeSymbol = null;

        // If it is an array, and unwrapArray = true, unwrap it before unwrapping nullable.
        if (UnwrapArray && TypeSymbol is IArrayTypeSymbol ArrayTypeSymbol)
        {
            UnwrappedTypeSymbol = ArrayTypeSymbol.ElementType as INamedTypeSymbol;
        }
        else if (TypeSymbol is INamedTypeSymbol NamedTypeSymbol)
        {
            UnwrappedTypeSymbol = NamedTypeSymbol;
        }

        // If it is nullable, unwrap it.
        if (UnwrapNullable && UnwrappedTypeSymbol?.ConstructedFrom.SpecialType == SpecialType.System_Nullable_T)
        {
            UnwrappedTypeSymbol = UnwrappedTypeSymbol.TypeArguments[0] as INamedTypeSymbol;
        }

        return UnwrappedTypeSymbol ?? TypeSymbol;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static IEnumerable<ITypeSymbol> GetThisAndBaseTypes(this ITypeSymbol? Type)
    {
        var Current = Type;

        while (Current != null)
        {
            yield return Current;
            Current = Current.BaseType;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Symbol"></param>
    /// <param name="AttributeType"></param>
    /// <returns></returns>
    public static bool HasAttribute(this ISymbol Symbol, INamedTypeSymbol AttributeType)
    {
        foreach (var AttributeData in Symbol.GetAttributes())
        {
            if (SymbolEqualityComparer.Default.Equals(AttributeData.AttributeClass, AttributeType))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Attributes"></param>
    /// <param name="AttributeType"></param>
    /// <returns></returns>
    public static bool HasAttribute(this ImmutableArray<AttributeData> Attributes, INamedTypeSymbol AttributeType)
    {
        return Attributes.TryGetAttribute(AttributeType, out _);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Attributes"></param>
    /// <param name="AttributeType"></param>
    /// <param name="MatchedAttribute"></param>
    /// <returns></returns>
    public static bool TryGetAttribute(this ImmutableArray<AttributeData> Attributes, INamedTypeSymbol AttributeType, [NotNullWhen(true)] out AttributeData? MatchedAttribute)
    {
        foreach (var AttributeData in Attributes)
        {
            if (SymbolEqualityComparer.Default.Equals(AttributeData.AttributeClass, AttributeType))
            {
                MatchedAttribute = AttributeData;
                return true;
            }
        }

        MatchedAttribute = null;
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Symbol"></param>
    /// <param name="InterfaceType"></param>
    /// <returns></returns>
    public static bool HasAttributeImplementingInterface(this ISymbol Symbol, INamedTypeSymbol InterfaceType)
    {
        return Symbol.TryGetAttributeImplementingInterface(InterfaceType, out var _);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Symbol"></param>
    /// <param name="InterfaceType"></param>
    /// <param name="MatchedAttribute"></param>
    /// <returns></returns>
    public static bool TryGetAttributeImplementingInterface(this ISymbol Symbol, INamedTypeSymbol InterfaceType, [NotNullWhen(true)] out AttributeData? MatchedAttribute)
    {
        foreach (var AttributeData in Symbol.GetAttributes())
        {
            if (AttributeData.AttributeClass is not null && AttributeData.AttributeClass.Implements(InterfaceType))
            {
                MatchedAttribute = AttributeData;
                return true;
            }
        }

        MatchedAttribute = null;
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Attributes"></param>
    /// <param name="InterfaceType"></param>
    /// <returns></returns>
    public static bool HasAttributeImplementingInterface(this ImmutableArray<AttributeData> Attributes, INamedTypeSymbol InterfaceType)
    {
        return Attributes.TryGetAttributeImplementingInterface(InterfaceType, out var _);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Attributes"></param>
    /// <param name="InterfaceType"></param>
    /// <param name="MatchedAttribute"></param>
    /// <returns></returns>
    public static bool TryGetAttributeImplementingInterface(this ImmutableArray<AttributeData> Attributes, INamedTypeSymbol InterfaceType, [NotNullWhen(true)] out AttributeData? MatchedAttribute)
    {
        foreach (var AttributeData in Attributes)
        {
            if (AttributeData.AttributeClass is not null && AttributeData.AttributeClass.Implements(InterfaceType))
            {
                MatchedAttribute = AttributeData;
                return true;
            }
        }

        MatchedAttribute = null;
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="InterfaceType"></param>
    /// <returns></returns>
    public static bool Implements(this ITypeSymbol Type, ITypeSymbol InterfaceType)
    {
        foreach (var T in Type.AllInterfaces)
        {
            if (SymbolEqualityComparer.Default.Equals(T, InterfaceType))
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
    /// <param name="TypeName"></param>
    /// <param name="SemanticModel"></param>
    /// <returns></returns>
    public static bool IsType(this INamedTypeSymbol Type, string TypeName, SemanticModel SemanticModel)
        => SymbolEqualityComparer.Default.Equals(Type, SemanticModel.Compilation.GetTypeByMetadataName(TypeName));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="OtherType"></param>
    /// <returns></returns>
    public static bool IsType(this INamedTypeSymbol Type, INamedTypeSymbol OtherType)
        => SymbolEqualityComparer.Default.Equals(Type, OtherType);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Symbol"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static ITypeSymbol GetParameterType(this ISymbol Symbol)
    {
        return Symbol switch
        {
            IParameterSymbol ParameterSymbol => ParameterSymbol.Type,
            IPropertySymbol PropertySymbol => PropertySymbol.Type,
            _ => throw new InvalidOperationException($"Unexpected symbol type: {Symbol}")
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Symbol"></param>
    /// <returns></returns>
    public static ImmutableArray<IParameterSymbol> GetParameters(this ISymbol? Symbol)
        => Symbol switch
        {
            IMethodSymbol MethodSymbol => MethodSymbol.Parameters,
            IPropertySymbol ParameterSymbol => ParameterSymbol.Parameters,
            _ => ImmutableArray<IParameterSymbol>.Empty,
        };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Info"></param>
    /// <returns></returns>
    public static ISymbol? GetAnySymbol(this SymbolInfo Info)
        => Info.Symbol ?? Info.CandidateSymbols.FirstOrDefault();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ParameterSymbol"></param>
    /// <returns></returns>
    public static bool IsOptional(this IParameterSymbol ParameterSymbol) =>
        ParameterSymbol.Type is INamedTypeSymbol
        {
            NullableAnnotation: NullableAnnotation.Annotated
        } || ParameterSymbol.HasExplicitDefaultValue;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="PropertySymbol"></param>
    /// <returns></returns>
    public static bool IsOptional(this IPropertySymbol PropertySymbol) =>
        PropertySymbol.Type is INamedTypeSymbol
        {
            NullableAnnotation: NullableAnnotation.Annotated
        } && !PropertySymbol.IsRequired;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ParameterSymbol"></param>
    /// <returns></returns>
    public static string GetDefaultValueString(this IParameterSymbol ParameterSymbol)
    {
        return !ParameterSymbol.HasExplicitDefaultValue
            ? "null"
            : InnerGetDefaultValueString(ParameterSymbol.ExplicitDefaultValue);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="DefaultValue"></param>
    /// <returns></returns>
    private static string InnerGetDefaultValueString(object? DefaultValue)
    {
        return DefaultValue switch
        {
            string @string => SymbolDisplay.FormatLiteral(@string, true),
            char @char => SymbolDisplay.FormatLiteral(@char, true),
            bool @bool => @bool ? "true" : "false",
            null => "default",
            float @float when @float is float.NegativeInfinity => "float.NegativeInfinity",
            float @float when @float is float.PositiveInfinity => "float.PositiveInfinity",
            float @float when @float is float.NaN => "float.NaN",
            float @float => $"{SymbolDisplay.FormatPrimitive(@float, false, false)}F",
            double @double when @double is double.NegativeInfinity => "double.NegativeInfinity",
            double @double when @double is double.PositiveInfinity => "double.PositiveInfinity",
            double @double when @double is double.NaN => "double.NaN",
            decimal @decimal => $"{SymbolDisplay.FormatPrimitive(@decimal, false, false)}M",
            _ => SymbolDisplay.FormatPrimitive(DefaultValue, false, false),
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Attribute"></param>
    /// <param name="ArgumentName"></param>
    /// <param name="ArgumentValue"></param>
    /// <returns></returns>
    public static bool TryGetNamedArgumentValue<T>(this AttributeData Attribute, string ArgumentName, out T? ArgumentValue)
    {
        ArgumentValue = default;

        foreach (var NamedArgument in Attribute.NamedArguments)
        {
            if (string.Equals(NamedArgument.Key, ArgumentName, StringComparison.Ordinal))
            {
                var RouteParameterNameConstant = NamedArgument.Value;
                ArgumentValue = (T?)RouteParameterNameConstant.Value;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ParameterSymbol"></param>
    /// <returns></returns>
    public static string GetParameterInfoFromConstructorCode(this IParameterSymbol ParameterSymbol)
    {
        if (ParameterSymbol is { ContainingSymbol: IMethodSymbol Constructor })
        {
            var ConstructedType = $"typeof({ParameterSymbol.ContainingType.ToDisplayString()})";
            var ParameterTypes = Constructor.Parameters.Select(Parameter => $"typeof({Parameter.Type.ToDisplayString()})");
            var ParameterTypesString = string.Join(", ", ParameterTypes);
            var GetConstructorParameters = $$"""new[] { {{ParameterTypesString}} }""";
            return $"{ConstructedType}.GetConstructor({GetConstructorParameters})?.GetParameters()[{ParameterSymbol.Ordinal}]";
        }

        return "null";
    }
}
