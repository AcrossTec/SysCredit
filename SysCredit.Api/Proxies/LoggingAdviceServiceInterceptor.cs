namespace SysCredit.Api.Proxies;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

using Castle.DynamicProxy;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Helpers;

using static System.String;

/// <summary>
///     Logger Proxy para todos los métodos de los servicios.<br />
///     Da información del método que se está ejecutando y los resultados del mismo.<br />
///     Transforma todas las excepciones en un <see cref="ServiceException" />.
/// </summary>
/// <param name="ServiceLogger"></param>
public class LoggingAdviceServiceInterceptor(ILogger ServiceLogger) : IInterceptor
{
    /// <summary>
    ///     Método que intersepta los métodos de los servicios marcados por <see cref="MethodIdAttribute" />.
    /// </summary>
    /// <param name="Invocation">
    ///     Información del método de servicio actual en ejecución.
    /// </param>
    [RequiresUnreferencedCode("Uso de código dinámico para poder deducir el tipo correcto para Invocation.ReturnValue.")]
    [UnconditionalSuppressMessage("Trimming", "IL2026:Using dynamic types might cause types or members to be removed by trimmer.", Justification = "<Pending>")]
    [SuppressMessage("Trimming", "IL2046:'RequiresUnreferencedCodeAttribute' annotations must match across all interface implementations or overrides.", Justification = "<Pending>")]
    public void Intercept(IInvocation Invocation)
    {
        BeforeProceed(Invocation);
        Invocation.Proceed();

        if (Invocation.MethodInvocationTarget.IsAsyncMethod())
        {
            Invocation.ReturnValue = InterceptAsync((dynamic)Invocation.ReturnValue, Invocation);
        }
        else
        {
            AfterProceedSync(Invocation);
        }
    }

    /// <summary>
    ///     Crea un <see cref="ServiceException" /> desde alguna excepción generalizada.
    /// </summary>
    /// <param name="Invocation">
    ///     Información del método de servicio actual en ejecución.
    /// </param>
    /// <param name="Exception">
    ///     Excepción que será usada para crear el <see cref="ServiceException" />.
    /// </param>
    /// <returns>
    ///     Regresa un <see cref="ServiceException" /> con información detallada del error actual.
    /// </returns>
    private static ServiceException CreateServiceException(IInvocation Invocation, Exception Exception)
    {
        Exception = Exception.CreateExceptionUsingMethodInfo<ServiceException>(Invocation.MethodInvocationTarget, Status =>
        {
            Status.ErrorCode = Invocation.MethodInvocationTarget.GetServiceExecuteErrorCode();
            Status.ErrorMessage = Invocation.MethodInvocationTarget.GetServiceExecuteErrorCodeMessage();
        });

        Exception.Data[SysCreditConstants.IsFromValidationExceptionKey] = false;
        return Exception.As<ServiceException>()!;
    }

    /// <summary>
    ///     Crea un <see cref="ServiceException" /> desde alguna excepción generalizada.
    /// </summary>
    /// <param name="Invocation">
    ///     Información del método de servicio actual en ejecución.
    /// </param>
    /// <param name="ValidationResult">
    ///     Resultados de validar un objeto de tipo <see cref="Requests.IRequest" />.
    /// </param>
    /// <returns>
    ///     Regresa un <see cref="ServiceException" /> con información detallada del error actual.
    /// </returns>
    private static ServiceException CreateServiceException(IInvocation Invocation, ValidationException ValidationResult)
    {
        var Exception = ValidationResult.CreateExceptionUsingMethodInfo<ServiceException>(Invocation.MethodInvocationTarget, Status =>
        {
            Status.Errors = ValidationResult.ValidationResult.ErrorsToDictionaryWithErrorCode();
            Status.ErrorCode = Invocation.MethodInvocationTarget.GetServiceValidationErrorCode();
            Status.ErrorMessage = Invocation.MethodInvocationTarget.GetServiceValidationErrorCodeMessage();
            Status.Extensions[SysCreditConstants.ValidatedInstanceKey] = ValidationResult.ValidatedInstanceType.ToString();
        });

        Exception.Data[SysCreditConstants.IsFromValidationExceptionKey] = true;
        return Exception;
    }

    /// <summary>
    ///     Método de intercepción que será invocado según el tipo de <see cref="IInvocation.ReturnValue" />.
    /// </summary>
    /// <param name="Task">
    ///     Tarea que representa el resultado del método asincrono.
    /// </param>
    /// <param name="Invocation">
    ///     Información detallada del método que se está interceptando.
    /// </param>
    /// <returns>
    ///     Regresa una nueva tarea con la misma información del método interceptado.
    /// </returns>
    private async Task InterceptAsync(Task Task, IInvocation Invocation)
    {
        try
        {
            await Task.ConfigureAwait(continueOnCapturedContext: false);
            await AfterProceedAsync(Invocation, ProceedAsyncResult: default(object));
        }
        catch (ValidationException ValidationResult)
        {
            throw CreateServiceException(Invocation, ValidationResult);
        }
        catch (Exception Exception) when (Exception is not StoreException)
        {
            throw CreateServiceException(Invocation, Exception);
        }
    }

