namespace SysCredit.Mobile.ViewModels.Catalogs.Relationships;

using CommunityToolkit.Mvvm.Input;
using SysCredit.Mobile.Views.Catalogs.Relationships;

public partial class RelationshipsCrudPageViewModel : ViewModelBase
{
    public Func<Task> DisplayPrompt { get; set; } = null!;

    [RelayCommand]
    public async Task GoToFetch()
    {
        await Shell.Current.GoToAsync(nameof(ViewRelationshipCatalogPage));
    }

    [RelayCommand]
    public async Task DisplayInsert()
    {
        await DisplayPrompt.Invoke();
    }

    [RelayCommand]
    public async Task GoToDelete()
    {
        await Shell.Current.GoToAsync(nameof(DeleteRelationshipPage));
    }

    [RelayCommand]
    public async Task GoToUpdate()
    {
        await Shell.Current.GoToAsync(nameof(UpdateRelationshipPage));
    }
}
