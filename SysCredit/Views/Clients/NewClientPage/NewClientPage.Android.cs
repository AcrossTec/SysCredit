namespace SysCredit.Views.Clients;

using Android.Graphics.Drawables;

using Google.Android.Material.TextField;

using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//
// https://github.com/FreakyAli/MAUI.FreakyControls
//

public partial class NewClientPage
{
    private void ModifyEntry()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("EntryCustomization", (Handler, View) =>
        {
            Handler.PlatformView.SetHintTextColor(Color.FromArgb("#999999").ToPlatform());

            var GradientDrawable = new GradientDrawable();
            GradientDrawable.SetCornerRadius(Handler.MauiContext!.Context!.ToPixels(6));
            GradientDrawable.SetStroke((int)Handler.MauiContext!.Context!.ToPixels(1), Color.FromArgb("#AFAFAF").ToDefaultColorStateList());
            GradientDrawable.SetColor(Colors.Transparent.ToPlatform());
            Handler.PlatformView.SetBackground(GradientDrawable);

            var Padding = 6;
            var PaddingTop = (int)Handler.MauiContext!.Context!.ToPixels(Padding);
            var PaddingBottom = (int)Handler.MauiContext!.Context!.ToPixels(Padding);
            var PaddingLeft = (int)Handler.MauiContext!.Context!.ToPixels(30);
            var PaddingRight = (int)Handler.MauiContext!.Context!.ToPixels(Padding);
            Handler.PlatformView.SetPadding(PaddingLeft, PaddingTop, PaddingRight, PaddingBottom);
        });
    }
}