    /// <summary>
    ///     Método de intercepción que será invocado según el tipo de <see cref="IInvocation.ReturnValue" />.
    /// </summary>
    /// <typeparam name="TResult">
    ///     Tipo del resultado de la tarea.
    /// </typeparam>
    /// <param name="Task">
    ///     Tarea que representa el resultado del método asincrono.
    /// </param>
    /// <param name="Invocation">
    ///     Información detallada del método que se está interceptando.
    /// </param>
    /// <returns>
    ///     Regresa una nueva tarea con la misma información del método interceptado.
    /// </returns>
    private async Task<TResult?> InterceptAsync<TResult>(Task<TResult> Task, IInvocation Invocation)
    {
        try
        {
            TResult? ProceedAsyncResult = await Task.ConfigureAwait(continueOnCapturedContext: false);
            await AfterProceedAsync(Invocation, ProceedAsyncResult);
            return ProceedAsyncResult;
        }
        catch (ValidationException ValidationResult)
        {
            throw CreateServiceException(Invocation, ValidationResult);
        }
        catch (Exception Exception) when (Exception is not StoreException)
        {
            throw CreateServiceException(Invocation, Exception);
        }
    }

    /// <summary>
    ///     Método de intercepción que será invocado según el tipo de <see cref="IInvocation.ReturnValue" />.
    /// </summary>
    /// <param name="Task">
    ///     Tarea que representa el resultado del método asincrono.
    /// </param>
    /// <param name="Invocation">
    ///     Información detallada del método que se está interceptando.
    /// </param>
    /// <returns>
    ///     Regresa una nueva tarea con la misma información del método interceptado.
    /// </returns>
    private async ValueTask InterceptAsync(ValueTask Task, IInvocation Invocation)
    {
        try
        {
            await Task.ConfigureAwait(continueOnCapturedContext: false);
            await AfterProceedAsync(Invocation, ProceedAsyncResult: default(object));
        }
        catch (ValidationException ValidationResult)
        {
            throw CreateServiceException(Invocation, ValidationResult);
        }
        catch (Exception Exception) when (Exception is not StoreException)
        {
            throw CreateServiceException(Invocation, Exception);
        }
    }

    /// <summary>
    ///     Método de intercepción que será invocado según el tipo de <see cref="IInvocation.ReturnValue" />.
    /// </summary>
    /// <typeparam name="TResult">
    ///     Tipo del resultado de la tarea.
    /// </typeparam>
    /// <param name="Task">
    ///     Tarea que representa el resultado del método asincrono.
    /// </param>
    /// <param name="Invocation">
    ///     Información detallada del método que se está interceptando.
    /// </param>
    /// <returns>
    ///     Regresa una nueva tarea con la misma información del método interceptado.
    /// </returns>
    private async ValueTask<TResult?> InterceptAsync<TResult>(ValueTask<TResult> Task, IInvocation Invocation)
    {
        try
        {
            var ProceedAsyncResult = await Task.ConfigureAwait(continueOnCapturedContext: false);
            await AfterProceedAsync(Invocation, ProceedAsyncResult);
            return ProceedAsyncResult;
        }
        catch (ValidationException ValidationResult)
        {
            throw CreateServiceException(Invocation, ValidationResult);
        }
        catch (Exception Exception) when (Exception is not StoreException)
        {
            throw CreateServiceException(Invocation, Exception);
        }
    }

    /// <summary>
    ///     Método de intercepción que será invocado según el tipo de <see cref="IInvocation.ReturnValue" />.
    /// </summary>
    /// <typeparam name="TSource">
    ///     Tipo del resultado de la tarea.
    /// </typeparam>
    /// <param name="Source">
    ///     Colección asincrona que esta siendo interseptada.
    /// </param>
    /// <param name="Invocation">
    ///     Información detallada del método que se está interceptando.
    /// </param>
    /// <returns>
    ///     Regresa una nueva tarea con la misma información del método interceptado.
    /// </returns>
    private IAsyncEnumerable<TSource> InterceptAsync<TSource>(IAsyncEnumerable<TSource> Source, IInvocation Invocation)
    {
        ServiceLogger.LogInformation("\n[SERVICE: Result] {MethodInfo}\n{Args}", Invocation.MethodInvocationTarget, CreateStringLogParameters(Invocation));
        return Source;
    }

    /// <summary>
    ///     Log que se ejecuta antes de invocar el método interceptado.
    /// </summary>
    /// <param name="Invocation">
    ///     Objeto que posee toda la información relacionada al método que se está interceptando.
    /// </param>
    private void BeforeProceed(IInvocation Invocation)
    {
        ServiceLogger.LogInformation("\n[SERVICE: Invoke] {MethodInfo}\n{Args}", Invocation.MethodInvocationTarget, CreateStringLogParameters(Invocation));
    }

