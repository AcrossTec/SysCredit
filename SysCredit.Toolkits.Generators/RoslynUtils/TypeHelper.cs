namespace System.Runtime.CompilerServices;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

/// <summary>
/// 
/// </summary>
public static class TypeHelper
{
    /// <summary>
    ///     Checks to see if a given type is compiler generated.
    ///
    ///     <remarks>
    ///         The compiler will annotate either the target type or the declaring type with the CompilerGenerated attribute.
    ///         We walk up the declaring types until we find a CompilerGenerated attribute or declare the type as not compiler generated otherwise.
    ///     </remarks>
    /// </summary>
    /// <param name="Type">
    ///     The type to evaluate.
    /// </param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="Type"/> is compiler generated.
    /// </returns>
    internal static bool IsCompilerGeneratedType(Type? Type = null)
    {
        if (Type is not null)
        {
            return Attribute.IsDefined(Type, typeof(CompilerGeneratedAttribute)) || IsCompilerGeneratedType(Type.DeclaringType);
        }

        return false;
    }

    /// <summary>
    ///     Checks to see if a given method is compiler generated.
    /// </summary>
    /// <param name="Method">
    ///     The method to evaluate.
    /// </param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="Method"/> is compiler generated.
    /// </returns>
    private static bool IsCompilerGeneratedMethod(MethodInfo Method)
    {
        return Attribute.IsDefined(Method, typeof(CompilerGeneratedAttribute)) || IsCompilerGeneratedType(Method.DeclaringType);
    }

    /// <summary>
    ///     Parses generated local function name out of a generated method name.
    ///     This code is a stop-gap and exists to address the issues with extracting original method names from generated local functions.
    ///     See https://github.com/dotnet/roslyn/issues/55651 for more info.
    /// </summary>
    private static bool TryParseLocalFunctionName(string GeneratedName, [NotNullWhen(true)] out string? OriginalName)
    {
        OriginalName = null;

        var StartIndex = GeneratedName.LastIndexOf(">g__", StringComparison.Ordinal);
        var EndIndex = GeneratedName.LastIndexOf("|", StringComparison.Ordinal);

        if (StartIndex >= 0 && EndIndex >= 0 && EndIndex - StartIndex > 4)
        {
            OriginalName = GeneratedName.Substring(StartIndex + 4, EndIndex - StartIndex - 4);
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Tries to get non-compiler-generated name of function. This parses generated local function names out of a generated method name if possible.
    /// </summary>
    internal static bool TryGetNonCompilerGeneratedMethodName(MethodInfo Method, [NotNullWhen(true)] out string? OriginalName)
    {
        var MethodName = Method.Name;

        if (!IsCompilerGeneratedMethod(Method))
        {
            OriginalName = MethodName;
            return true;
        }

        return TryParseLocalFunctionName(MethodName, out OriginalName);
    }
}
