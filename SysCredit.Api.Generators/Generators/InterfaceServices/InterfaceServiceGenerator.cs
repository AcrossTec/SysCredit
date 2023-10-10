namespace SysCredit.Api.Generators;

using Microsoft.CodeAnalysis;

using System;
using System.Diagnostics;

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
        //  if (!Debugger.IsAttached)
        //  {
        //      Debugger.Launch();
        //  }
        // #endif

        var ServiceInfoProvider = Context.SyntaxProvider.CreateSyntaxProvider(IsSyntaxTargetForGeneration, GetSemanticTargetForGeneration);
        Context.RegisterImplementationSourceOutput(ServiceInfoProvider, Emit);
    }
}
