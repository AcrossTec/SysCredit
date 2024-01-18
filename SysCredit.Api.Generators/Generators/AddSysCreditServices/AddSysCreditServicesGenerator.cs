namespace SysCredit.Api.Generators;

using System.Diagnostics;

using Microsoft.CodeAnalysis;

/// <summary>
///     Genera el método public static partial IServiceCollection AddSysCreditServices(this IServiceCollection Services).
/// </summary>
[Generator(LanguageNames.CSharp)]
public partial class AddSysCreditServicesGenerator : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext Context)
    {
        // #if DEBUG
        //         if (!Debugger.IsAttached) Debugger.Launch();
        // #endif
        var SysCreditApiServices = Context.SyntaxProvider.CreateSyntaxProvider(SysCreditApiServicesPredicate, SysCreditApiServicesTransform);
        Context.RegisterImplementationSourceOutput(SysCreditApiServices.Collect(), EmitSourceCode);
    }
}
