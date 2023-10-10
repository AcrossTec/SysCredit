namespace SysCredit.Api.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SysCredit.Toolkits.Generators.Extensions;

/// <inheritdoc />
internal partial class ErrorCodeGenerator
{
    /// <summary>
    ///     Verifica si una declaración es válida para generar código.
    /// </summary>
    /// <param name="Node">
    ///     Declaración que se va ha verificar.
    /// </param>
    /// <param name="Token">
    ///     Propagates notification that operations should be canceled.
    /// </param>
    /// <returns>
    ///     Regresa True sí <paramref name="Node"/> es válido para la generación de código.
    /// </returns>
    private static bool IsSyntaxTargetForGeneration(SyntaxNode Node, CancellationToken Token)
    {
        return Node is ClassDeclarationSyntax ClassDeclaration && ClassDeclaration.HasOrPotentiallyHasAttributes();
    }

    /// <summary>
    ///     Convierte <see cref="GeneratorSyntaxContext.Node"/> en un <see cref="AttributeData"/>.
    /// </summary>
    /// <param name="Context">
    ///     Context passed to an <see cref="ISyntaxContextReceiver"/> when <see cref="ISyntaxContextReceiver.OnVisitSyntaxNode(GeneratorSyntaxContext)"/> is called.
    /// </param>
    /// <param name="Token">
    ///     Propagates notification that operations should be canceled.
    /// </param>
    /// <returns>
    ///     Regresa <see cref="GeneratorSyntaxContext.Node"/> convertido en un <see cref="AttributeData"/>.
    /// </returns>
    private static (AttributeData? ErrorCodePrefix, AttributeData? ErrorCategory) GetSemanticTargetForGeneration(GeneratorSyntaxContext Context, CancellationToken Token)
    {
        var ClassSymbol = Context.SemanticModel.GetDeclaredSymbol((ClassDeclarationSyntax)Context.Node)!;
        var ErrorCodePrefixSymbol = Context.SemanticModel.Compilation.GetErrorCodePrefixAttributeMetadataName();
        var ErrorCategoryAttributeSymbol = Context.SemanticModel.Compilation.GetErrorCategoryAttributeMetadataName();
        return (ClassSymbol.GetAttributeIfExists(ErrorCodePrefixSymbol), ClassSymbol.GetAttributeIfExists(ErrorCategoryAttributeSymbol));
    }

    /// <summary>
    ///     Obtiene el valor del primer argumento del constructor de <paramref name="Attribute"/>.
    /// </summary>
    /// <param name="Value">
    ///     Atributo del que se obtendrá el primer valor de su constructor.
    /// </param>
    /// <param name="Token">
    ///     Propagates notification that operations should be canceled.
    /// </param>
    /// <returns>
    ///     Regresa el valor del primer argumento del constructor de <paramref name="Attribute"/> transformado en un String.
    /// </returns>
    private static (string ErrorCodePrefix, string? ErrorCategory) SelectErrorCodePrefix((AttributeData? ErrorCodePrefix, AttributeData? ErrorCategory) Value, CancellationToken Token)
    {
        return ((string)Value.ErrorCodePrefix!.ConstructorArguments[0].Value!, (string?)Value.ErrorCategory?.ConstructorArguments[0].Value);
    }

    /// <summary>
    ///     Busca el atributo ErrorCodeRangeAttribute y regresa su valor mínimo y máximo.
    /// </summary>
    /// <param name="Compilation">
    ///     The compilation object is an immutable representation of a single invocation of the compiler.
    ///     Although immutable, a compilation is also on-demand, and will realize and cache data as necessary.
    ///     A compilation can produce a new compilation from existing compilation with the application of small deltas.
    ///     In many cases, it is more efficient than creating a new compilation from scratch, as the new compilation can reuse information from the old compilation.
    /// </param>
    /// <param name="Token">
    ///     Propagates notification that operations should be canceled.
    /// </param>
    /// <returns>
    ///     Regresa el valor mímimo y máximo del atributo ErrorCodeRangeAttribute sólo si existe, sino los valores por defectos configurados. 
    /// </returns>
    private static (int MinCodeNumber, int MaxCodeNumber) SelectErrorCodeRange(Compilation Compilation, CancellationToken Token)
    {
        var ErrorCodeRangeAttributeSymbol = Compilation.GetErrorCodeRangeAttributeMetadataName();
        var Attribute = Compilation.Assembly.GetAttributes().GetAttributeIfExists(ErrorCodeRangeAttributeSymbol);

        if (Attribute is { ConstructorArguments: [TypedConstant MinCodeNumber, TypedConstant MaxCodeNumber] })
        {
            return ((int)MinCodeNumber.Value!, (int)MaxCodeNumber.Value!);
        }

        return (Constants.MinCodeNumber, Constants.MaxCodeNumber);
    }
}
