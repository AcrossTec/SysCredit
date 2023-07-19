﻿namespace SysCredit;

using Maui.FreakyControls.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class MauiProgram
{
    private static void ConfigureMauiHandlers(IMauiHandlersCollection Handlers)
    {
    }

    private static void ConfigureEffects(IEffectsBuilder Effects)
    {
    }

    public static MauiAppBuilder UseFreakyControls(this MauiAppBuilder Builder)
    {
        // Initialization is now a one-liner and the old methods have been deprecated and will be removed in future updates.
        // Takes one argument if you would like to init Skiasharp through FreakyControls or not (Used for RadioButton, Checkbox & SVGImageView)
        Builder.InitializeFreakyControls();
        return Builder;
    }
}
