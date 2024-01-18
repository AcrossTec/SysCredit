namespace SysCredit.Api.Generators;

using System.Diagnostics;

using Microsoft.CodeAnalysis;

/// <summary>
///    Genera la inferfaz con todos los métodos de un Servicio marcados con un MethodIdAttribute.
/// </summary>
[Generator(LanguageNames.CSharp)]
public partial class ManagerInterfaceGenerator : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext Context)
    {
#if DEBUG
        if (!Debugger.IsAttached) Debugger.Launch();
#endif
        var ManagerInfoProviders = Context.SyntaxProvider.CreateSyntaxProvider(SysCreditApiManagerPredicate, SysCreditApiManagerTransform);
        Context.RegisterImplementationSourceOutput(ManagerInfoProviders, EmitSourceCode);
    }
}
