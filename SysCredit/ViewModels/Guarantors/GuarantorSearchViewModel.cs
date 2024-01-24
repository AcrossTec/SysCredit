namespace SysCredit.Mobile.ViewModels.Guarantors;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Sharpnado.CollectionView.Paging;
using Sharpnado.CollectionView.Services;
using Sharpnado.CollectionView.ViewModels;
using Sharpnado.TaskLoaderView;

using SysCredit.Mobile.Controls.Dialogs;
using SysCredit.Mobile.Messages;
using SysCredit.Mobile.Models;
using SysCredit.Mobile.Services.Https;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class GuarantorSearchViewModel : ViewModelBase
{
    private const int PageSize = 20;
    private const int FirstPage = 1;
    private const int MaxItemCount = 200;

    private readonly ISysCreditApiService SysCreditApi;

    public GuarantorSearchViewModel(ISysCreditApiService SysCreditApi)
    {
        this.SysCreditApi = SysCreditApi;
        Initialize();
    }

    protected virtual void Initialize()
    {
        Guarantors = new ObservableRangeCollection<Guarantor>();
        GuarantorsPaginator = new Paginator<Guarantor>(LoadGuarantorsPageAsync, pageSize: PageSize, maxItemCount: MaxItemCount);
        GuarantorsLoaderNotifier = new TaskLoaderNotifier<IReadOnlyCollection<Guarantor>>();
        GuarantorsLoaderNotifier.Load(LoadFirstPage);
    }

    [ObservableProperty]
    private Paginator<Guarantor> m_GuarantorsPaginator = default!;

    [ObservableProperty]
    private TaskLoaderNotifier<IReadOnlyCollection<Guarantor>> m_GuarantorsLoaderNotifier = default!;

    [ObservableProperty]
    private ObservableRangeCollection<Guarantor> m_Guarantors = default!;

    [ObservableProperty]
    private string? m_Query;

    [RelayCommand]
    private async Task OnTap(Guarantor Guarantor)
    {
        if (await Popups.ShowSysCreditPopup("¿Agregar Fiador?", "Si", "No"))
        {
            Messenger.Send<InsertValueMessage<Guarantor>>(new(Guarantor));
        }
    }

    [RelayCommand]
    private void OnPerformSearch()
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
        UserDialogs.ShowLoading("Cagando...");
        var SearchResponse = await SysCreditApi.SearchGuarantorsAsync(Query, PageNumber, PageSize);
        UserDialogs.HideHud();

        if (SearchResponse.Status.HasError)
        {
            await Popups.ShowSysCreditPopup("Ha ocurrido un error al leer datos del servidor");
            return new PageResult<Guarantor>();
        }

        if (SearchResponse.Data == null || SearchResponse.Data.Count == 0)
        {
            await Popups.ShowSysCreditPopup("No hay registros que coincidan con la búsqueda");
            return new PageResult<Guarantor>();
        }

        if (IsRefresh)
        {
            Guarantors = new();
        }

        Guarantors.AddRange(SearchResponse.Data);
        PageResult<Guarantor> ResultPage = new(Guarantors.Count /*TODO: ServerCount*/, Guarantors);
        return ResultPage;
    }
}
