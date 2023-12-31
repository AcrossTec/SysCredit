﻿namespace SysCredit.Api.Constants;

/// <summary>
///     Constantes de uso general.
/// </summary>
public static class SysCreditConstants
{
    public const string Empty = "";

    public const string SqlProcedureKey = "SqlProcedure";

    public const string SqlErrorsKey = "SqlErrors";

    public const string ErrorStatusKey = "ErrorStatus";

    public const string ErrorCategoryKey = "ErrorCategory";

    public const string ErrorCodeKey = "ErrorCode";

    public const string ExceptionTypeKey = "ExceptionType";

    public const string ExceptionMessagesKey = "ExceptionMessages";

    public const string ExceptionSourceKey = "ExceptionSource";

    public const string ExceptionStackTraceKey = "ExceptionStackTrace";

    public const string MethodIdKey = "MethodId";

    public const string MethodFullNameKey = "MethodFullName";

    public const string TypeFullNameKey = "TypeFullName";

    public const string ValidatedInstanceKey = "ValidatedInstance";

    public const string IsFromValidationExceptionKey = "IsFromValidationException";

    public const string DefaultKey = "DefaultKey";

    public const string ConnectionStringKey = "SysCreditConnectionString";

    public const string ConnectionStringEnv = $"SQLCONNSTR_{ConnectionStringKey}";

    public const string CorsOriginsKey = "CorsOrigins";

    public const string CorsAllowSpecificOrigins = "CorsAllowSpecificOrigins";

    public const string AuthorizationHeaderName = "Authorization";

    public const string AuthorizationHeaderScheme = "Bearer";

    public const string Log4NetConfigFile = "Log4Net.config";
}
