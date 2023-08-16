namespace SysCredit.Mobile.ViewModels;

using CommunityToolkit.Mvvm.Input;

using SysCredit.Mobile.Services;

public interface IViewModelBase : IQueryAttributable
{
    public INavigationService NavigationService { get; }

    public IAsyncRelayCommand InitializeAsyncCommand { get; }

    public bool IsBusy { get; }

    public bool IsInitialized { get; }

    Task InitializeAsync();
}