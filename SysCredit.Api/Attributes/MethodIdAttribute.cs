namespace SysCredit.Api.Attributes;

/// <summary>
///     Establece un código único para un método.
/// </summary>
/// <param name="MethodId">
///     Código único que será usado para algún método.
/// </param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class MethodIdAttribute(string MethodId) : Attribute
{
    /// <summary>
    ///     Código único usado por el método.
    /// </summary>
    public readonly string MethodId = MethodId;
}
