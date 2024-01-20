using Microsoft.CodeAnalysis;

/// <summary>
/// 
/// </summary>
public static class SyntaxTreeExtensions
{
    /// <summary>
    ///     Utilize the same logic used by the interceptors API for resolving the source mapped value of a path.
    ///     https://github.com/dotnet/roslyn/blob/f290437fcc75dad50a38c09e0977cce13a64f5ba/src/Compilers/CSharp/Portable/Compilation/CSharpCompilation.cs#L1063-L1064
    /// </summary>
    /// <param name="Tree"></param>
    /// <param name="Resolver"></param>
    /// <returns></returns>
    public static string GetInterceptorFilePath(this SyntaxTree Tree, SourceReferenceResolver? Resolver)
        => Resolver?.NormalizePath(Tree.FilePath, baseFilePath: null) ?? Tree.FilePath;
}
