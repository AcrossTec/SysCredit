namespace SysCredit.Views.Clients;

using System.Windows.Input;

public partial class NewClientPage : ContentPage
{
    public NewClientPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public ICommand BackButtonCommand => new Command(() => Shell.Current.SendBackButtonPressed());
}
