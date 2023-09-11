namespace SysCredit.Api.Constants;

/// <summary>
/// 
/// </summary>
public static class SysCreditConstants
{
    public const string Empty = "";

    public const string ConnectionStringKey = "SysCreditConnectionString";

    public const string ConnectionStringEnv = $"SQLCONNSTR_{ConnectionStringKey}";

    public const string CorsOriginsKey = "CorsOrigins";

    public const string CorsAllowSpecificOrigins = "CorsAllowSpecificOrigins";
}
