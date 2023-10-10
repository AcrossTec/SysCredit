namespace SysCredit.Api.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class InterfaceServiceGenerator
{
    internal partial class Parser
    {
        internal static bool IsSyntaxTargetForGeneration(SyntaxNode Node)
            => Node is ClassDeclarationSyntax Class && Class.AttributeLists.Count > 0;
    }
}
