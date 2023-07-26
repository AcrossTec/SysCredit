namespace SysCredit.Extensions;

using System.Collections;
using System.Runtime.CompilerServices;

public static class PropertyExtensions
{
    private static readonly ConditionalWeakTable<object, StrongBox<Hashtable>> ExtensionField = new();

    public static Hashtable Properties(this object @object)
    {
        var Box = ExtensionField.GetValue(@object, _ => new StrongBox<Hashtable>(new Hashtable()));
        return Box.Value!;
    }
}
