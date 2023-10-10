namespace SysCredit.Toolkits.Generators.Extensions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Text;

/// <summary>
///     Extension methods for the <see cref="SourceProductionContext"/> type.
/// </summary>
public static class SourceProductionContextExtensions
{
    /// <summary>
    ///     Adds a new source file to a target <see cref="SourceProductionContext"/> instance.
    /// </summary>
    /// <param name="Context">
    ///     The input <see cref="SourceProductionContext"/> instance to use.
    /// </param>
    /// <param name="Name">
    ///     The name of the source file to add.
    /// </param>
    /// <param name="CompilationUnit">
    ///     The <see cref="CompilationUnitSyntax"/> instance representing the syntax tree to add.
    /// </param>
    public static void AddSource(this SourceProductionContext Context, string Name, CompilationUnitSyntax CompilationUnit)
    {
#if !ROSLYN_4_3_1_OR_GREATER
        // We're fine with the extra allocation in the few cases where adjusting the filename is necessary.
        // This will only ever be done when code generation is executed again anyway, which is a slow path.
        Name = Name.Replace('+', '.').Replace('`', '_');
#endif

        // Add the UTF8 text for the input compilation unit
        Context.AddSource(Name, CompilationUnit.GetText(Encoding.UTF8));
    }
}
