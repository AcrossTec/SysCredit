namespace SysCredit.Mobile.ViewModels.Guarantors;

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
using CommunityToolkit.Mvvm.Messaging;

using InputKit.Shared.Controls;

using SysCredit.Mobile.Controls;
using SysCredit.Mobile.Messages;
using SysCredit.Mobile.Models;
using SysCredit.Mobile.Models.Customers.Creates;

public partial class GuarantorRegistrationViewModel : ViewModelBase
{
    public GuarantorRegistrationViewModel()
    {
    }

    public InputKit.Shared.Controls.FormView? Form { get; set; }

    public CreateGuarantor Model { get; } = new();

    [RelayCommand]
    private async Task RegisterGuarantor()
    {
        Messenger.Send(new ValueMessage<CreateGuarantor>(Model));
        Form?.Reset();

        await Popups.ShowSysCreditPopup("Fiador registrado correctamente");
    }
}
