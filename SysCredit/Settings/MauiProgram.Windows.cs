namespace SysCredit;

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
        return Builder;
    }
}
