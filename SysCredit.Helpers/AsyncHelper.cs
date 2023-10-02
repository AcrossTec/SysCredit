namespace SysCredit.Helpers;

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Ofrece algunos métodos de ayuda para convertir una operación asincrona en sincrona.
/// </summary>
public static class AsyncHelper
{
    /// <summary>
    ///     Fábrica de Tasks para con argumentos de creación por defecto.
    /// </summary>
    private static readonly TaskFactory TaskFactory = new TaskFactory(CancellationToken.None,
        TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

    /// <summary>
    ///     Ejecuta una tarea asincrona como una operación sincrona.
    /// </summary>
    /// <typeparam name="TResult">
    ///     Tipo del resultado de la operación asincrona como sincrona.
    /// </typeparam>
    /// <param name="Function">
    ///     Método que regresa alguna tarea asincrona que se ejecutará como sincrona.
    /// </param>
    /// <returns>
    ///     Regresa el resultado de la operación sincrona.
    /// </returns>
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

    /// <summary>
    ///     Ejecuta una tarea asincrona como una operación sincrona.
    /// </summary>
    /// <param name="Function">
    ///     Método que regresa alguna tarea asincrona que se ejecutará como sincrona.
    /// </param>
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

    /// <summary>
    ///     Espera el resultado de un ValueTask como una operación sincrona.
    /// </summary>
    /// <param name="Task">
    ///     Objeto Task para esperar sus resultados.
    /// </param>
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

    /// <summary>
    ///     Espera el resultado de un ValueTask como una operación sincrona.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo del resultado de la tarea asincrona.
    /// </typeparam>
    /// <param name="Task">
    ///     Objeto Task para esperar sus resultados.
    /// </param>
    /// <returns>
    ///     Regresa el resultado de una tarea asincrona como sincrona.
    /// </returns>
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
