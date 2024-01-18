namespace SysCredit.Api.Generators;

using Microsoft.CodeAnalysis;

/// <summary>
///    Genera la inferfaz con todos los métodos de un Servicio marcados con un MethodIdAttribute.
/// </summary>
[Generator(LanguageNames.CSharp)]
public partial class InterfaceServiceGenerator : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext Context)
    {
        // #if DEBUG
        //         if (!Debugger.IsAttached) Debugger.Launch();
        // #endif

        var ServiceInfoProvider = Context.SyntaxProvider.CreateSyntaxProvider(SysCreditApiServicePredicate, SysCreditApiServiceTransform);
        Context.RegisterImplementationSourceOutput(ServiceInfoProvider, EmitSourceCode);
    }
}
