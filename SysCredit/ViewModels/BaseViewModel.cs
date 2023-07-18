namespace SysCredit.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.Generic;

public class BaseViewModel : ObservableObject, IQueryAttributable
{
    public IDictionary<string, object>? QueryParams { get; private set; }

    public virtual void ApplyQueryAttributes(IDictionary<string, object> Query)
    {
        QueryParams = Query;
    }
}
