namespace SysCredit;

using Android.App;
using Android.Runtime;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr Handle, JniHandleOwnership Ownership)
        : base(Handle, Ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}