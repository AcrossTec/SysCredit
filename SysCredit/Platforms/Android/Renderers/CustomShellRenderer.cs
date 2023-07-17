namespace SysCredit.Platforms.Android.Renderers;

using global::Android.Content;
using global::Android.Graphics;
using global::Android.OS;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CustomShellRenderer : ShellRenderer
{
    protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker()
    {
        return new CustomToolbarAppearanceTracker();
    }
}

internal class CustomToolbarAppearanceTracker : IShellToolbarAppearanceTracker
{
    public void SetAppearance(AndroidX.AppCompat.Widget.Toolbar Toolbar, IShellToolbarTracker ToolbarTracker, ShellAppearance Appearance)
    {
        ToolbarTracker.TintColor = Appearance.ForegroundColor;
    }

    public void ResetAppearance(AndroidX.AppCompat.Widget.Toolbar Toolbar, IShellToolbarTracker ToolbarTracker)
    {
        ToolbarTracker.TintColor = null;
    }

    public void Dispose()
    {
    }
}
