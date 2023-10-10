namespace SysCredit.Toolkits.Generators.Extensions;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Immutable;

/// <summary>
///     Extension methods for <see cref="IncrementalGeneratorInitializationContext"/>.
/// </summary>
public static class IncrementalGeneratorInitializationContextExtensions
{
    /// <summary>
    ///     Conditionally invokes <see cref="IncrementalGeneratorInitializationContext.RegisterSourceOutput{TSource}(IncrementalValueProvider{TSource}, Action{SourceProductionContext, TSource})"/>
    ///     if the value produced by the input <see cref="IncrementalValueProvider{TValue}"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="Context">
    ///     The input <see cref="IncrementalGeneratorInitializationContext"/> value being used.
    /// </param>
    /// <param name="Source">
    ///     The source <see cref="IncrementalValueProvider{TValues}"/> instance.
    /// </param>
    /// <param name="Action">
    ///     The conditional <see cref="Action"/> to invoke.
    /// </param>
    public static void RegisterConditionalSourceOutput(this IncrementalGeneratorInitializationContext Context, IncrementalValueProvider<bool> Source, Action<SourceProductionContext> Action)
    {
        Context.RegisterSourceOutput(Source, (Context, Condition) =>
        {
            if (Condition)
            {
                Action(Context);
            }
        });
    }

    /// <summary>
    ///     Conditionally invokes <see cref="IncrementalGeneratorInitializationContext.RegisterImplementationSourceOutput{TSource}(IncrementalValueProvider{TSource}, Action{SourceProductionContext, TSource})"/>
    ///     if the value produced by the input <see cref="IncrementalValueProvider{TValue}"/> is <see langword="true"/>, and also supplying a given input state.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of state to pass to the source production callback to invoke.
    /// </typeparam>
    /// <param name="Context">
    ///     The input <see cref="IncrementalGeneratorInitializationContext"/> value being used.
    /// </param>
    /// <param name="Source">
    ///     The source <see cref="IncrementalValueProvider{TValues}"/> instance.
    /// </param>
    /// <param name="Action">
    ///     The conditional <see cref="Action{T}"/> to invoke.
    /// </param>
    public static void RegisterConditionalImplementationSourceOutput<T>(this IncrementalGeneratorInitializationContext Context, IncrementalValueProvider<(bool Condition, T State)> Source, Action<SourceProductionContext, T> Action)
    {
        Context.RegisterImplementationSourceOutput(Source, (Context, Item) =>
        {
            if (Item.Condition)
            {
                Action(Context, Item.State);
            }
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
    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext Context, IncrementalValuesProvider<ImmutableArray<Diagnostic>> Diagnostics)
    {
        Context.RegisterSourceOutput(Diagnostics, static (Context, Diagnostics) =>
        {
            foreach (Diagnostic Diagnostic in Diagnostics)
            {
                Context.ReportDiagnostic(Diagnostic);
            }
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
    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext Context, IncrementalValuesProvider<Diagnostic> Diagnostics)
    {
        Context.RegisterSourceOutput(Diagnostics, static (Context, Diagnostic) => Context.ReportDiagnostic(Diagnostic));
    }
}
