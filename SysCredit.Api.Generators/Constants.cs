﻿namespace SysCredit.Api.Generators;

public static class Constants
{
    public const int MinCodeNumber = 0;
    public const int MaxCodeNumber = 500;

    public const string Tab = "    ";
    public const string NewLine = "\r\n";
    public const string GeneratedFileExtension = ".Generated.cs";

    public const string AutoGenerated =
        "//------------------------------------------------------------------------------\r\n" +
        "// <auto-generated>                                                             \r\n" +
        "//     This code was generated by a tool.                                       \r\n" +
        "//     Runtime Version: 1.0.0.0                                                 \r\n" +
        "//                                                                              \r\n" +
        "//     Changes to this file may cause incorrect behavior and will be lost if    \r\n" +
        "//     the code is regenerated.                                                 \r\n" +
        "// </auto-generated>                                                            \r\n" +
        "//------------------------------------------------------------------------------\r\n";

    public const string SysCreditApiConstantsNamespaceName = "SysCredit.Api.Constants";
    public const string SysCreditApiConstantsNamespace = "namespace SysCredit.Api.Constants;\r\n";
    public const string ErrorCodePrefixAttribute = "SysCredit.Api.Attributes.ErrorCodePrefixAttribute";
}