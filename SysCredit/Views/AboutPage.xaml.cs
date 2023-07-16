namespace SysCredit.Views;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }

    private void OnMenuImageButton_Clicked(object Sender, EventArgs Event)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}