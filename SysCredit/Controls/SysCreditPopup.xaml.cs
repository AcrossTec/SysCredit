namespace SysCredit.Controls;

using Mopups.Pages;

using System.Windows.Input;

public partial class SysCreditPopup : PopupPage
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(SysCreditPopup));

    public static readonly BindableProperty IsCancelEnabledProperty =
        BindableProperty.Create(nameof(IsCancelEnabled), typeof(bool), typeof(SysCreditPopup), false);

    public static readonly BindableProperty OkTextProperty =
        BindableProperty.Create(nameof(OkText), typeof(string), typeof(SysCreditPopup), "Aceptar");

    public static readonly BindableProperty CancelTextProperty =
        BindableProperty.Create(nameof(CancelText), typeof(string), typeof(SysCreditPopup), "Cancelar");

    public static readonly BindableProperty ButtonWidthProperty =
       BindableProperty.Create(nameof(ButtonWidth), typeof(double), typeof(SysCreditPopup), 100.0);

    public static readonly BindableProperty OkCommandParameterProperty =
      BindableProperty.Create(nameof(OkCommandParameter), typeof(object), typeof(SysCreditPopup));

    public static readonly BindableProperty OkCommandProperty =
        BindableProperty.Create(nameof(OkCommand), typeof(ICommand), typeof(SysCreditPopup));

    public static readonly BindableProperty CancelCommandParameterProperty =
      BindableProperty.Create(nameof(CancelCommandParameter), typeof(object), typeof(SysCreditPopup));

    public static readonly BindableProperty CancelCommandProperty =
        BindableProperty.Create(nameof(CancelCommand), typeof(ICommand), typeof(SysCreditPopup));

    private static readonly Color BackdropColor = Colors.Black.WithAlpha(.6f);

    public SysCreditPopup()
    {
        InitializeComponent();
        BindingContext = this;
        BackgroundColor = BackdropColor;
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string OkText
    {
        get => (string)GetValue(OkTextProperty);
        set => SetValue(OkTextProperty, value);
    }

    public string CancelText
    {
        get => (string)GetValue(CancelTextProperty);
        set => SetValue(CancelTextProperty, value);
    }

    public bool IsCancelEnabled
    {
        get => (bool)GetValue(IsCancelEnabledProperty);
        set => SetValue(IsCancelEnabledProperty, value);
    }

    public double ButtonWidth
    {
        get => (double)GetValue(ButtonWidthProperty);
        set => SetValue(ButtonWidthProperty, value);
    }

    public ICommand? OkCommand
    {
        get => (ICommand)GetValue(OkCommandProperty);
        set => SetValue(OkCommandProperty, value);
    }

    public object? OkCommandParameter
    {
        get => GetValue(OkCommandParameterProperty);
        set => SetValue(OkCommandParameterProperty, value);
    }

    public ICommand? CancelCommand
    {
        get => (ICommand)GetValue(CancelCommandProperty);
        set => SetValue(CancelCommandProperty, value);
    }

    public object? CancelCommandParameter
    {
        get => GetValue(CancelCommandParameterProperty);
        set => SetValue(CancelCommandParameterProperty, value);
    }
}
