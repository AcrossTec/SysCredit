namespace SysCredit.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using System.Collections.Generic;

public class ViewModelBase : ObservableRecipient, IQueryAttributable
{
    public ViewModelBase() : base(WeakReferenceMessenger.Default)
    {
    }

    public string CompanyName => (string)Application.Current!.Resources[nameof(CompanyName)];

    public IDictionary<string, object>? QueryParams { get; private set; }

    public TParam? LookupParam<TParam>(string Key, TParam? Default = default)
    {
        if (QueryParams!.TryGetValue(Key, out var Value))
        {
            return (TParam)Value;
        }

        return Default;
    }

    public virtual void ApplyQueryAttributes(IDictionary<string, object> Query)
    {
        QueryParams = Query;
    }
}
