namespace SysCredit.Api.Extensions;

using SysCredit.Api.Stores;

using SysCredit.Models;

/// <summary>
/// 
/// </summary>
public static class GeneralExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="object"></param>
    /// <returns></returns>
    public static TObject? As<TObject>(this object? @object) => (TObject?)@object;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="object"></param>
    /// <returns></returns>
    public static IStore AsStore(this object @object) => @object.As<IStore>()!;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="object"></param>
    /// <returns></returns>
    public static IStore<T> AsStore<T>(this object @object) where T : Entity => @object.As<IStore<T>>()!;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    public static string? EscapedLike(this string? Value)
    {
        Value = Value?.Replace("[", "[[]");
        Value = Value?.Replace("%", "[%]");
        Value = Value?.Replace("_", "[_]");
        return Value;
    }
}
