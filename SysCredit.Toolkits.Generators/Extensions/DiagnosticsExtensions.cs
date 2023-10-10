namespace SysCredit.Toolkits.Generators.Extensions;

using Microsoft.CodeAnalysis;

using SysCredit.Toolkits.Generators.Helpers;
using SysCredit.Toolkits.Generators.Models;

/// <summary>
///     Extension methods for <see cref="GeneratorExecutionContext"/>, specifically for reporting diagnostics.
/// </summary>
public static class DiagnosticsExtensions
{
    /// <summary>
    ///     Adds a new diagnostics to the target builder.
    /// </summary>
    /// <param name="Diagnostics">
    ///     The collection of produced <see cref="DiagnosticInfo"/> instances.
    /// </param>
    /// <param name="Descriptor">
    ///     The input <see cref="DiagnosticDescriptor"/> for the diagnostics to create.
    /// </param>
    /// <param name="Symbol">
    ///     The source <see cref="ISymbol"/> to attach the diagnostics to.
    /// </param>
    /// <param name="Args">
    ///     The optional arguments for the formatted message to include.
    /// </param>
    public static void Add(this in ImmutableArrayBuilder<DiagnosticInfo> Diagnostics, DiagnosticDescriptor Descriptor, ISymbol Symbol, params object[] Args)
    {
        Diagnostics.Add(DiagnosticInfo.Create(Descriptor, Symbol, Args));
    }

    /// <summary>
    ///     Adds a new diagnostics to the target builder.
    /// </summary>
    /// <param name="Diagnostics">
    ///     The collection of produced <see cref="DiagnosticInfo"/> instances.
    /// </param>
    /// <param name="Descriptor">
    ///     The input <see cref="DiagnosticDescriptor"/> for the diagnostics to create.
    /// </param>
    /// <param name="Node">
    ///     The source <see cref="SyntaxNode"/> to attach the diagnostics to.
    /// </param>
    /// <param name="Args">
    ///     The optional arguments for the formatted message to include.
    /// </param>
    public static void Add(this in ImmutableArrayBuilder<DiagnosticInfo> Diagnostics, DiagnosticDescriptor Descriptor, SyntaxNode Node, params object[] Args)
    {
        Diagnostics.Add(DiagnosticInfo.Create(Descriptor, Node, Args));
    }

    /// <summary>
    ///     Registers an output node into an <see cref="IncrementalGeneratorInitializationContext"/> to output diagnostics.
    /// </summary>
    /// <param name="Context">
    ///     The input <see cref="IncrementalGeneratorInitializationContext"/> instance.
    /// </param>
    /// <param name="Diagnostics">
    ///     The input <see cref="IncrementalValuesProvider{TValues}"/> sequence of diagnostics.
    /// </param>
    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext Context, IncrementalValuesProvider<DiagnosticInfo> Diagnostics)
    {
        Context.RegisterSourceOutput(Diagnostics, static (Context, Diagnostic) =>
        {
            Context.ReportDiagnostic(Diagnostic.ToDiagnostic());
        });
    }

    /// <summary>
    ///     Registers an output node into an <see cref="IncrementalGeneratorInitializationContext"/> to output diagnostics.
    /// </summary>
    /// <param name="Context">
    ///     The input <see cref="IncrementalGeneratorInitializationContext"/> instance.
    /// </param>
    /// <param name="Diagnostics">
    ///     The input <see cref="IncrementalValuesProvider{TValues}"/> sequence of diagnostics.
    /// </param>
    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext Context, IncrementalValuesProvider<EquatableArray<DiagnosticInfo>> Diagnostics)
    {
        Context.RegisterSourceOutput(Diagnostics, static (context, diagnostics) =>
        {
            foreach (DiagnosticInfo diagnostic in diagnostics)
            {
                context.ReportDiagnostic(diagnostic.ToDiagnostic());
            }
        });
    }
}
