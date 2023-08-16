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

    // ValueTask and ValueTask<T> do not have to support blocking on a call to GetResult when backed by an IValueTaskSource or IValueTaskSource<T> implementation.
    // Convert to a Task or Task<T> to do so in case the task hasn't completed yet.

    public static void Wait(ValueTask Task)
    {
        var Awaiter = Task.GetAwaiter();

        if (!Awaiter.IsCompleted)
        {
            Task.AsTask().GetAwaiter().GetResult();
            return;
        }

        Awaiter.GetResult();
    }

    public static T Wait<T>(ValueTask<T> Task)
    {
        var Awaiter = Task.GetAwaiter();

        if (!Awaiter.IsCompleted)
        {
            return Task.AsTask().GetAwaiter().GetResult();
        }

        return Awaiter.GetResult();
    }
}
