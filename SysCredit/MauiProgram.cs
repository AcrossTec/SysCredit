namespace SysCredit.Mobile;

using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Maps;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Messaging;

using DotNurse.Injector;

using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;

using Mopups.Hosting;

using Sharpnado.CollectionView;
using Sharpnado.Tabs;

using SkiaSharp.Views.Maui.Controls.Hosting;

using SysCredit.Mobile.Messages;
using SysCredit.Mobile.Services;
using SysCredit.Mobile.Services.Https;
using SysCredit.Mobile.Services.Settings;
using SysCredit.Mobile.ViewModels.Customers;
using SysCredit.Mobile.ViewModels.Guarantors;

using The49.Maui.BottomSheet;
using The49.Maui.ContextMenu;
using The49.Maui.Insets;

using UraniumUI;

public static partial class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        DeviceDisplay.MainDisplayInfoChanged += OnDeviceDisplayInfoChanged;

        var Builder = MauiApp.CreateBuilder();

        Builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMarkup()
            // .UseMauiCommunityToolkitMaps("<Key>")
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .UseUraniumUIBlurs(false)
            .UseUraniumUIWebComponents()
            .UseSharpnadoTabs(loggerEnable: false)
            .UseSharpnadoCollectionView(loggerEnable: false)
            .UseSkiaSharp()
            .UseInsets()
            .UseContextMenu()
            .UseBottomSheet()
            .UseUserDialogs()
            .ConfigureMopups()
            .ConfigureMauiHandlers(ConfigureMauiHandlers)
            .ConfigureEffects(ConfigureEffects)
            .ConfigureFonts(ConfigureFonts)
            .RegisterRoutes()
            .RegisterAppServices()
            .RegisterViewModels()
            .RegisterViews();

#if DEBUG
        Builder.Logging.AddDebug();
#endif
        global::Sharpnado.TaskLoaderView.Initializer.Initialize(loggerEnable: false);
        return Builder.Build();
    }

    public static void OnDeviceDisplayInfoChanged(object? Sender, DisplayInfoChangedEventArgs Event)
    {
        //
        // Device information
        //  https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device/information?tabs=android
        //
        // Device display information
        //  https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device/display?tabs=android
        //
        // WeakReferenceManager
        //  https://www.c-sharpcorner.com/article/net-maui-good-bye-messagingcenter-welcome-weakreferencemanager/
        //
        WeakReferenceMessenger.Default.Send(new DisplayOrientationChanged(Event.DisplayInfo.Orientation));
    }

    private static void ConfigureFonts(IFontCollection Fonts)
    {
        // https://andreinitescu.github.io/IconFont2Code/

        // Fonts.AddFontAwesomeIconFonts();
        // Fonts.AddMaterialIconFonts();
        // Fonts.AddFluentIconFonts();

        Fonts.AddFont("Segoe-Fluent-Icons.ttf", "Fluent");

        Fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        Fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

        Fonts.AddFont("Brands-Regular-400.otf", "FontAwesomeBrands");
        Fonts.AddFont("Free-Regular-400.otf", "FontAwesomeRegular");
        Fonts.AddFont("Free-Solid-900.otf", "FontAwesomeSolid");

        Fonts.AddFont("MaterialIconsOutlined-Regular.otf", "MaterialOutlined");
        Fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialRegular");
        Fonts.AddFont("MaterialIconsRound-Regular.otf", "MaterialRound");
        Fonts.AddFont("MaterialIconsSharp-Regular.otf", "MaterialSharp");
        Fonts.AddFont("MaterialIconsTwoTone-Regular.otf", "MaterialTwoTone");

        Fonts.AddFont("Inter-Black.ttf", "InterBlack");
        Fonts.AddFont("Inter-Bold.ttf", "InterBold");
        Fonts.AddFont("Inter-ExtraBold.ttf", "InterExtraBold");
        Fonts.AddFont("Inter-ExtraLight.ttf", "InterExtraLight");
        Fonts.AddFont("Inter-Light.ttf", "InterLight");
        Fonts.AddFont("Inter-Medium.ttf", "InterMedium");
        Fonts.AddFont("Inter-Regular.ttf", "InterRegular");
        Fonts.AddFont("Inter-SemiBold.ttf", "InterSemiBold");
        Fonts.AddFont("Inter-Thin.ttf", "InterThin");
        Fonts.AddFont("Inter-VariableFont.ttf", "InterVariableFont");
    }

    private static MauiAppBuilder RegisterAppServices(this MauiAppBuilder Builder)
    {
        Builder.Services.AddSingleton<ISettingsService, SettingsService>();
        Builder.Services.AddSingleton<INavigationService, MauiNavigationService>();
        Builder.Services.AddScoped<ISysCreditApiService, SysCreditApiService>();
        Builder.Services.AddMopupsDialogs();
        return Builder;
    }

    private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder Builder)
    {
        Builder.Services.AddScoped<ReferenceRegistrationViewModel>();
        Builder.Services.AddScoped<ReferenceListViewModel>();
        Builder.Services.AddScoped<GuarantorSearchViewModel>();
        Builder.Services.AddScoped<GuarantorRegistrationViewModel>();
        Builder.Services.AddScoped<GuarantorListViewModel>();
        return Builder;
    }

    private static MauiAppBuilder RegisterViews(this MauiAppBuilder Builder)
    {
        var ThisAssembly = typeof(MauiProgram).Assembly;

        Builder.Services
            .AddServicesFrom(
                Type => typeof(Page).IsAssignableFrom(Type),
                ServiceLifetime.Transient,
                Options => Options.Assembly = ThisAssembly)
            .AddServicesByAttributes(assembly: ThisAssembly);

        return Builder;
    }

    private static MauiAppBuilder RegisterRoutes(this MauiAppBuilder Builder)
    {
        // Routing.RegisterRoute(nameof(CustomerRegistrationPage), typeof(CustomerRegistrationPage));
        // Routing.RegisterRoute(nameof(CustomerInformationPage), typeof(CustomerInformationPage));
        // Routing.RegisterRoute(nameof(CustomerEditPage), typeof(CustomerEditPage));
        // Routing.RegisterRoute(nameof(CustomerListPage), typeof(CustomerListPage));
        // Routing.RegisterRoute(nameof(CustomerSearchPage), typeof(CustomerSearchPage));

        // Routing.RegisterRoute(nameof(GuarantorRegistrationPage), typeof(GuarantorRegistrationPage));
        // Routing.RegisterRoute(nameof(GuarantorInformationPage), typeof(GuarantorInformationPage));
        // Routing.RegisterRoute(nameof(GuarantorListPage), typeof(GuarantorListPage));
        // Routing.RegisterRoute(nameof(GuarantorEditPage), typeof(GuarantorEditPage));
        // Routing.RegisterRoute(nameof(GuarantorSearchPage), typeof(GuarantorSearchPage));

        // Routing.RegisterRoute(nameof(LoanRequestPage), typeof(LoanRequestPage));
        // Routing.RegisterRoute(nameof(LoanListPage), typeof(LoanListPage));
        return Builder;
    }
}
