﻿namespace SysCredit.Api.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SysCredit.Toolkits.Generators.Extensions;

using System.Text;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

/// <inheritdoc />
internal partial class ErrorCodeGenerator
{
    /// <summary>
    ///     Genera la clase:
    ///     <code>
    ///         // ------------------------------------------------------------------------------
    ///         //  &lt;auto-generated&gt;                                                       
    ///         //      This code was generated by a tool.                                       
    ///         //      Runtime Version: 1.0.0.0                                                 
    ///         //                                                                               
    ///         //      Changes to this file may cause incorrect behavior and will be lost if    
    ///         //      the code is regenerated.                                                 
    ///         //  &lt;/auto-generated&gt;                                                      
    ///         // ------------------------------------------------------------------------------
    ///
    ///         namespace SysCredit.Api.Constants;
    ///
    ///         /// &lt;summary&gt;
    ///         ///     Códigos de error de SysCredit.Api
    ///         /// &lt;summary&gt;
    ///         public static partial class ErrorCodes
    ///         {
    ///             public const string PREFIX0001 = "PREFIX0001";
    ///         }
    ///     </code>
    /// </summary>
    /// <param name="Context">
    ///     Context passed to an incremental generator when it has registered an output via <see cref="IncrementalGeneratorInitializationContext.RegisterSourceOutput{TSource}(IncrementalValueProvider{TSource}, Action{SourceProductionContext, TSource})"/>.
    /// </param>
    /// <param name="Tuple">
    ///     Objeto que posee los prefijos de error y la categoría que pertenece ademas de los números de código mínimos y máximos.
    /// </param>
    private static void Emit(SourceProductionContext Context, ((string ErrorCodePrefix, string? ErrorCategory) Left, (int MinCodeNumber, int MaxCodeNumber) Right) Tuple)
    {
        CompilationUnitSyntax CompilationUnit = SyntaxFactory.CompilationUnit()
            .WithMembers(
                SingletonList<MemberDeclarationSyntax>(
                    FileScopedNamespaceDeclaration(IdentifierName("SysCredit.Api.Constants"))
                    .WithNamespaceKeyword(
                        Token(
                            TriviaList(
                                Comment("//------------------------------------------------------------------------------"),
                                Comment("// <auto-generated>                                                             "),
                                Comment("//     This code was generated by a tool.                                       "),
                                Comment("//     Runtime Version: 1.0.0.0                                                 "),
                                Comment("//                                                                              "),
                                Comment("//     Changes to this file may cause incorrect behavior and will be lost if    "),
                                Comment("//     the code is regenerated.                                                 "),
                                Comment("// </auto-generated>                                                            "),
                                Comment("//------------------------------------------------------------------------------")),
                            SyntaxKind.NamespaceKeyword,
                            TriviaList()))
                    .WithMembers(
                        SingletonList<MemberDeclarationSyntax>(
                            ClassDeclaration("ErrorCodes")
                            .WithModifiers(
                                TokenList(
                                    Token(
                                        TriviaList(
                                            Comment("/// <summary>"),
                                            Comment("///     Códigos de error de SysCredit.Api"),
                                            Comment("/// <summary>")),
                                        SyntaxKind.PublicKeyword,
                                        TriviaList()),
                                    Token(SyntaxKind.StaticKeyword),
                                    Token(SyntaxKind.PartialKeyword)))
                            .WithMembers(
                                List<MemberDeclarationSyntax>(
                                    GetFieldDeclarationSyntaxes(Tuple.Left, Tuple.Right)))))))
            .NormalizeWhitespace();

        string CSharpText = CompilationUnit.GetText(Encoding.UTF8).ToString();
        Context.AddSource(Helpers.FileName($"ErrorCodesFor{Tuple.Left.ErrorCodePrefix}"), CompilationUnit);
    }

    /// <summary>
    ///     Genera las declaraciones de campos para la clase ErrorCodes.
    /// </summary>
    /// <param name="CodeInfo">
    ///     Prefijos usados por los códigos de error.
    /// </param>
    /// <param name="MinCodeNumber">
    ///     Número de código de error mínimo.
    /// </param>
    /// <param name="MaxCodeNumber">
    ///     Número de código de error máximo.
    /// </param>
    /// <returns>
    ///     Regresa las declaraciones de los campos.
    /// </returns>
    private static IEnumerable<FieldDeclarationSyntax> GetFieldDeclarationSyntaxes((string ErrorCodePrefix, string? ErrorCategory) CodeInfo, (int MinCodeNumber, int MaxCodeNumber) Range)
    {
        // Construct the generated field as follows:
        // public const string <CodePrefix><CodeNumber> = "<CodePrefix><CodeNumber>";

        foreach (var CodeNumber in Enumerable.Range(Range.MinCodeNumber, Range.MaxCodeNumber))
        {
            yield return FieldDeclaration(VariableDeclaration(PredefinedType(Token(SyntaxKind.StringKeyword)))
                            .WithVariables(
                                SingletonSeparatedList(
                                    VariableDeclarator($"{CodeInfo.ErrorCodePrefix}{CodeNumber,4:D4}")
                                        .WithInitializer(EqualsValueClause(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal($"{CodeInfo.ErrorCodePrefix}{CodeNumber,4:D4}")))))))
                            .WithModifiers(TokenList(
                                Token(
                                    TriviaList(
                                        Comment($"/// <summary>"),
                                        Comment($"///     Categoría {CodeInfo.ErrorCategory ?? "\"Sin Categoría\""}: {CodeInfo.ErrorCodePrefix}{CodeNumber,4:D4}"),
                                        Comment($"/// <summary>")),
                                    SyntaxKind.PublicKeyword,
                                    TriviaList()),
                                Token(SyntaxKind.ConstKeyword)))
                            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }
    }
}
