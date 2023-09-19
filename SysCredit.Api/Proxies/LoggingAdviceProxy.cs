namespace SysCredit.Api.Proxies;

using SysCredit.Api.Extensions;

using System.Reflection;
using System.Text;

/// <summary>
///     Proxy para informar sobre los métodos que se estan ejecutando en los servicios y controladores.
/// </summary>
/// <remarks>
///     <a href="https://dzone.com/articles/aspect-oriented-programming-in-c-with-realproxy">
///         Aspect Oriented Programming in C# With RealProxy
///     </a>
///     <br />
///     <a href="https://learn.microsoft.com/en-us/archive/msdn-magazine/2014/february/aspect-oriented-programming-aspect-oriented-programming-with-the-realproxy-class">
///         Aspect-Oriented Programming: Aspect-Oriented Programming with the RealProxy Class
///     </a>
///     <br />
///     <a href="https://learn.microsoft.com/es-mx/archive/msdn-magazine/2014/february/aspect-oriented-programming-aspect-oriented-programming-with-the-realproxy-class">
///         Programación orientada a aspectos con la clase RealProxy
///     </a>
///     <br />
///     <a href="https://devblogs.microsoft.com/dotnet/migrating-realproxy-usage-to-dispatchproxy/">
///         Migrating RealProxy Usage to DispatchProxy
///     </a>
/// </remarks>
public class LoggingAdviceProxy<TInterface> : DispatchProxy
{
    private TInterface Decorated = default!;
    private ILogger<TInterface> Logger = default!;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Decorated"></param>
    /// <param name="ServiceProvider"></param>
    /// <returns></returns>
    public static TInterface Create(TInterface? Decorated, IServiceProvider ServiceProvider)
    {
        TInterface Proxy = Create<TInterface, LoggingAdviceProxy<TInterface>>();
        Proxy.As<LoggingAdviceProxy<TInterface>>()!.Initializer(Decorated, ServiceProvider);
        return Proxy;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Decorated"></param>
    /// <param name="ServiceProvider"></param>
    private void Initializer(TInterface? Decorated, IServiceProvider ServiceProvider)
    {
        this.Decorated = Decorated ?? throw new ArgumentNullException(nameof(Decorated));
        this.Logger = ServiceProvider.GetRequiredService<ILogger<TInterface>>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TargetMethod"></param>
    /// <param name="Args"></param>
    /// <returns></returns>
    protected override object? Invoke(MethodInfo? TargetMethod, object?[]? Args)
    {
        try
        {
            try
            {
                LogBefore(TargetMethod!, Args);
            }
            catch (Exception Ex)
            {
                // Do not stop method execution if exception
                LogException(Ex, TargetMethod!);
            }

            var Result = TargetMethod!.Invoke(Decorated, Args);

            try
            {
                LogAfter(TargetMethod, Args, Result);
            }
            catch (Exception Ex)
            {
                // Do not stop method execution if exception
                LogException(Ex, TargetMethod);
            }

            return Result;
        }
        catch (TargetInvocationException Ex)
        {
            LogException(Ex.InnerException ?? Ex, TargetMethod!);
            throw Ex.InnerException ?? Ex;
        }
    }

    private static string GetStringValue(object? @object)
    {
        if (@object is null) return "null";

        var TypeInfo = @object.GetType().GetTypeInfo();

        if (TypeInfo.IsPrimitive || TypeInfo.IsEnum || @object is string)
        {
            return @object.ToString()!;
        }

        try
        {
            return System.Text.Json.JsonSerializer.Serialize(@object);
        }
        catch
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(@object);
            }
            catch
            {
                return @object.ToString()!;
            }
        }
    }

    private void LogException(Exception Exception, MethodInfo MethodInfo)
    {
        try
        {
            var ErrorMessage = new StringBuilder();

            ErrorMessage.AppendLine();
            ErrorMessage.AppendLine($"Class {Decorated!.GetType().FullName}");
            ErrorMessage.AppendLine($"Method {MethodInfo.Name} threw exception");
            ErrorMessage.AppendLine(Exception.GetDescription());

            Logger.LogError(Exception, ErrorMessage.ToString());
        }
        catch
        {
            // Ignored
            // Method should return original exception
        }
    }

    private void LogAfter(MethodInfo MethodInfo, object?[]? Args, object? Result)
    {
        var AfterMessage = new StringBuilder();

        AfterMessage.AppendLine();
        AfterMessage.AppendLine($"Class {Decorated!.GetType().FullName}");
        AfterMessage.AppendLine($"Method {MethodInfo.Name} executed");
        AfterMessage.AppendLine("Output:");
        AfterMessage.AppendLine(GetStringValue(Result));

        var Parameters = MethodInfo.GetParameters();

        if (Parameters.Any())
        {
            AfterMessage.AppendLine("Parameters:");

            for (var Index = 0; Index < Parameters.Length; ++Index)
            {
                var Parameter = Parameters[Index];
                var Arg = Args![Index];

                AfterMessage.AppendLine($"{Parameter.Name}: {GetStringValue(Arg)}");
            }
        }

        Logger.LogInformation(AfterMessage.ToString());
    }

    private void LogBefore(MethodInfo MethodInfo, object?[]? Args)
    {
        var BeforeMessage = new StringBuilder();

        BeforeMessage.AppendLine();
        BeforeMessage.AppendLine($"Class {Decorated!.GetType().FullName}");
        BeforeMessage.AppendLine($"Method {MethodInfo.Name} executing");

        var Parameters = MethodInfo.GetParameters();

        if (Parameters.Any())
        {
            BeforeMessage.AppendLine("Parameters:");

            for (var Index = 0; Index < Parameters.Length; ++Index)
            {
                var Parameter = Parameters[Index];
                var Arg = Args![Index];

                BeforeMessage.AppendLine($"{Parameter.Name}: {GetStringValue(Arg)}");
            }
        }

        Logger.LogInformation(BeforeMessage.ToString());
    }
}

/// <summary>
/// 
/// </summary>
public static class LoggingAdvice
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Interface"></param>
    /// <param name="DecoratedType"></param>
    /// <param name="ServiceProvider"></param>
    /// <returns></returns>
    public static object Create(Type Interface, Type DecoratedType, IServiceProvider ServiceProvider)
    {
        var ConstructorInfo = DecoratedType.GetTypeInfo().DeclaredConstructors.Single();
        var Parameters = ConstructorInfo.GetParameters().Select(ParameterInfo => ServiceProvider.GetRequiredService(ParameterInfo.ParameterType)).ToArray();
        var Decorated = ConstructorInfo.Invoke(Parameters);
        var ProxyInfo = typeof(LoggingAdviceProxy<>).MakeGenericType(Interface).GetTypeInfo();
        var CreateMethodInfo = ProxyInfo.GetMethod(nameof(DispatchProxy.Create), BindingFlags.Static | BindingFlags.Public, new Type[] { Interface, ServiceProvider.GetType() })!;
        return CreateMethodInfo.Invoke(default, new object[] { Decorated, ServiceProvider })!;
    }
}
