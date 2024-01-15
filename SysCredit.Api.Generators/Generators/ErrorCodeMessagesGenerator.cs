namespace SysCredit.Api.Generators;

using Microsoft.CodeAnalysis;

/// <summary>
///     Generador de código para los mensajes de error.
/// </summary>
[Generator(LanguageNames.CSharp)]
public class ErrorCodeMessagesGenerator : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext Context)
    {
    }
}
