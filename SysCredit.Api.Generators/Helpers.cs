﻿namespace SysCredit.Api.Generators;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

public static class Helpers
{
    public static string FileName(string Name)
        => $"{Name}{Constants.GeneratedFileExtension}";

    public static TObject? As<TObject>(this object? @object)
        => (TObject?)@object;

    public static INamedTypeSymbol GetErrorCodePrefixAttributeMetadata(this Compilation Compilation)
        => Compilation.GetTypeByMetadataName(Constants.ErrorCodePrefixAttribute)!;

    public static AttributeData? GetAttributeIfExists(this INamedTypeSymbol TypeSymbol, INamedTypeSymbol AttributeSymbol)
        => TypeSymbol.GetAttributes().GetAttributeIfExists(AttributeSymbol);

    public static AttributeData? GetAttributeIfExists(this IEnumerable<AttributeData> Source, INamedTypeSymbol AttributeSymbol)
        => Source.FirstOrDefault(Attribute => SymbolEqualityComparer.Default.Equals(AttributeSymbol, Attribute.AttributeClass));
}