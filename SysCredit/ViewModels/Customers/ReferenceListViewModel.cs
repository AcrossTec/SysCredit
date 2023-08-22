namespace SysCredit.Mobile.ViewModels.Customers;

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
using SysCredit.Mobile.Models.Customers.Creates;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

public partial class ReferenceListViewModel : ViewModelBase, IRecipient<InsertValueMessage<CreateReference>>
{
    private const int PageSize = 20;
    private const int FirstPage = 1;
    private const int MaxItemCount = 200;

    public ReferenceListViewModel()
    {
        Initialize();
    }

    public void Receive(InsertValueMessage<CreateReference> Message)
    {
        References.Add(Message.Value);
    }

    [NotNull]
    public Paginator<CreateReference> ReferencesPaginator { get; private set; } = default!;

    [NotNull]
    public TaskLoaderNotifier<IReadOnlyCollection<CreateReference>> ReferencesLoaderNotifier { get; private set; } = default!;

    [NotNull]
    [ObservableProperty]
    private ObservableRangeCollection<CreateReference> m_References = default!;

    [ObservableProperty]
    private int m_CurrentIndex;

    [RelayCommand]
    private async Task OnTap(CreateReference Reference)
    {
        if (await Popups.ShowSysCreditPopup("¿Borrar Referencia?", "Si", "No"))
        {
            References.Remove(Reference);                                        // Remover en ReferenceListPage
            Messenger.Send<DeleteValueMessage<CreateReference>>(new(Reference)); // Remover en CustomerRegistrationPage

            if (References.Count == 0)
            {
                ReferencesLoaderNotifier.Load(true);
            }
        }
    }

    [RelayCommand]
    private void OnScrollBegin()
    {
        Debug.WriteLine("ReferenceListViewModel: OnScrollBeginCommand");
    }

    [RelayCommand]
    private void OnScrollEnd()
    {
        Debug.WriteLine("ReferenceListViewModel: OnScrollEndCommand");
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

    protected virtual void Initialize()
    {
        Messenger.Register<InsertValueMessage<CreateReference>>(this);
        References = new ObservableRangeCollection<CreateReference>();
        ReferencesPaginator = new Paginator<CreateReference>(LoadReferencesPageAsync, pageSize: PageSize, maxItemCount: MaxItemCount);
        ReferencesLoaderNotifier = new TaskLoaderNotifier<IReadOnlyCollection<CreateReference>>();
        ReferencesLoaderNotifier.Load(LoadFirstPage);
    }

    private async Task<IReadOnlyCollection<CreateReference>> LoadFirstPage(bool IsRefreshing)
    {
        PageResult<CreateReference> Result = await ReferencesPaginator.LoadPage(FirstPage);
        return Result.Items;
    }

    protected virtual async Task<PageResult<CreateReference>> LoadReferencesPageAsync(int PageNumber, int PageSize, bool IsRefresh)
    {
        await ValueTask.CompletedTask;
        IObservableCollection<CreateReference> ClientReferences = default!;
        Messenger.Send<ActionMessage<Request<IObservableCollection<CreateReference>>>>(new(References => ClientReferences = References));

        if (IsRefresh || (ReferencesPaginator.TotalRemoteCount != ClientReferences.Count))
        {
            References = new();
        }

        var Items = ClientReferences.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();

        References.AddRange(Items);
        PageResult<CreateReference> ResultPage = new(ClientReferences.Count, Items);
        return ResultPage;
    }
}
