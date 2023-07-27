namespace SysCredit.ViewModels.Customers;

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using InputKit.Shared.Controls;

using SysCredit.Extensions;
using SysCredit.Messages;
using SysCredit.Models.Customers.Creates;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

public partial class ReferenceRegistrationBottomSheetViewModel : ViewModelBase
{
    public ReferenceRegistrationBottomSheetViewModel()
    {
    }

    [ObservableProperty]
    private CreateReference m_Model = new();

    [RelayCommand]
    private void RegisterReference()
    {
        WeakReferenceMessenger.Default.Send(new ValueMessage<CreateReference>(Model));
        this.Property<FormView>("Form")?.Reset();
    }
}
