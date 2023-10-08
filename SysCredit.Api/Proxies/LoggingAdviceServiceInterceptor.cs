namespace SysCredit.Api.Proxies;

using Castle.DynamicProxy;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Helpers;

using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

using static Constants.ErrorCodes;
using static Properties.ErrorCodeMessages;
using static System.String;

/// <summary>
///     Logger Proxy para todos los métodos de los servicios.<br />
///     Da información del método que se está ejecutando y los resultados del mismo.<br />
///     Transforma todas las excepciones en un <see cref="EndpointFlowException" /> y <see cref="ProxyException" />.
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
    public void Intercept(IInvocation Invocation)
    {
        BeforeProceed(Invocation);
        Invocation.Proceed();

        if (IsAsyncMethod(Invocation.MethodInvocationTarget))
        {
            Invocation.ReturnValue = InterceptAsync((dynamic)Invocation.ReturnValue, Invocation);
        }
        else
        {
            AfterProceedSync(Invocation);
        }
    }

    /// <summary>
    ///     Crea un <see cref="ProxyException" /> desde alguna excepción generalizada.
    /// </summary>
    /// <param name="ErrorCode">
    ///     Código de error asociado al error que se ha producido.
    /// </param>
    /// <param name="Invocation">
    ///     Información del método de servicio actual en ejecución.
    /// </param>
    /// <param name="InnerException">
    ///     Excepción que será usada para crear el <see cref="ProxyException" />.
    /// </param>
    /// <returns>
    ///     Regresa un <see cref="ProxyException" /> con información detallada del error actual.
    /// </returns>
    private static ProxyException CreateProxyException(string ErrorCode, IInvocation Invocation, Exception InnerException)
    {
        return new ProxyException(GetMessageFromCode(ErrorCode), InnerException)
        {
            Data =
            {
                [SysCreditConstants.ErrorCodeKey]      = ErrorCode,
                [SysCreditConstants.TypeFullNameKey]   = Invocation.TargetType.ToString(),
                [SysCreditConstants.MethodFullNameKey] = Invocation.MethodInvocationTarget.ToString(),
                [SysCreditConstants.MethodIdKey]       = Invocation.MethodInvocationTarget.GetMethodId(),
                [SysCreditConstants.ErrorCategoryKey]  = Invocation.MethodInvocationTarget.GetErrorCategory()
            }
        };
    }

    /// <summary>
    ///     Crea un <see cref="EndpointFlowException" /> desde alguna excepción generalizada.
    /// </summary>
    /// <param name="Invocation">
    ///     Información del método de servicio actual en ejecución.
    /// </param>
    /// <param name="ValidationResult">
    ///     Resultados de validar un objeto de tipo <see cref="Requests.IRequest" />.
    /// </param>
    /// <returns>
    ///     Regresa un <see cref="EndpointFlowException" /> con información detallada del error actual.
    /// </returns>
    private static EndpointFlowException CreateEndpointFlowException(IInvocation Invocation, ValidationException ValidationResult)
    {
        return new EndpointFlowException(new ErrorStatus
        {
            MethodId = Invocation.MethodInvocationTarget.GetMethodId(),
            ErrorCode = Invocation.MethodInvocationTarget.GetValidationErrorCode(),
            ErrorMessage = Format(Invocation.MethodInvocationTarget.GetValidationErrorCodeMessage()!, ValidationResult.ValidatedInstanceType),
            ErrorCategory = Invocation.MethodInvocationTarget.GetErrorCategory(),
            Errors = ValidationResult.ValidationResult.ErrorsToDictionaryWithErrorCode(),
        }, ValidationResult);
    }

    /// <summary>
    ///     Verifica si un método regresa una tarea asincrona.
    /// </summary>
    /// <param name="Method">
    ///     Método que será verificado.
    /// </param>
    /// <returns>
    ///     Regresa <see cref="bool">True</see> si el método regresa una tarea asincrona y <see cref="bool">False</see> si no.
    /// </returns>
    private static bool CheckMethodReturnTypeIsTaskType(MethodInfo Method)
    {
        Type ReturnType = Method.ReturnType;

        if (ReturnType.IsGenericType)
        {
            if (ReturnType.IsInterface && ReturnType.GetGenericTypeDefinition() == typeof(IAsyncEnumerable<>))
            {
                return true;
            }
            else if (ReturnType.GetGenericTypeDefinition() == typeof(Task<>) || ReturnType.GetGenericTypeDefinition() == typeof(ValueTask<>))
            {
                return true;
            }
        }
        else if (ReturnType == typeof(Task) || ReturnType == typeof(ValueTask))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Verifica si un método es asincrono.
    /// </summary>
    /// <param name="Method">
    ///     Método que será verificado.
    /// </param>
    /// <returns>
    ///     Regresa True si el método es asincrono en caso contrario False.
    /// </returns>
    private static bool IsAsyncMethod(MethodInfo Method)
    {
        bool IsAsyncStateMachine = Attribute.IsDefined(Method, typeof(AsyncStateMachineAttribute), inherit: false);
        bool IsTaskType = CheckMethodReturnTypeIsTaskType(Method);

        return IsAsyncStateMachine && IsTaskType;
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
            await AfterProceedAsync(Invocation, ProceedAsyncResult: null);
        }
        catch (ValidationException ValidationResult)
        {
            throw CreateEndpointFlowException(Invocation, ValidationResult);
        }
        catch (Exception Exception) when (Exception is not SqlException)
        {
            throw CreateProxyException(LoggingAdviceServiceInterceptorErrorCode, Invocation, Exception);
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
            throw CreateEndpointFlowException(Invocation, ValidationResult);
        }
        catch (Exception Exception) when (Exception is not SqlException)
        {
            throw CreateProxyException(LoggingAdviceServiceInterceptorErrorCode, Invocation, Exception);
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
            await AfterProceedAsync(Invocation, ProceedAsyncResult: null);
        }
        catch (ValidationException ValidationResult)
        {
            throw CreateEndpointFlowException(Invocation, ValidationResult);
        }
        catch (Exception Exception) when (Exception is not SqlException)
        {
            throw CreateProxyException(LoggingAdviceServiceInterceptorErrorCode, Invocation, Exception);
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
            throw CreateEndpointFlowException(Invocation, ValidationResult);
        }
        catch (Exception Exception) when (Exception is not SqlException)
        {
            throw CreateProxyException(LoggingAdviceServiceInterceptorErrorCode, Invocation, Exception);
        }
    }

    /// <summary>
    ///     Método de intercepción que será invocado según el tipo de <see cref="IInvocation.ReturnValue" />.
    /// </summary>
    /// <typeparam name="TSource">
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
    /// <param name="HasAsynResult">
    ///     Parámetro que indica si el método tiene resultados.
    /// </param>
    /// <returns>
    ///     Representa una operación asincrona sin resultados.
    /// </returns>
    private async Task AfterProceedAsync(IInvocation Invocation, dynamic? ProceedAsyncResult = null)
    {
        string Result = await GetStringOfAsync(ProceedAsyncResult);
        ServiceLogger.LogInformation("\n[SERVICE: Result] {MethodInfo}\nReturn: {Result}", Invocation.MethodInvocationTarget, Result);
    }

    /// <inheritdoc cref="GetStringOf(object?)" />
    private static ValueTask<string> GetStringOfAsync(object? @object)
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
                PropertyNamingPolicy = JsonDefaultNamingPolicy.DefaultNamingPolicy,
                WriteIndented = true
            });
        }
        catch
        {
            try
            {
                // Serializar ha texto usando una implementación de terceros.
                return Newtonsoft.Json.JsonConvert.SerializeObject(@object, Newtonsoft.Json.Formatting.Indented);
            }
            catch
            {
                // Regresar un texto usando la implementación .ToString() del objeto.
                return @object.ToString()!;
            }
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
    public static object Create(Type Interface, Type TargetType, IServiceProvider ServiceProvider)
    {
        var TargetConstructor = TargetType.GetTypeInfo().DeclaredConstructors.Single();
        var Parameters = TargetConstructor.GetParameters().Select(ParameterInfo => ServiceProvider.GetRequiredService(ParameterInfo.ParameterType)).ToArray();
        var Target = TargetConstructor.Invoke(Parameters);

        var ProxyGenerator = ServiceProvider.GetRequiredService<ProxyGenerator>();
        var ServiceLogger = ServiceProvider.GetRequiredService(typeof(ILogger<>).MakeGenericType(TargetType)).As<ILogger>()!;

        return ProxyGenerator.CreateInterfaceProxyWithTargetInterface(Interface, Target, new LoggingAdviceServiceInterceptor(ServiceLogger));
    }
}
