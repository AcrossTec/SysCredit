namespace SysCredit.Mobile;

using Android.App;
using Android.Runtime;

[Application]
public class MainApplication(IntPtr Handle, JniHandleOwnership Ownership) : MauiApplication(Handle, Ownership)
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}