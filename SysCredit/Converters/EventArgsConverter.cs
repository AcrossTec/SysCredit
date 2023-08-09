namespace SysCredit.Converters;

using CommunityToolkit.Maui.Converters;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public class EventArgsConverter : EventArgsConverter<EventArgs>
{
}

public class EventArgsConverter<T> : BaseConverterOneWay<T?, object?> where T : EventArgs
{
    [return: NotNullIfNotNull(nameof(Value))]
    public override object? ConvertFrom(T? Value, CultureInfo? Culture = null)
    {
        return Value;
    }

    public override object? DefaultConvertReturnValue { get; set; }
}
