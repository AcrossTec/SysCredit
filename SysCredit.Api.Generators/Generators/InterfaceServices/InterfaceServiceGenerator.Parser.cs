namespace SysCredit.Api.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SysCredit.Toolkits.Generators.Extensions;

using System.Collections.Immutable;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

/// <inheritdoc />
public partial class InterfaceServiceGenerator
{
    /// <summary>
    ///     Tiene información detallada sobre los datos de una clase para crear su interfaz.
    /// </summary>
    /// <param name="InterfaceName">
    ///     Nombre de la interfaz que se creará.
    /// </param>
    /// <param name="ModelName">
    ///     Nombre del modelo de la interfaz base usada por la interfaz autogenerada.
    /// </param>
    /// <param name="ClassDeclaration">
    ///     Información de la clase que declara el atributo del servicio.
    /// </param>
    /// <param name="Usings">
    ///     Espacios de nombres usados por la clase de servicio.
    /// </param>
    /// <param name="MethodDeclarations">
    ///     Métodos usados por la clase de servicio.
    /// </param>
    private record struct InterfaceServiceInfo(string InterfaceName, string ModelName, ClassDeclarationSyntax ClassDeclaration, ImmutableArray<UsingDirectiveSyntax> Usings, ImmutableArray<MethodDeclarationSyntax> MethodDeclarations);

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
    private static bool SysCreditApiServicePredicate(SyntaxNode Node, CancellationToken Token)
    {
        return Node is ClassDeclarationSyntax @class && @class.HasServiceAttribute() && @class.HasServiceModelAttribute();
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
    ///     Regresa <see cref="GeneratorSyntaxContext.Node"/> convertido en un <see cref="InterfaceServiceInfo"/>.
    /// </returns>
    private static InterfaceServiceInfo SysCreditApiServiceTransform(GeneratorSyntaxContext Context, CancellationToken Token)
    {
        var ClassDeclaration = (ClassDeclarationSyntax)Context.Node;

        var UsingDirectives = GetUsingDirectives(ClassDeclaration);

        var MethodDeclarations = from MethodDeclaration in ClassDeclaration.Members.OfType<MethodDeclarationSyntax>()
                                 where MethodDeclaration.HasAttribute("MethodId")
                                    && MethodDeclaration.Modifiers.Any(Modifier => Modifier.IsKind(SyntaxKind.PublicKeyword) && !Modifier.IsKind(SyntaxKind.StaticKeyword))
                                 select MethodDeclaration.WithBody(default)
                                                         .WithExpressionBody(default)
                                                         .WithAttributeLists(List<AttributeListSyntax>())
                                                         .WithSemicolonToken(SyntaxFactory.Token(
                                                             TriviaList(),
                                                             SyntaxKind.SemicolonToken,
                                                             TriviaList(Comment(Constants.NewLine))))
                                                         .RemoveModifier(SyntaxKind.AsyncKeyword)
                                                         .RemoveModifier(SyntaxKind.PublicKeyword)
                                                         .RemoveModifier(SyntaxKind.PartialKeyword)
                                                         .As<MethodDeclarationSyntax>();

        var NameSyntaxes = ClassDeclaration.GetGenericNameSyntaxFromAttributes(Arity: 1);

        // TODO: Verificar que tipo correcto de los argumentos: IdentifierNameSyntax, QualifiedNameSyntax, etc...

        var ServiceNameSyntax = NameSyntaxes.First(NameSyntax => NameSyntax.Identifier.Text == "Service");
        var InterfaceName = ServiceNameSyntax.TypeArgumentList.Arguments.OfType<IdentifierNameSyntax>().First();

        var ServiceModelNameSyntax = NameSyntaxes.FirstOrDefault(Attribute => Attribute.Identifier.Text == "ServiceModel");
        var ModelName = ServiceModelNameSyntax?.TypeArgumentList.Arguments.OfType<IdentifierNameSyntax>().First() ?? IdentifierName("SysCredit.Models.Entity");

        return new(
            InterfaceName.Identifier.Text,
            ModelName.Identifier.Text,
            ClassDeclaration.WithParameterList(default)
                            .WithAttributeLists(List<AttributeListSyntax>())
                            .WithBaseList(BaseList(SingletonSeparatedList<BaseTypeSyntax>(SimpleBaseType(IdentifierName(InterfaceName.Identifier.Text)))))
                            .WithMembers(List<MemberDeclarationSyntax>())
                            .WithOpenBraceToken(SyntaxFactory.Token(SyntaxKind.OpenBraceToken))
                            .WithCloseBraceToken(SyntaxFactory.Token(SyntaxKind.CloseBraceToken)),
            UsingDirectives,
            MethodDeclarations.ToImmutableArray());
    }

    /// <summary>
    ///     Obtiene todos los espacios de nombres de donde esté declarada la clase.
    /// </summary>
    /// <param name="class">
    ///     Declaración de la clase que será analizada para obtener todos sus espacios de nombres.
    /// </param>
    /// <returns>
    ///     Regresa todos los espacios de nombres usados por <paramref name="class"/>.
    /// </returns>
    private static ImmutableArray<UsingDirectiveSyntax> GetUsingDirectives(ClassDeclarationSyntax @class)
    {
        BaseNamespaceDeclarationSyntax? @namespace = @class.GetNamespaceDeclarationSyntax();
        return @namespace?.Usings.ToImmutableArray() ?? ImmutableArray<UsingDirectiveSyntax>.Empty;
    }
}
