namespace SysCredit.Controls;

public static class TabLayout
{
    public static readonly BindableProperty SelectedTabIndicatorColorProperty = BindableProperty.CreateAttached("SelectedTabIndicatorColor", typeof(Color), typeof(TabLayout), Colors.White);

    public static readonly BindableProperty BackgroundColorProperty = BindableProperty.CreateAttached("BackgroundColor", typeof(Color), typeof(TabLayout), Colors.Transparent);

    public static readonly BindableProperty TabIconTintColorProperty = BindableProperty.CreateAttached("TabIconTintColor", typeof(Color), typeof(TabLayout), Colors.White);


    public static Color GetSelectedTabIndicatorColor(BindableObject TabShellSection)
    {
        return (Color)TabShellSection.GetValue(SelectedTabIndicatorColorProperty);
    }

    public static void SetSelectedTabIndicatorColor(BindableObject TabShellSection, Color Value)
    {
        TabShellSection.SetValue(SelectedTabIndicatorColorProperty, Value);
    }

    public static Color GetBackgroundColor(BindableObject TabShellSection)
    {
        return (Color)TabShellSection.GetValue(BackgroundColorProperty);
    }

    public static void SetBackgroundColor(BindableObject TabShellSection, Color Value)
    {
        TabShellSection.SetValue(BackgroundColorProperty, Value);
    }

    public static Color GetTabIconTintColor(BindableObject TabShellSection)
    {
        return (Color)TabShellSection.GetValue(TabIconTintColorProperty);
    }

    public static void SetTabIconTintColor(BindableObject TabShellSection, Color Value)
    {
        TabShellSection.SetValue(TabIconTintColorProperty, Value);
    }
}
