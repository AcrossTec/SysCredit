namespace SysCredit.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class LoanRequestViewModel : BaseViewModel
{
}

public partial class LoanRequestViewModel
{
    public BackButtonBehavior? BackButtonBehavior { get; private set; }

    private TemplatedPage? _Page;

    public TemplatedPage? Page
    {
        get => _Page;
        set
        {
            BackButtonBehavior = Shell.GetBackButtonBehavior(_Page = value);
            SetBackButtonBehavior(false);
        }
    }

    public override void ApplyQueryAttributes(IDictionary<string, object> Query)
    {
        base.ApplyQueryAttributes(Query);

        if (Query.TryGetValue(nameof(BackButtonBehavior), out var Value))
        {
            SetBackButtonBehavior(Convert.ToBoolean(Value));
        }
    }

    private void SetBackButtonBehavior(bool IsEnabled)
    {
        Shell.SetBackButtonBehavior(Page, IsEnabled ? BackButtonBehavior : default);
    }
}
