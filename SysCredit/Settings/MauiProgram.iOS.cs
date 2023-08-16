namespace SysCredit.Mobile;

using global::Controls.UserDialogs.Maui;

public static partial class MauiProgram
{
    private static void ConfigureMauiHandlers(IMauiHandlersCollection Handlers)
    {
    }

    private static void ConfigureEffects(IEffectsBuilder Effects)
    {
    }

    private static void UseUserDialogs()
    {
        const string FontFamily = "Inter-Bold.ttf";

        AlertConfig.DefaultBackgroundColor = Colors.Purple;
        AlertConfig.DefaultMessageFontFamily = FontFamily;
        AlertConfig.DefaultUserInterfaceStyle = UserInterfaceStyle.Dark;
        AlertConfig.DefaultPositiveButtonTextColor = Colors.Purple;

        ToastConfig.DefaultCornerRadius = 15.0f;
        ToastConfig.DefaultMessageFontFamily = FontFamily;

        ConfirmConfig.DefaultMessageFontFamily = FontFamily;
        ActionSheetConfig.DefaultMessageFontFamily = FontFamily;
        SnackbarConfig.DefaultMessageFontFamily = FontFamily;
        HudDialogConfig.DefaultMessageFontFamily = FontFamily;
    }
}
