namespace SysCredit.Api.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;
using System.Linq;

public static class Helpers
{
    public static string FileName(string Name)
        => $"{Name}{Constants.GeneratedFileExtension}";

    public static TObject? As<TObject>(this object? @object)
        => (TObject?)@object;

    public static bool WhereNotNull<TSource>(TSource? Source)
        => Source is not null;

    public static INamedTypeSymbol GetErrorCodePrefixAttributeMetadataName(this Compilation Compilation)
        => Compilation.GetTypeByMetadataName(Constants.ErrorCodePrefixAttribute)!;

    public static INamedTypeSymbol GetErrorCodeRangeAttributeMetadataName(this Compilation Compilation)
        => Compilation.GetTypeByMetadataName(Constants.ErrorCodeRangeAttribute)!;

    public static INamedTypeSymbol GetErrorCategoryAttributeMetadataName(this Compilation Compilation)
        => Compilation.GetTypeByMetadataName(Constants.ErrorCategoryAttribute)!;

    public static INamedTypeSymbol GetServiceAttributeMetadataName(this Compilation Compilation)
        => Compilation.GetTypeByMetadataName(Constants.ServiceAttribute)!;

    public static AttributeData? GetAttributeIfExists(this INamedTypeSymbol TypeSymbol, INamedTypeSymbol AttributeSymbol)
        => TypeSymbol.GetAttributes().GetAttributeIfExists(AttributeSymbol);

    public static AttributeData? GetAttributeIfExists(this IEnumerable<AttributeData> Source, INamedTypeSymbol AttributeSymbol)
        => Source.FirstOrDefault(Attribute => SymbolEqualityComparer.Default.Equals(AttributeSymbol, Attribute.AttributeClass));

    public static IEnumerable<AttributeSyntax> GetGenericAttributes(this MemberDeclarationSyntax MemberSyntax, int Arity)
    {
        foreach (var Attribute in MemberSyntax.CollectAttributeList())
        {
            if (Attribute is { Name: GenericNameSyntax NameSyntax } && NameSyntax.Arity == Arity)
            {
                yield return Attribute;
            }
        }
    }

    public static IEnumerable<GenericNameSyntax> GetGenericNameSyntaxFromAttributes(this MemberDeclarationSyntax MemberSyntax, int Arity)
    {
        foreach (var Attribute in MemberSyntax.CollectAttributeList())
        {
            if (Attribute is { Name: GenericNameSyntax NameSyntax } && NameSyntax.Arity == Arity)
            {
                yield return NameSyntax;
            }
        }
    }

    public static IEnumerable<AttributeSyntax> CollectAttributeList(this MemberDeclarationSyntax MemberSyntax)
        => MemberSyntax.AttributeLists.Collect();

    public static IEnumerable<AttributeSyntax> Collect(in this SyntaxList<AttributeListSyntax> AttributeLists)
        => AttributeLists.SelectMany(AttributeList => AttributeList.Attributes);

    public static bool HasAttribute(this MemberDeclarationSyntax MemberSyntax, string Name)
        => MemberSyntax.AttributeLists.HasAttribute(Name);

    public static bool HasServiceAttribute(this MemberDeclarationSyntax MemberSyntax)
        => MemberSyntax.AttributeLists.HasServiceAttribute();

    public static bool HasServiceModelAttribute(this MemberDeclarationSyntax MemberSyntax)
        => MemberSyntax.AttributeLists.HasServiceModelAttribute();

    public static bool HasServiceAttribute(in this SyntaxList<AttributeListSyntax> AttributeLists)
        => AttributeLists.HasGenericAttribute(Arity: 1, Name: "Service");

    public static bool HasServiceModelAttribute(in this SyntaxList<AttributeListSyntax> AttributeLists)
        => AttributeLists.HasGenericAttribute(Arity: 1, Name: "ServiceModel");

    public static bool HasGenericAttribute(in this SyntaxList<AttributeListSyntax> AttributeLists, int Arity, string Name)
    {
        foreach (var Attribute in AttributeLists.Collect())
        {
            if (Attribute is { Name: GenericNameSyntax NameSyntax } && NameSyntax.Arity == Arity && NameSyntax.Identifier.Text == Name)
            {
                return true;
            }
        }

        return false;
    }

    public static bool HasAttribute(in this SyntaxList<AttributeListSyntax> AttributeLists, string Name)
    {
        foreach (var Attribute in AttributeLists.Collect())
        {
            if (Attribute is { Name: IdentifierNameSyntax { Arity: 0 } NameSyntax } && NameSyntax.Identifier.Text == Name)
            {
                return true;
            }
        }

        return false;
    }
}
