namespace SysCredit.Mobile.Controls;

using SysCredit.Mobile.Controls.Parameters;

using System.Windows.Input;

public partial class FormView : InputKit.Shared.Controls.FormView
{
    public static readonly BindableProperty ResetCommandProperty = BindableProperty.Create(nameof(SubmitCommand), typeof(ICommand), typeof(FormView));

    public static readonly BindableProperty ResetCommandParameterProperty = BindableProperty.Create(nameof(ResetCommandParameter), typeof(object), typeof(FormView));

    public FormView()
    {
        InitializeComponent();
    }

    public override void Reset()
    {
        if (ResetCommand is not null)
        {
            ResetCommand.Execute(new FormResetCommandParameter(this, () => base.Reset(), ResetCommandParameter));
        }
        else
        {
            base.Reset();
        }
    }

    public void BaseReset()
    {
        base.Reset();
    }

    public ICommand ResetCommand
    {
        get => (ICommand)GetValue(ResetCommandProperty);
        set => SetValue(ResetCommandProperty, value);
    }

    public object? ResetCommandParameter
    {
        get => GetValue(ResetCommandParameterProperty);
        set => SetValue(ResetCommandParameterProperty, value);
    }
}
