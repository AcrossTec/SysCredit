
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
        Routes.Add(("Client/Add", typeof(NewClientPage)));
        Routes.Add(("Client/List", typeof(ListClientPage)));
        Routes.Add(("Loan/Request", typeof(LoanRequestPage)));

        foreach (var (Route, Page) in Routes)
        {
            Routing.RegisterRoute(Route, Page);
        }
    }
}
