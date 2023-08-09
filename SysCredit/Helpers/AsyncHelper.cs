namespace SysCredit.Helpers;

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

public static class AsyncHelper
{
    private static readonly TaskFactory TaskFactory = new TaskFactory(CancellationToken.None,
        TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

    public static TResult RunSync<TResult>(Func<Task<TResult>> Function)
    {
        var CultureUI = CultureInfo.CurrentUICulture;
        var Culture = CultureInfo.CurrentCulture;

        return TaskFactory.StartNew(() =>
        {
            Thread.CurrentThread.CurrentCulture = Culture;
            Thread.CurrentThread.CurrentUICulture = CultureUI;
            return Function();
        }).Unwrap().GetAwaiter().GetResult();
    }

    public static void RunSync(Func<Task> Function)
    {
        var CultureUI = CultureInfo.CurrentUICulture;
        var Culture = CultureInfo.CurrentCulture;

        TaskFactory.StartNew(() =>
        {
            Thread.CurrentThread.CurrentCulture = Culture;
            Thread.CurrentThread.CurrentUICulture = CultureUI;
            return Function();
        }).Unwrap().GetAwaiter().GetResult();
    }
}
