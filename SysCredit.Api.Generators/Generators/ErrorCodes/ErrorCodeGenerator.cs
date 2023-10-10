namespace SysCredit.Api.Generators;

using Microsoft.CodeAnalysis;

using SysCredit.Toolkits.Generators.Extensions;

using System.Diagnostics;
using System.Linq;

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
        //         if (!Debugger.IsAttached)
        //         {
        //             Debugger.Launch();
        //         }
        // #endif 

        Context.RegisterPostInitializationOutput(AddErrorCodeAttributes);

        //Context.SyntaxProvider.ForAttributeWithMetadataName()

        var ErrorCodePrefixesProvider = Context.SyntaxProvider
            .CreateSyntaxProvider(IsSyntaxTargetForGeneration, GetSemanticTargetForGeneration)
            .Where(Helpers.WhereNotNull)
            .Distinct()
            .Select(SelectErrorCodePrefix)
            .Collect();

        Context.RegisterImplementationSourceOutput(ErrorCodePrefixesProvider, Emit);
    }
}
