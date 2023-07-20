namespace SysCredit;

using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.Logging;

using SysCredit.Messages;

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
            .UseFreakyControls()
            .ConfigureMauiHandlers(ConfigureMauiHandlers)
            .ConfigureEffects(ConfigureEffects)
            .ConfigureFonts(ConfigureFonts);

#if DEBUG
        Builder.Logging.AddDebug();
#endif

        return Builder.Build();
    }

    public static void OnDeviceDisplayInfoChanged(object? Sender, DisplayInfoChangedEventArgs Event)
    {
        // Device information
        //  https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device/information?tabs=android
        //
        // Device display information
        //  https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device/display?tabs=android
        //
        // WeakReferenceManager
        //  https://www.c-sharpcorner.com/article/net-maui-good-bye-messagingcenter-welcome-weakreferencemanager/

        WeakReferenceMessenger.Default.Send(new DisplayOrientationChanged(Event.DisplayInfo.Orientation));
    }

    private static void ConfigureFonts(IFontCollection Fonts)
    {
        Fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        Fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        
        Fonts.AddFont("Brands-Regular-400.otf", "FontAwesomeBrands");
        Fonts.AddFont("Free-Regular-400.otf", "FontAwesomeRegular");
        Fonts.AddFont("Free-Solid-900.otf", "FontAwesomeSolid");
        
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
}
