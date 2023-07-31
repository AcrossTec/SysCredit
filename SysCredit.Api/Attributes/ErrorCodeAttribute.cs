namespace SysCredit.Api.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ErrorCodeAttribute : Attribute
{
    public string Prefix { get; set; } = string.Empty;

    public string[] Codes { get; set; } = Array.Empty<string>();

    public string GetErrorCode(int CodeIndex)
    {
        return $"{Prefix}{Codes[CodeIndex]}";
    }
}
