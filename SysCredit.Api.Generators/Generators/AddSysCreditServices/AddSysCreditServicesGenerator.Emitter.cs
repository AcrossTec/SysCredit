﻿namespace SysCredit.Api.Generators;

using System.Collections.Immutable;
using System.Linq;
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
    /// <param name="Context"></param>
    /// <param name="ExpressionStatementSyntaxes"></param>
    private void EmitSourceCode(SourceProductionContext Context, ImmutableArray<StatementSyntax> ExpressionStatementSyntaxes)
    {
        CompilationUnitSyntax CompilationUnit = SyntaxFactory.CompilationUnit()
            .WithMembers(SingletonList<MemberDeclarationSyntax>(
                FileScopedNamespaceDeclaration(IdentifierName("SysCredit.Api.Extensions"))
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
                    .WithUsings(SingletonList(UsingDirective(IdentifierName("Microsoft.Extensions.DependencyInjection"))))
                    .WithMembers(SingletonList<MemberDeclarationSyntax>(
                        ClassDeclaration("WebApplicationBuilderExtensions")
                            .WithModifiers(TokenList(
                                Token(SyntaxKind.PublicKeyword),
                                Token(SyntaxKind.StaticKeyword),
                                Token(SyntaxKind.PartialKeyword)))
                            .WithMembers(SingletonList<MemberDeclarationSyntax>(
                                MethodDeclaration(IdentifierName("IServiceCollection"), Identifier("AddSysCreditServices"))
                                    .WithModifiers(TokenList(
                                        Token(SyntaxKind.PublicKeyword),
                                        Token(SyntaxKind.StaticKeyword),
                                        Token(SyntaxKind.PartialKeyword)))
                                    .WithParameterList(ParameterList(SingletonSeparatedList<ParameterSyntax>(
                                        Parameter(Identifier("Services"))
                                            .WithModifiers(TokenList(Token(SyntaxKind.ThisKeyword)))
                                            .WithType(IdentifierName("IServiceCollection")))))
                                    .WithBody(Block(ExpressionStatementSyntaxes.Concat([ReturnStatement(IdentifierName("Services"))])))))))))
            .NormalizeWhitespace();
        string CSharpText = CompilationUnit.GetText(Encoding.UTF8).ToString();
        Context.AddSource(Helpers.FileName($"SysCredit.Api.Extensions.WebApplicationBuilderExtensions"), CompilationUnit);
    }
}
