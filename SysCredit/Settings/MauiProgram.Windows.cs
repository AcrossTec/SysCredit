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

    public static MauiAppBuilder InitSkiaSharpLibrary(this MauiAppBuilder Builder)
    {
        return Builder;
    }
}
