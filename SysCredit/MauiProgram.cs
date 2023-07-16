namespace SysCredit;

using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;

using Microsoft.Extensions.Logging;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var Builder = MauiApp.CreateBuilder();

        Builder
            .UseMauiApp<App>()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMarkup()
            .ConfigureFonts(Fonts =>
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

                Fonts.AddFont("SysCredit-Icon-Fonts.ttf", "SysCredit");
            })
            .ConfigureMauiHandlers(Handlers =>
            {
#if ANDROID
                Handlers.AddHandler(typeof(Shell), typeof(SysCredit.Platforms.Android.Renderers.CustomShellRenderer));
#endif
            });

#if DEBUG
        Builder.Logging.AddDebug();
#endif

        return Builder.Build();
    }
}
