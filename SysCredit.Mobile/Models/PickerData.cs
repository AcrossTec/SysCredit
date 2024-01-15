namespace SysCredit.Mobile.Models;

public class PickerData : BindableObject
{
    public readonly BindableProperty DataProperty = BindableProperty.Create(nameof(Data), typeof(object), typeof(PickerData));
    public readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string), typeof(PickerData), string.Empty);

    public object? Data
    {
        get => GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
}
