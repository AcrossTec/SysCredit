namespace SysCredit.Api.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using System.Linq;
using System.Text;
using System.Collections.Immutable;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

/// <summary>
///     Un generador de código para generar una inferfaz con todos los métodos de un Servicio marcados con un MethodIdAttribute.
/// </summary>
[Generator(LanguageNames.CSharp)]
public partial class InterfaceServiceGenerator : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext Context)
    {
    }
}
