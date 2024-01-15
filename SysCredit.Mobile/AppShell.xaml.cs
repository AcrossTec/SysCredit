namespace SysCredit.Mobile;

using SysCredit.Mobile.Views;
using SysCredit.Mobile.Views.Customers;
using SysCredit.Mobile.Views.Loans;

using System.Windows.Input;

//
//  References:
//      https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/flyout
//
//  Respond to system theme changes:
//      https://learn.microsoft.com/en-us/dotnet/maui/user-interface/system-theme-changes
//
//  GravatarImageSourcePage.xaml:
//      https://github.com/CommunityToolkit/Maui/blob/main/samples/CommunityToolkit.Maui.Sample/Pages/ImageSources/GravatarImageSourcePage.xaml
//

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }
}
