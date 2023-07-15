
namespace SysCredit;

using SysCredit.Views;

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

    public ICommand HelpCommand => new Command<string>(async (Url) => await Launcher.OpenAsync(Url));

    void RegisterRoutes()
    {
        Routes.Add(("Home", typeof(MainPage)));
        Routes.Add(("Generic1", typeof(GenericPage)));
        Routes.Add(("Generic2", typeof(GenericPage)));
        Routes.Add(("Generic3", typeof(GenericPage)));
        Routes.Add(("About", typeof(AboutPage)));

        foreach (var (Route, Page) in Routes)
        {
            Routing.RegisterRoute(Route, Page);
        }
    }
}
