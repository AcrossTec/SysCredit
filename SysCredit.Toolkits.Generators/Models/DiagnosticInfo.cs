namespace SysCredit.Toolkits.Generators.Models;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SysCredit.Toolkits.Generators.Helpers;

using System.Collections.Immutable;
using System.Linq;

/// <summary>
///     A model for a serializeable diagnostic info.
/// </summary>
/// <param name="Descriptor">
///     The wrapped <see cref="DiagnosticDescriptor"/> instance.
/// </param>
/// <param name="SyntaxTree">
///     The tree to use as location for the diagnostic, if available.
/// </param>
/// <param name="TextSpan">
///     The span to use as location for the diagnostic.
/// </param>
/// <param name="Arguments">
///     The diagnostic arguments.
/// </param>
public sealed record DiagnosticInfo(DiagnosticDescriptor Descriptor, SyntaxTree? SyntaxTree, TextSpan TextSpan, EquatableArray<string> Arguments)
{
    /// <summary>
    ///     Creates a new <see cref="Diagnostic"/> instance with the state from this model.
    /// </summary>
    /// <returns>
    ///     A new <see cref="Diagnostic"/> instance with the state from this model.
    /// </returns>
    public Diagnostic ToDiagnostic()
    {
        if (SyntaxTree is not null)
        {
            return Diagnostic.Create(Descriptor, Location.Create(SyntaxTree, TextSpan), Arguments.ToArray());
        }

        return Diagnostic.Create(Descriptor, null, Arguments.ToArray());
    }

    /// <summary>
    ///     Creates a new <see cref="DiagnosticInfo"/> instance with the specified parameters.
    /// </summary>
    /// <param name="Descriptor">
    ///     The input <see cref="DiagnosticDescriptor"/> for the diagnostics to create.
    /// </param>
    /// <param name="Symbol">
    ///     The source <see cref="ISymbol"/> to attach the diagnostics to.
    /// </param>
    /// <param name="Args">
    ///     The optional arguments for the formatted message to include.
    /// </param>
    /// <returns>
    ///     A new <see cref="DiagnosticInfo"/> instance with the specified parameters.
    /// </returns>
    public static DiagnosticInfo Create(DiagnosticDescriptor Descriptor, ISymbol Symbol, params object[] Args)
    {
        Location Location = Symbol.Locations.First();
        return new(Descriptor, Location.SourceTree, Location.SourceSpan, Args.Select(static Arg => Arg.ToString()).ToImmutableArray());
    }

    /// <summary>
    ///     Creates a new <see cref="DiagnosticInfo"/> instance with the specified parameters.
    /// </summary>
    /// <param name="Descriptor">
    ///     The input <see cref="DiagnosticDescriptor"/> for the diagnostics to create.
    /// </param>
    /// <param name="Node">
    ///     The source <see cref="SyntaxNode"/> to attach the diagnostics to.
    /// </param>
    /// <param name="Args">
    ///     The optional arguments for the formatted message to include.
    /// </param>
    /// <returns>
    ///     A new <see cref="DiagnosticInfo"/> instance with the specified parameters.
    /// </returns>
    public static DiagnosticInfo Create(DiagnosticDescriptor Descriptor, SyntaxNode Node, params object[] Args)
    {
        Location Location = Node.GetLocation();
        return new(Descriptor, Location.SourceTree, Location.SourceSpan, Args.Select(static Arg => Arg.ToString()).ToImmutableArray());
    }
}
