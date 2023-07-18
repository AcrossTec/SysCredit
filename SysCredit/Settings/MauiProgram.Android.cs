namespace SysCredit;

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
        Handlers.AddHandler(typeof(Shell), typeof(SysCredit.Platforms.Android.Renderers.CustomShellRenderer));
        Handlers.AddFreakyHandlers(); // To Init your freaky handlers for Entry and Editor
    }

    public static MauiAppBuilder InitSkiaSharpLibrary(this MauiAppBuilder Builder)
    {
        // This line is needed for the following issue: https://github.com/mono/SkiaSharp/issues/1979
        Builder.InitSkiaSharp(); // Use this if you want to use FreakySvgImageView
        return Builder;
    }
}
