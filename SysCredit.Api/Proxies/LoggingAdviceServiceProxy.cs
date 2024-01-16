namespace SysCredit.Api.Proxies;

using System.Reflection;
using System.Text.Json;

using SysCredit.Api.Extensions;
using SysCredit.Helpers;

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
[Obsolete("Use LoggingAdviceServiceInterceptor Class", error: true)]
public class LoggingAdviceServiceProxy<TInterface> : DispatchProxy
{
    private TInterface Decorated = default!;
    private ILogger<TInterface> DecoratedLogger = default!;
    private ILogger<LoggingAdviceServiceProxy<TInterface>> ProxyLogger = default!;

    /// <summary>
    ///     Crear un proxy para el tipo <typeparamref name="TInterface" />.
    /// </summary>
    /// <param name="Decorated">
    ///     Objeto al que se le está creando el proxy.
    /// </param>
    /// <param name="ServiceProvider">
    ///     Interfaz de servicio IoC de Asp .Net
    /// </param>
    /// <returns></returns>
    public static TInterface Create(TInterface? Decorated, IServiceProvider ServiceProvider)
    {
        TInterface Proxy = Create<TInterface, LoggingAdviceServiceProxy<TInterface>>();
        Proxy.As<LoggingAdviceServiceProxy<TInterface>>()!.Initializer(Decorated, ServiceProvider);
        return Proxy;
    }

    /// <summary>
    ///     Sirve como constructor del proxy que se está creando.
    /// </summary>
    /// <param name="Decorated">
    ///     Objeto al que se le va ha crear el proxy.
    /// </param>
    /// <param name="ServiceProvider">
    ///     Interfaz de servicio IoC de Asp .Net
    /// </param>
    private void Initializer(TInterface? Decorated, IServiceProvider ServiceProvider)
    {
        this.Decorated = Decorated ?? throw new ArgumentNullException(nameof(Decorated));
        this.DecoratedLogger = ServiceProvider.GetRequiredService<ILogger<TInterface>>();
        this.ProxyLogger = ServiceProvider.GetRequiredService<ILogger<LoggingAdviceServiceProxy<TInterface>>>();
    }

    /// <summary>
    ///     Método Proxy que es invocado cuando es llamado cualquier método o propiedad de <typeparamref name="TInterface" />.
    /// </summary>
    /// <param name="TargetMethod">
    ///     <see cref="MemberInfo" /> del método original al que se le creó el proxy.
    /// </param>
    /// <param name="Args">
    ///     Argumentos usandos al invocar el método o propuedad de <typeparamref name="TInterface" />.
    /// </param>
    /// <returns>
    ///     Regresa el resultado procesado de alguna posible invocacion de <paramref name="TargetMethod" />.
    /// </returns>
    protected override object? Invoke(MethodInfo? TargetMethod, object?[]? Args)
    {
        DecoratedLogger.LogInformation("\n[SERVICE] {MethodInfo}\n{Args}", TargetMethod, CreateStringLogParameters(TargetMethod!, Args));
        var ServiceResult = TargetMethod!.Invoke(Decorated, Args);
        return ServiceResult;
    }

    /// <summary>
    ///     Obtiene la representación en cadena de un objeto.
    /// </summary>
    /// <param name="object">
    ///     Objeto que se va ha convertir ha string.
    /// </param>
    /// <returns>
    ///     Regresa un objeto como un texto.
    /// </returns>
    private static string GetStringOf(object? @object)
    {
        if (@object is null) return "null";

        var TypeInfo = @object.GetType().GetTypeInfo();

        if (TypeInfo.IsPrimitive || TypeInfo.IsEnum || @object is string)
        {
            return @object.ToString()!;
        }

        try
        {
            // Serializar ha texto usando la implementacion nativa de DotNet.
            return JsonSerializer.Serialize(@object, new JsonSerializerOptions
            {
                WriteIndented = true,
                TypeInfoResolver = SysCreditSerializerContext.Default,
                PropertyNamingPolicy = JsonDefaultNamingPolicy.DefaultNamingPolicy
            });
        }
        catch
        {
            // Regresar un texto usando la implementación .ToString() del objeto.
            return @object.ToString()!;
        }
    }

    /// <summary>
    ///     Obtiene una cadena para ser usanda como log de los argumentos de un método.
    /// </summary>
    /// <param name="MethodInfo">
    ///     Información del método original del proxy.
    /// </param>
    /// <param name="Args">
    ///     Argumentos pasados la método desde el proxy.
    /// </param>
    /// <returns>
    ///     Regresa un cadena con la información de los argumentos.
    /// </returns>
    private string CreateStringLogParameters(MethodInfo MethodInfo, object?[]? Args)
    {
        return string.Join(Environment.NewLine,
            MethodInfo.GetParameters().Select((ParameterInfo, Index) => $"{ParameterInfo.Name} {GetStringOf(Args![Index])}"));
    }
}

/// <summary>
///     Clase de fábrica para crear un <see cref="LoggingAdviceServiceProxy{TInterface}" />.
/// </summary>
/// <seealso cref="LoggingAdviceServiceProxy{TInterface}" />
[Obsolete("Use LoggingAdviceServiceInterceptor.Create Method", error: true)]
public static class LoggingAdviceService
{
    /// <summary>
    ///     Método de fabricación que construye un objecto de tipo <see cref="LoggingAdviceServiceProxy{TInterface}" />.
    /// </summary>
    /// <param name="Interface">
    ///     Interfaz que implementa el tipo <paramref name="DecoratedType" />.
    /// </param>
    /// <param name="DecoratedType">
    ///     Tipo del objeto al que se le está creando el Proxy.
    /// </param>
    /// <param name="ServiceProvider">
    ///     Interfaz de servicio IoC de Asp .Net
    /// </param>
    /// <returns>
    ///     Regresa un Proxy que implementa la clase base <see cref="DispatchProxy" />.
    /// </returns>
    public static object Create(Type Interface, Type DecoratedType, IServiceProvider ServiceProvider)
    {
        var ConstructorInfo = DecoratedType.GetTypeInfo().DeclaredConstructors.Single();
        var Parameters = ConstructorInfo.GetParameters().Select(ParameterInfo => ServiceProvider.GetRequiredService(ParameterInfo.ParameterType)).ToArray();
        var Decorated = ConstructorInfo.Invoke(Parameters);
        var ProxyInfo = typeof(LoggingAdviceServiceProxy<>).MakeGenericType(Interface).GetTypeInfo();
        var CreateMethodInfo = ProxyInfo.GetMethod(nameof(DispatchProxy.Create), BindingFlags.Static | BindingFlags.Public, new Type[] { Interface, ServiceProvider.GetType() })!;
        return CreateMethodInfo.Invoke(default, new object[] { Decorated, ServiceProvider })!;
    }
}