    /// <summary>
    ///     Log que se ejecuta después de invocar el método interceptado.
    /// </summary>
    /// <param name="Invocation">
    ///     Objeto que posee toda la información relacionada al método que se está interceptando.
    /// </param>
    private void AfterProceedSync(IInvocation Invocation)
    {
        string Result = Invocation.MethodInvocationTarget.ReturnType == typeof(void) ? "void" : GetStringOf(Invocation.ReturnValue);
        ServiceLogger.LogInformation("\n[SERVICE: Result] {MethodInfo}\nReturn: {Result}", Invocation.MethodInvocationTarget, Result);
    }

    /// <summary>
    ///     Log que se ejecuta después de invocar el método asincrono interceptado.
    /// </summary>
    /// <param name="Invocation">
    ///     Objeto que posee toda la información relacionada al método que se está interceptando.
    /// </param>
    /// <param name="ProceedAsyncResult">
    ///     Parámetro que indica si el método tiene resultados.
    /// </param>
    /// <returns>
    ///     Representa una operación asincrona sin resultados.
    /// </returns>
    private async Task AfterProceedAsync<TProceedAsyncResult>(IInvocation Invocation, TProceedAsyncResult? ProceedAsyncResult = default)
    {
        string Result = await GetStringOfAsync(ProceedAsyncResult);
        ServiceLogger.LogInformation("\n[SERVICE: Result] {MethodInfo}\nReturn: {Result}", Invocation.MethodInvocationTarget, Result);
    }

    /// <inheritdoc cref="GetStringOf{TObject}" />
    private static ValueTask<string> GetStringOfAsync<TObject>(TObject? @object)
    {
        return ValueTask.FromResult(GetStringOf(@object));
    }

    /// <summary>
    ///     Obtiene la representación en cadena del objeto <paramref name="object" />.
    /// </summary>
    /// <param name="object">
    ///     Objeto que se va ha convertir en string.
    /// </param>
    /// <returns>
    ///     Regresa un objeto representado como texto.
    /// </returns>
    private static string GetStringOf<TObject>(TObject? @object)
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
            var JsonTypeInfo = SysCreditSerializerContext.Default.GetTypeInfo(TypeInfo)!;

            JsonTypeInfo.Options.WriteIndented = true;
            JsonTypeInfo.Options.PropertyNamingPolicy = JsonDefaultNamingPolicy.DefaultNamingPolicy;
            JsonTypeInfo.Options.TypeInfoResolverChain.Add(SysCreditSerializerContext.Default);

            return JsonSerializer.Serialize(@object, JsonTypeInfo);
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
    /// <param name="Invocation">
    ///     Objeto con la información completa del método original del Proxy y sus argumentos.
    /// </param>
    /// <returns>
    ///     Regresa un cadena con la información de los argumentos.
    /// </returns>
    private static string CreateStringLogParameters(IInvocation Invocation)
    {
        return Join(Environment.NewLine, Invocation.MethodInvocationTarget.GetParameters()
            .Select((ParameterInfo, Index) => $"{ParameterInfo.Name} {GetStringOf(Invocation.Arguments[Index])}"));
    }

    /// <summary>
    ///     Método de fabricación que construye un objecto de tipo <see cref="LoggingAdviceServiceInterceptor" />.
    /// </summary>
    /// <param name="Interface">
    ///     Interfaz que implementa el tipo <paramref name="TargetType" />.
    /// </param>
    /// <param name="TargetType">
    ///     Tipo del objeto al que se le está creando el Proxy.
    /// </param>
    /// <param name="ServiceProvider">
    ///     Interfaz de servicio IoC de Asp .Net
    /// </param>
    /// <returns>
    ///     Regresa un Proxy que implementa la clase base <see cref="Lightwind.AsyncInterceptor.AsyncInterceptorBase" />.
    /// </returns>
    [RequiresDynamicCode("\"Create\" usa código dinámico para crear el proxy en Runtime.")]
    public static object Create(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type Interface,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] Type TargetType,
        IServiceProvider ServiceProvider)
    {
        var TargetConstructor = TargetType.GetTypeInfo().DeclaredConstructors.Single();
        var Parameters = TargetConstructor.GetParameters().Select(ParameterInfo => ServiceProvider.GetRequiredService(ParameterInfo.ParameterType)).ToArray();
        var Target = TargetConstructor.Invoke(Parameters);

        var ProxyGenerator = ServiceProvider.GetRequiredService<ProxyGenerator>();
        var ServiceLogger = ServiceProvider.GetRequiredService(typeof(ILogger<>).MakeGenericType(TargetType)).As<ILogger>()!;

        return ProxyGenerator.CreateInterfaceProxyWithTargetInterface(Interface, Target, new LoggingAdviceServiceInterceptor(ServiceLogger));
    }
}
