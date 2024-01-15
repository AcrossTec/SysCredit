namespace SysCredit.Mobile.ViewModels.Guarantors;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using DynamicData.Binding;

using Sharpnado.CollectionView.Paging;
using Sharpnado.CollectionView.Services;
using Sharpnado.CollectionView.ViewModels;
using Sharpnado.TaskLoaderView;

using SysCredit.Helpers.Delegates;
using SysCredit.Mobile.Messages;
using SysCredit.Mobile.Models;

using System.Diagnostics;

public partial class GuarantorListViewModel : ViewModelBase
{
    private const int PageSize = 20;
    private const int FirstPage = 1;
    private const int MaxItemCount = 200;

    public GuarantorListViewModel()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        Guarantors = new ObservableRangeCollection<Guarantor>();
        GuarantorsPaginator = new Paginator<Guarantor>(LoadGuarantorsPageAsync, pageSize: PageSize, maxItemCount: MaxItemCount);
        GuarantorsLoaderNotifier = new TaskLoaderNotifier<IReadOnlyCollection<Guarantor>>();
    }

    [ObservableProperty]
    private Paginator<Guarantor> m_GuarantorsPaginator = default!;

    [ObservableProperty]
    private TaskLoaderNotifier<IReadOnlyCollection<Guarantor>> m_GuarantorsLoaderNotifier = default!;

    [ObservableProperty]
    private ObservableRangeCollection<Guarantor> m_Guarantors = default!;

    [ObservableProperty]
    private int m_CurrentIndex;

    [RelayCommand]
    private async Task OnTap(Guarantor Guarantor)
    {
        if (await Popups.ShowSysCreditPopup("¿Borrar Referencia?", "Si", "No"))
        {
            Guarantors.Remove(Guarantor);                                  // Remover en GuarantorListPage
            Messenger.Send<DeleteValueMessage<Guarantor>>(new(Guarantor)); // Remover en GuarantorRegistrationPage
        }
    }

    [RelayCommand]
    private void OnScrollBegin()
    {
        Debug.WriteLine("GuarantorListViewModel: OnScrollBeginCommand");
    }

    [RelayCommand]
    private void OnScrollEnd()
    {
        Debug.WriteLine("GuarantorListViewModel: OnScrollEndCommand");
    }

    [RelayCommand]
    private void OnDragStarted(DragAndDropInfo DragInfo)
    {
        Debug.WriteLine($"OnDragStarted( From: {DragInfo.From}, To: {DragInfo.To} )");
    }

    [RelayCommand]
    private void OnDragEnded(DragAndDropInfo DragInfo)
    {
        Debug.WriteLine($"OnDragEnded( From: {DragInfo.From}, To: {DragInfo.To} )");
    }

    [RelayCommand]
    private void OnAppearing(EventArgs EventInfo)
    {
        GuarantorsLoaderNotifier.Load(LoadFirstPage);
    }

    private async Task<IReadOnlyCollection<Guarantor>> LoadFirstPage(bool IsRefreshing)
    {
        PageResult<Guarantor> Result = await GuarantorsPaginator.LoadPage(FirstPage);
        return Result.Items;
    }

    protected virtual async Task<PageResult<Guarantor>> LoadGuarantorsPageAsync(int PageNumber, int PageSize, bool IsRefresh)
    {
        await ValueTask.CompletedTask;
        IObservableCollection<Guarantor> ClientGuarantors = default!;
        Messenger.Send<ActionMessage<Fetch<IObservableCollection<Guarantor>>>>(new(Guarantors => ClientGuarantors = Guarantors));

        if (IsRefresh || (GuarantorsPaginator.TotalRemoteCount != ClientGuarantors.Count))
        {
            Guarantors = new();
        }

        var Items = ClientGuarantors.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();

        Guarantors.AddRange(Items);
        PageResult<Guarantor> ResultPage = new(ClientGuarantors.Count, Items);
        return ResultPage;
    }
}
