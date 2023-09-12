namespace SysCredit.Api.Properties;

using System.Globalization;
using System.Resources;

/// <summary>
///   A strongly-typed resource class, for looking up localized strings, etc.
/// </summary>
public class ErrorCodeMessages
{
    /// <summary>
    ///   Returns the cached ResourceManager instance used by this class.
    /// </summary>
    static ErrorCodeMessages() => ResourceManager = new($"SysCredit.Api.Properties.{nameof(ErrorCodeMessages)}", typeof(ErrorCodeMessages).Assembly);

    /// <summary>
    ///   Returns the cached ResourceManager instance used by this class.
    /// </summary>
    public static ResourceManager ResourceManager { get; }

    /// <summary>
    ///   Overrides the current thread's CurrentUICulture property for all
    ///   resource lookups using this strongly typed resource class.
    /// </summary>
    public static CultureInfo? Culture { get; set; }

    /// <summary>
    ///     Looks up a localized string.
    /// </summary>
    /// <param name="ErrorCode">
    ///     ErrorCode used as key value.
    /// </param>
    /// <returns>
    ///     Return looks up a localized string .
    /// </returns>
    public static string? GetMessage(string ErrorCode) => ResourceManager.GetString(ErrorCode, Culture)!;

    /// <summary>
    ///   Looks up a localized string similar to: 
    /// </summary>
    public static string DATAC0001 => ResourceManager.GetString(nameof(DATAC0001), Culture)!;

    /// <summary>
    ///   Looks up a localized string similar to: 
    /// </summary>
    public static string DATAC0002 => ResourceManager.GetString(nameof(DATAC0002), Culture)!;

    /// <summary>
    ///   Looks up a localized string similar to: 
    /// </summary>
    public static string DATAG0001 => ResourceManager.GetString(nameof(DATAG0001), Culture)!;

    /// <summary>
    ///   Looks up a localized string similar to: 
    /// </summary>
    public static string DATAG0002 => ResourceManager.GetString(nameof(DATAG0002), Culture)!;

    /// <summary>
    ///   Looks up a localized string similar to: 
    /// </summary>
    public static string DATALT0001 => ResourceManager.GetString(nameof(DATALT0001), Culture)!;

    /// <summary>
    ///   Looks up a localized string similar to: 
    /// </summary>
    public static string DATALT0002 => ResourceManager.GetString(nameof(DATALT0002), Culture)!;

    /// <summary>
    ///   Looks up a localized string similar to: 
    /// </summary>
    public static string DATALT0003 => ResourceManager.GetString(nameof(DATALT0003), Culture)!;

    /// <summary>
    ///   Looks up a localized string similar to: 
    /// </summary>
    public static string DATALT0004 => ResourceManager.GetString(nameof(DATALT0004), Culture)!;

    /// <summary>
    ///   Looks up a localized string similar to: 
    /// </summary>
    public static string DATALT0005 => ResourceManager.GetString(nameof(DATALT0005), Culture)!;

    /// <summary>
    ///   Looks up a localized string similar to: 
    /// </summary>
    public static string DATALT0006 => ResourceManager.GetString(nameof(DATALT0006), Culture)!;
}
