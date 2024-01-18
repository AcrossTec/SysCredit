namespace SysCredit.Api.Generators;

using System.Linq;

using Microsoft.CodeAnalysis;

using SysCredit.Toolkits.Generators.Extensions;

/// <summary>
///     Generador del Api estandar de los códigos de errores para SysCredit.Api.
/// </summary>
[Generator(LanguageNames.CSharp)]
internal partial class ErrorCodeGenerator : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext Context)
    {
        // #if DEBUG
        //         if (!Debugger.IsAttached) Debugger.Launch();
        // #endif

        Context.RegisterPostInitializationOutput(GenerateErrorCodeAttributes);
        var ErrorCodeRangeProvider = Context.CompilationProvider.Select(SelectErrorCodeRange);
        var ErrorCodePrefixesProvider = Context.SyntaxProvider
            .CreateSyntaxProvider(IsSyntaxTargetForGeneration, GetSemanticTargetForGeneration)
            .Where(static Ecp => Ecp.ErrorCodePrefix is not null)
            .Distinct()
            .Select(SelectErrorCodePrefix)
            .Combine(ErrorCodeRangeProvider);

        Context.RegisterImplementationSourceOutput(ErrorCodePrefixesProvider, Emit);
    }
}
