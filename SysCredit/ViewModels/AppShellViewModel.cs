namespace SysCredit.ViewModels;

using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

public partial class AppShellViewModel : ViewModelBase
{
    [RelayCommand]
    private async Task OpenHelp(string Url)
    {
        await Launcher.OpenAsync(Url);
    }
}
