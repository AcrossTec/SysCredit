namespace SysCredit.Api.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ErrorCodeAttribute : Attribute
{
    public ErrorCodeAttribute(string Prefix, string[] Codes)
    {
        this.Prefix = Prefix;
        this.Codes = Codes;
    }

    public string Prefix { get; }

    public string[] Codes { get; }

    public string GetErrorCode(int CodeIndex)
    {
        return $"{Prefix}{Codes[CodeIndex]}";
    }
}
