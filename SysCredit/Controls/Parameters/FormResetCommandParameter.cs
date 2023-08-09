namespace SysCredit.Controls.Parameters;

public class FormResetCommandParameter
{
    private Action Reset;

    public FormResetCommandParameter(InputKit.Shared.Controls.FormView Form, Action Reset, object? Parameter)
    {
        this.Form = Form;
        this.Reset = Reset;
        this.Parameter = Parameter;
    }

    public void FormReset() => Reset.Invoke();

    public InputKit.Shared.Controls.FormView Form { get; }

    public object? Parameter { get; set; }
}
