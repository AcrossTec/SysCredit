namespace SysCredit;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? SavedInstanceState)
    {
        base.OnCreate(SavedInstanceState);
    }

    public override void OnCreate(Bundle? SavedInstanceState, PersistableBundle? PersistentState)
    {
        base.OnCreate(SavedInstanceState, PersistentState);
    }

    public override void OnCreateContextMenu(IContextMenu? Menu, View? View, IContextMenuContextMenuInfo? MenuInfo)
    {
        base.OnCreateContextMenu(Menu, View, MenuInfo);
    }
}
