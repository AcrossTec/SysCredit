namespace SysCredit.Controls;

using CommunityToolkit.Maui.Views;

using Maui.FreakyControls;

using System.Windows.Input;

public partial class SysCreditPopup : Popup
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(SysCreditPopup));

    public static readonly BindableProperty IsCancelEnabledProperty =
        BindableProperty.Create(nameof(IsCancelEnabled), typeof(bool), typeof(SysCreditPopup), false, propertyChanged: OnIsCancelEnabledPropertyChanged);

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

    public SysCreditPopup()
    {
        InitializeComponent();
        ResultWhenUserTapsOutsideOfPopup = true;
        BindingContext = this;
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

    private void OnFreakyButtonClicked(object Sender, EventArgs Event)
    {
        View Button = (View)Sender;

        switch (Button.StyleId)
        {
            case "OKButton":
            {
                Close(true);
                break;
            }

            case "CancelButton":
            {
                Close(false);
                break;
            }
        }
    }

    private static void OnIsCancelEnabledPropertyChanged(BindableObject Bindable, object OldValue, object NewValue)
    {
        var Popup = (SysCreditPopup)Bindable;
        Popup.ResultWhenUserTapsOutsideOfPopup = !(bool)NewValue;
    }
}
