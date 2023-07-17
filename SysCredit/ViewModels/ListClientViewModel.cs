namespace SysCredit.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class ListClientViewModel : BaseViewModel, IQueryAttributable
{
}

public partial class ListClientViewModel
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

    public void ApplyQueryAttributes(IDictionary<string, object> Query)
    {
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
