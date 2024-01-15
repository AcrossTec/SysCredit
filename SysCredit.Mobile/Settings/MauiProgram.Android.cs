namespace SysCredit.Mobile;

using global::Controls.UserDialogs.Maui;

public static partial class MauiProgram
{
    private static void ConfigureMauiHandlers(IMauiHandlersCollection Handlers)
    {
        Handlers.AddHandler(typeof(Shell), typeof(SysCredit.Mobile.Platforms.Android.Renderers.CustomShellRenderer));
    }

    private static void ConfigureEffects(IEffectsBuilder Effects)
    {
    }

    private static MauiAppBuilder UseUserDialogs(this MauiAppBuilder Builder)
    {
        UserDialogs.UseUserDialogs(Builder, static () =>
        {
            const string FontFamily = "Inter-Bold.ttf";

            AlertConfig.DefaultBackgroundColor = Colors.Purple;
            AlertConfig.DefaultMessageFontFamily = FontFamily;
            AlertConfig.DefaultUserInterfaceStyle = UserInterfaceStyle.Dark;
            AlertConfig.DefaultPositiveButtonTextColor = Colors.Purple;

            HudDialogConfig.DefaultMessageFontFamily = FontFamily;
            HudDialogConfig.DefaultBackgroundColor = Color.FromArgb("#04174e").WithAlpha(0.7f);
            HudDialogConfig.DefaultLoaderColor = Color.FromArgb("#aa00ff");

            ToastConfig.DefaultCornerRadius = 15.0f;
            ToastConfig.DefaultMessageFontFamily = FontFamily;

            ConfirmConfig.DefaultMessageFontFamily = FontFamily;
            ActionSheetConfig.DefaultMessageFontFamily = FontFamily;
            SnackbarConfig.DefaultMessageFontFamily = FontFamily;
        });

        return Builder;
    }
}
