namespace SysCredit.Api.Generators;

using System.Collections.Immutable;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SysCredit.Toolkits.Generators.Extensions;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

/// <summary>
/// 
/// </summary>
public partial class AddSysCreditServicesGenerator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Node"></param>
    /// <param name="Token"></param>
    /// <returns></returns>
    private bool SysCreditApiServicesPredicate(SyntaxNode Node, CancellationToken Token)
    {
        return Node is ClassDeclarationSyntax @class && @class.HasServiceAttribute() && @class.HasServiceModelAttribute();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Context"></param>
    /// <param name="CancellationToken"></param>
    /// <returns></returns>
    private StatementSyntax SysCreditApiServicesTransform(GeneratorSyntaxContext Context, CancellationToken CancellationToken)
    {
        var ServiceClass = (ClassDeclarationSyntax)Context.Node;
        var ServiceSymbol = Context.SemanticModel.GetDeclaredSymbol(ServiceClass)!;
        var TypeArgumentSymbols = ServiceSymbol.GetAttributes().Where(Attribute => Attribute.AttributeClass!.Arity == 1).SelectMany(Attribute => Attribute.AttributeClass!.TypeArguments).ToImmutableArray();
        var ModelSymbol = TypeArgumentSymbols.First(TypeArgument => TypeArgument.HasInterfaceWithFullyQualifiedMetadataName("SysCredit.Models.IModel"));

        var Statement = ExpressionStatement(
                            InvocationExpression(
                                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("Services"),
                                    GenericName("AddScoped").WithTypeArgumentList(TypeArgumentList(SeparatedList<TypeSyntax>(NodeOrTokenList(
                                        GenericName("SysCredit.Api.Interfaces.IService").WithTypeArgumentList(TypeArgumentList(SingletonSeparatedList<TypeSyntax>(
                                            IdentifierName(ModelSymbol.ToDisplayString())))),
                                        Token(TriviaList(), SyntaxKind.CommaToken, TriviaList(Space)),
                                        IdentifierName(ServiceSymbol.ToDisplayString()))))))));

        string ServiceSymbolString = ServiceSymbol.ToDisplayString();
        string ModelSymbolString = ModelSymbol.ToDisplayString();
        string StatementString = Statement.GetText(Encoding.UTF8).ToString();

        return Statement;
    }
}
