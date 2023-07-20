namespace SysCredit.ViewModels.Customers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class CustomerListViewModel : BaseViewModel
{
}

public partial class CustomerListViewModel
{
    public override void ApplyQueryAttributes(IDictionary<string, object> Query)
    {
        base.ApplyQueryAttributes(Query);
        SetBackButtonBehavior(LookupParam<bool>(nameof(BackButtonBehavior)));
    }

    private void SetBackButtonBehavior(bool IsEnabled)
    {
        if (IsEnabled is false)
        {
            Shell.SetBackButtonBehavior(Shell.GetBackButtonBehavior(Shell.Current.CurrentPage), default);
        }
    }
}
