namespace SysCredit.Api.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SysCredit.Toolkits.Generators.Extensions;

internal partial class ErrorCodeGenerator
{
    private static bool IsSyntaxTargetForGeneration(SyntaxNode Node, CancellationToken Token)
    {
        return Node is ClassDeclarationSyntax ClassDeclaration && ClassDeclaration.HasOrPotentiallyHasAttributes();
    }

    private static AttributeData? GetSemanticTargetForGeneration(GeneratorSyntaxContext Context, CancellationToken Token)
    {
        var ClassSymbol = Context.SemanticModel.GetDeclaredSymbol((ClassDeclarationSyntax)Context.Node)!;
        var ErrorCodePrefixSymbol = Context.SemanticModel.Compilation.GetErrorCodePrefixAttributeMetadata();
        return ClassSymbol.GetAttributeIfExists(ErrorCodePrefixSymbol);
    }

    private static string SelectErrorCodePrefix(AttributeData? Attribute, CancellationToken Token)
    {
        return (string)Attribute!.ConstructorArguments[0].Value!;
    }
}
