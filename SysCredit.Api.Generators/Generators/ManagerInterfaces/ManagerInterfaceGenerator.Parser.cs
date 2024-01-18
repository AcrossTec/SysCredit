namespace SysCredit.Api.Generators;

using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SysCredit.Toolkits.Generators.Extensions;

/// <inheritdoc />
public partial class ManagerInterfaceGenerator
{
    private readonly record struct ManagerInfo(INamedTypeSymbol ManagerSymbol, ManagerInterfaceAttributeInfo ManagerInterfaceInfo, ManagerModelAttributeInfo ManagerModelInfo, ManagerMethodInfo MethodInfo);

    private readonly record struct ManagerMethodInfo(ImmutableArray<IMethodSymbol> Symbols, ImmutableArray<string> FullyQualifiedNames);

    private readonly record struct ManagerInterfaceAttributeInfo(INamedTypeSymbol ManagerInterfaceSymbol, ITypeSymbol InterfaceSymbol);

    private readonly record struct ManagerModelAttributeInfo(INamedTypeSymbol ManagerModelSymbol, ITypeSymbol ModelSymbol);

    /// <summary>
    ///     Verifica si una declaración es válida para generar código.
    /// </summary>
    /// <param name="Node">
    ///     Declaración que se va ha verificar.
    /// </param>
    /// <param name="Token">
    ///     Propagates notification that operations should be canceled.
    /// </param>
    /// <returns>
    ///     Regresa True sí <paramref name="Node"/> es válido para la generación de código.
    /// </returns>
    private static bool SysCreditApiManagerPredicate(SyntaxNode Node, CancellationToken Token)
    {
        return Node is ClassDeclarationSyntax @class && @class.HasManagerInterfaceAttribute() && @class.HasManagerModelAttribute();
    }

    /// <summary>
    ///     Convierte <see cref="GeneratorSyntaxContext.Node"/> en un <see cref="AttributeData"/>.
    /// </summary>
    /// <param name="Context">
    ///     Context passed to an <see cref="ISyntaxContextReceiver"/> when <see cref="ISyntaxContextReceiver.OnVisitSyntaxNode(GeneratorSyntaxContext)"/> is called.
    /// </param>
    /// <param name="Token">
    ///     Propagates notification that operations should be canceled.
    /// </param>
    /// <returns>
    ///     Regresa <see cref="GeneratorSyntaxContext.Node"/> convertido en un <see cref="ManagerInfo"/>.
    /// </returns>
    private static ManagerInfo SysCreditApiManagerTransform(GeneratorSyntaxContext Context, CancellationToken Token)
    {
        var ManagerClass = (ClassDeclarationSyntax)Context.Node;
        var ManagerSymbol = Context.SemanticModel.GetDeclaredSymbol(ManagerClass)!;

        var Attributes = ManagerSymbol.GetAttributes().Select(Attribute => Attribute.AttributeClass!).Where(Attribute => Attribute.Arity == 1).ToImmutableArray();

        var ManagerInterfaceSymbol = Attributes.First(Attribute => Attribute.Name is "ManagerInterface" or "ManagerInterfaceAttribute");
        var InterfaceSymbol = ManagerInterfaceSymbol.TypeArguments.First();

        var ManagerModelSymbol = Attributes.First(Attribute => Attribute.Name is "ManagerModel" or "ManagerModelAttribute");
        var ModelSymbol = ManagerModelSymbol.TypeArguments.First();

        var MethodSymbols = ManagerSymbol.GetMembers()
            .OfType<IMethodSymbol>()
            .Where(Method => Method.HasAttributeWithFullyQualifiedMetadataName(Constants.MethodIdAttribute))
            .ToImmutableArray();
        var MethodFullyQualifiedNames = MethodSymbols.Select(Method => $"{Method.ReturnType.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)} {Method.GetFullyQualifiedName()}({GetMethodParameters(Method)})").ToImmutableArray();

        return new(ManagerSymbol,
            new ManagerInterfaceAttributeInfo(ManagerInterfaceSymbol, InterfaceSymbol),
            new ManagerModelAttributeInfo(ManagerModelSymbol, ModelSymbol),
            new ManagerMethodInfo(MethodSymbols, MethodFullyQualifiedNames));
    }
}
