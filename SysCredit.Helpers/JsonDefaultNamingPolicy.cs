namespace SysCredit.Helpers;

using System.Runtime.CompilerServices;
using System.Text.Json;

public class JsonDefaultNamingPolicy : JsonNamingPolicy
{
    public static JsonDefaultNamingPolicy DefaultNamingPolicy { get; } = new JsonDefaultNamingPolicy();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ConvertName(string Name)
    {
        return Name;
    }
}
