namespace SysCredit.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Views;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using SysCredit.Models;

public partial class NewClientViewModel : BaseViewModel, IQueryAttributable
{
    [RelayCommand(CanExecute = nameof(CanSaveClient))]
    private Task SaveClient()
    {
        return Task.CompletedTask;
    }

    private bool CanSaveClient()
    {
        return true;
    }
}

public partial class NewClientViewModel
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
