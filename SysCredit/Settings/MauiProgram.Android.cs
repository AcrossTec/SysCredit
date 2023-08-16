namespace SysCredit.Mobile;

public static partial class MauiProgram
{
    private static void ConfigureMauiHandlers(IMauiHandlersCollection Handlers)
    {
        Handlers.AddHandler(typeof(Shell), typeof(SysCredit.Mobile.Platforms.Android.Renderers.CustomShellRenderer));
    }

    private static void ConfigureEffects(IEffectsBuilder Effects)
    {
    }
}
