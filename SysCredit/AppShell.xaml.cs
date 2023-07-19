
namespace SysCredit;

using SysCredit.Views;
using SysCredit.Views.Clients;
using SysCredit.Views.Loans;

using System.Windows.Input;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();

        BindingContext = this;
    }

    public List<(string Route, Type Page)> Routes { get; } = new();

    public ICommand HelpCommand => new Command(async (Url) => await Launcher.OpenAsync((string)Url));

    void RegisterRoutes()
    {
        Routes.Add((nameof(NewClientPage), typeof(NewClientPage)));
        Routes.Add((nameof(ListClientPage), typeof(ListClientPage)));
        Routes.Add((nameof(LoanRequestPage), typeof(LoanRequestPage)));
        Routes.Add((nameof(SearchGuarantorPage), typeof(SearchGuarantorPage)));
        Routes.Add((nameof(NewGuarantorPage), typeof(NewGuarantorPage)));

        foreach (var (Route, Page) in Routes)
        {
            Routing.RegisterRoute(Route, Page);
        }
    }
}
