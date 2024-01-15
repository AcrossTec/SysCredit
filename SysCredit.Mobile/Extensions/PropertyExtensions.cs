namespace SysCredit.Mobile;

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

    public static T? As<T>(this object @object) => (T?)@object;

    public static T? Property<T>(this object @object, string PropertyName)
    => (T?)@object.Properties()[PropertyName];

    public static object? Property(this object @object, string PropertyName)
        => @object.Properties()[PropertyName];

    public static void Property(this object @object, string PropertyName, object Value)
        => @object.Properties()[PropertyName] = Value;
}
