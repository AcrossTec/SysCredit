namespace SysCredit.Mobile.Platforms.Android.Renderers;

using global::Android.Content;
using global::Android.Graphics;
using global::Android.OS;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class CustomShellRenderer : ShellRenderer
{
    protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker()
    {
        return new CustomToolbarAppearanceTracker(this);
    }

    protected override IShellTabLayoutAppearanceTracker CreateTabLayoutAppearanceTracker(ShellSection ShellSection)
    {
        return new CustomTabLayoutAppearanceTracker(this)
        {
            ShellSection = ShellSection
        };
    }

    protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection ShellSection)
    {
        return new CustomSectionRenderer(this)
        {
            ShellSection = ShellSection
        };
    }
}

internal class CustomSectionRenderer : ShellSectionRenderer, Google.Android.Material.Tabs.TabLayoutMediator.ITabConfigurationStrategy
{
    protected IShellSectionController SectionController => ShellSection;

    protected IMauiContext MauiContext => ShellContext.Shell.Handler!.MauiContext!;

    protected Google.Android.Material.Tabs.TabLayout TabLayout { get; private set; } = default!;

    public CustomSectionRenderer(IShellContext ShellContext) : base(ShellContext)
    {
    }

    void Google.Android.Material.Tabs.TabLayoutMediator.ITabConfigurationStrategy.OnConfigureTab(Google.Android.Material.Tabs.TabLayout.Tab Tab, int Position)
    {
        ShellContent Content = SectionController.GetItems()[Position];

        Content.Icon.LoadImage(MauiContext, Finished =>
        {
            var BaseDrawable = Finished?.Value;

            if (BaseDrawable != null)
            {
                using var Constant = BaseDrawable.GetConstantState();
                using var NewDrawable = Constant!.NewDrawable();
                using var IconDrawable = NewDrawable.Mutate();
                IconDrawable.SetColorFilter(SysCredit.Mobile.Controls.TabLayout.GetTabIconTintColor(ShellSection).ToPlatform(Colors.White), FilterMode.SrcAtop);
                Tab.SetIcon(IconDrawable);
            }
        });

        Tab.SetText(Content.Title);
    }

    public override global::Android.Views.View OnCreateView(global::Android.Views.LayoutInflater Inflater, global::Android.Views.ViewGroup Container, Bundle SavedInstanceState)
    {
        var RootView = (AndroidX.CoordinatorLayout.Widget.CoordinatorLayout)base.OnCreateView(Inflater, Container, SavedInstanceState);
        var AppBar = RootView.GetFirstChildOfType<Google.Android.Material.AppBar.AppBarLayout>() ?? throw new ArgumentNullException("RootView.GetFirstChildOfType<Google.Android.Material.AppBar.AppBarLayout>()");

        TabLayout = AppBar.GetFirstChildOfType<Google.Android.Material.Tabs.TabLayout>() ?? throw new ArgumentNullException("AppBar.GetFirstChildOfType<Google.Android.Material.Tabs.TabLayout>()");

        TabLayout.InlineLabel = true;
        TabLayout.TabIndicatorFullWidth = true;
        TabLayout.TabIndicatorAnimationMode = Google.Android.Material.Tabs.TabLayout.IndicatorAnimationModeLinear;
        TabLayout.TabMode = Google.Android.Material.Tabs.TabLayout.ModeFixed;
        TabLayout.TabGravity = Google.Android.Material.Tabs.TabLayout.GravityFill;
        TabLayout.TabSelected += OnTabLayoutTabSelected;
        TabLayout.TabUnselected += OnTabLayoutTabUnselected;

        return RootView;
    }

    private void OnTabLayoutTabUnselected(object? Sender, Google.Android.Material.Tabs.TabLayout.TabUnselectedEventArgs EventInfo)
    {
        EventInfo.Tab?.Icon?.SetColorFilter(SysCredit.Mobile.Controls.TabLayout.GetTabIconTintColor(ShellSection).ToPlatform(Colors.White), FilterMode.SrcAtop);
    }

    private void OnTabLayoutTabSelected(object? Sender, Google.Android.Material.Tabs.TabLayout.TabSelectedEventArgs EventInfo)
    {
        EventInfo.Tab?.Icon?.SetColorFilter(SysCredit.Mobile.Controls.TabLayout.GetSelectedTabIndicatorColor(ShellSection).ToPlatform(Colors.White), FilterMode.SrcAtop);
    }

    public override void OnDestroy()
    {
        TabLayout.TabSelected -= OnTabLayoutTabSelected;
        TabLayout.TabUnselected -= OnTabLayoutTabUnselected;
        base.OnDestroy();
    }
}

internal class CustomToolbarAppearanceTracker : ShellToolbarAppearanceTracker
{
    public CustomToolbarAppearanceTracker(IShellContext ShellContext) : base(ShellContext)
    {
    }

    public override void SetAppearance(AndroidX.AppCompat.Widget.Toolbar Toolbar, IShellToolbarTracker ToolbarTracker, ShellAppearance Appearance)
    {
        base.SetAppearance(Toolbar, ToolbarTracker, Appearance);
        ToolbarTracker.TintColor = Appearance.ForegroundColor;
    }

    public override void ResetAppearance(AndroidX.AppCompat.Widget.Toolbar Toolbar, IShellToolbarTracker ToolbarTracker)
    {
        base.ResetAppearance(Toolbar, ToolbarTracker);
        ToolbarTracker.TintColor = ShellRenderer.DefaultForegroundColor;
    }
}

internal class CustomTabLayoutAppearanceTracker : ShellTabLayoutAppearanceTracker
{
    public ShellSection ShellSection { get; set; } = default!;

    protected IShellContext ShellContext { get; }

    public CustomTabLayoutAppearanceTracker(IShellContext ShellContext) : base(ShellContext)
    {
        this.ShellContext = ShellContext;
    }

    public override void SetAppearance(Google.Android.Material.Tabs.TabLayout TabLayout, ShellAppearance Appearance)
    {
        base.SetAppearance(TabLayout, Appearance);
        TabLayout.SetTabTextColors(Color.White, SysCredit.Mobile.Controls.TabLayout.GetSelectedTabIndicatorColor(ShellSection).ToPlatform(Colors.White));
        TabLayout.SetBackgroundColor(SysCredit.Mobile.Controls.TabLayout.GetBackgroundColor(ShellSection).ToPlatform(Colors.Transparent));
        TabLayout.SetSelectedTabIndicatorColor(SysCredit.Mobile.Controls.TabLayout.GetSelectedTabIndicatorColor(ShellSection).ToPlatform(Colors.White));
    }

    public override void ResetAppearance(Google.Android.Material.Tabs.TabLayout TabLayout)
    {
        base.ResetAppearance(TabLayout);
        TabLayout.TabTextColors = default;
        TabLayout.SetBackgroundColor(Colors.Transparent.ToAndroid());
        TabLayout.SetSelectedTabIndicatorColor(Colors.White.ToAndroid());
    }
}
