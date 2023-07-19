namespace SysCredit;

using Maui.FreakyControls.Extensions;
// using Maui.FreakyEffects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class MauiProgram
{
    private static void ConfigureMauiHandlers(IMauiHandlersCollection Handlers)
    {
        Handlers.AddHandler(typeof(Shell), typeof(SysCredit.Platforms.Android.Renderers.CustomShellRenderer));
    }

    private static void ConfigureEffects(IEffectsBuilder Effects)
    {
        // Effects.InitFreakyEffects();
    }

    public static MauiAppBuilder UseFreakyControls(this MauiAppBuilder Builder)
    {
        // Initialization is now a one-liner and the old methods have been deprecated and will be removed in future updates.
        // Takes one argument if you would like to init Skiasharp through FreakyControls or not (Used for RadioButton, Checkbox & SVGImageView)
        Builder.InitializeFreakyControls();
        return Builder;
    }
}
