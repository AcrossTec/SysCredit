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

/// <summary>
///     Logger Proxy para todos los métodos de los servicios.<br />
///     Da información del método que se está ejecutando y los resultados del mismo.<br />
///     Transforma todas las excepciones en un <see cref="EndpointFlowException" /> y <see cref="ProxyException" />.
/// </summary>
/// <param name="ServiceLogger"></param>
public class LoggingAdviceServiceInterceptor(ILogger ServiceLogger) : IInterceptor
{
    /// <summary>
    ///     Guarda el resultado del método actual ejecutado.
    /// </summary>
    protected object? ProceedAsyncResult { get; set; }

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
        return new ProxyException(GetMessageFromCode(ErrorCode)!, InnerException)
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
    /// <param name="Invocation"></param>
    /// <param name="ValidationExceptionResult"></param>
    /// <returns></returns>
    private static EndpointFlowException CreateEndpointFlowException(IInvocation Invocation, ValidationException ValidationExceptionResult)
    {
        return new EndpointFlowException(new ErrorStatus
        {
            MethodId = Invocation.MethodInvocationTarget.GetMethodId(),
            ErrorCode = Invocation.MethodInvocationTarget.GetValidationErrorCode(),
            ErrorMessage = Invocation.MethodInvocationTarget.GetValidationErrorCodeMessage(),
            ErrorCategory = Invocation.MethodInvocationTarget.GetErrorCategory(),
            Errors = ValidationExceptionResult.ValidationResult.ErrorsToDictionaryWithErrorCode(),
        }, ValidationExceptionResult);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Method"></param>
    /// <returns></returns>
    private static bool CheckMethodReturnTypeIsTaskType(MethodInfo Method)
    {
        Type returnType = Method.ReturnType;

        if (returnType.IsGenericType)
        {
            if (returnType.GetGenericTypeDefinition() == typeof(Task<>) || returnType.GetGenericTypeDefinition() == typeof(ValueTask<>))
            {
                return true;
            }
        }
        else if (returnType == typeof(Task) || returnType == typeof(ValueTask))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Method"></param>
    /// <returns></returns>
    private static bool IsAsyncMethod(MethodInfo Method)
    {
        bool IsAsyncStateMachine = Attribute.IsDefined(Method, typeof(AsyncStateMachineAttribute), inherit: false);
        bool IsTaskType = CheckMethodReturnTypeIsTaskType(Method);

        return IsAsyncStateMachine && IsTaskType;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Task"></param>
    /// <param name="Invocation"></param>
    /// <returns></returns>
    private async Task InterceptAsync(Task Task, IInvocation Invocation)
    {
        try
        {
            await Task.ConfigureAwait(continueOnCapturedContext: false);
            await AfterProceedAsync(Invocation, HasAsynResult: false);
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
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Task"></param>
    /// <param name="Invocation"></param>
    /// <returns></returns>
    private async Task<TResult?> InterceptAsync<TResult>(Task<TResult> Task, IInvocation Invocation)
    {
        try
        {
            ProceedAsyncResult = await Task.ConfigureAwait(continueOnCapturedContext: false);
            await AfterProceedAsync(Invocation, HasAsynResult: true);
            return (TResult?)ProceedAsyncResult;
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
    /// 
    /// </summary>
    /// <param name="Task"></param>
    /// <param name="Invocation"></param>
    /// <returns></returns>
    private async ValueTask InterceptAsync(ValueTask Task, IInvocation Invocation)
    {
        try
        {
            await Task.ConfigureAwait(continueOnCapturedContext: false);
            await AfterProceedAsync(Invocation, HasAsynResult: false);
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
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Task"></param>
    /// <param name="Invocation"></param>
    /// <returns></returns>
    private async ValueTask<TResult?> InterceptAsync<TResult>(ValueTask<TResult> Task, IInvocation Invocation)
    {
        try
        {
            ProceedAsyncResult = await Task.ConfigureAwait(continueOnCapturedContext: false);
            await AfterProceedAsync(Invocation, HasAsynResult: true);
            return (TResult?)ProceedAsyncResult;
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
    /// 
    /// </summary>
    /// <param name="Invocation"></param>
    private void BeforeProceed(IInvocation Invocation)
    {
        ServiceLogger.LogInformation("\n[SERVICE: Invoke] {MethodInfo}\n{Args}", Invocation.MethodInvocationTarget, CreateStringLogParameters(Invocation));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Invocation"></param>
    private void AfterProceedSync(IInvocation Invocation)
    {
        string StringArgs = Invocation.MethodInvocationTarget.ReturnType == typeof(void) ? "void" : GetStringOf((dynamic)Invocation.ReturnValue);
        ServiceLogger.LogInformation("\n[SERVICE: Result] {MethodInfo}\nReturn: {Args}", Invocation.MethodInvocationTarget, StringArgs);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Invocation"></param>
    /// <param name="HasAsynResult"></param>
    /// <returns></returns>
    private async Task AfterProceedAsync(IInvocation Invocation, bool HasAsynResult)
    {
        string StringArgs = await GetStringOfAsync((dynamic?)ProceedAsyncResult);
        ServiceLogger.LogInformation("\n[SERVICE: Result] {MethodInfo}\nReturn: {Args}", Invocation.MethodInvocationTarget, StringArgs);
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
                PropertyNamingPolicy = JsonDefaultNamingPolicy.DefaultNamingPolicy
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
    /// 
    /// </summary>
    /// <param name="Source"></param>
    /// <returns></returns>
    private static string GetStringOf(IAsyncEnumerable<object> Source) => GetStringOf(Source.ToEnumerable());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="object"></param>
    /// <returns></returns>
    private static ValueTask<string> GetStringOfAsync(object? @object) => ValueTask.FromResult(GetStringOf(@object));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Source"></param>
    /// <returns></returns>
    private static async ValueTask<string> GetStringOfAsync(IAsyncEnumerable<object> Source) => await GetStringOfAsync(Source.ToListAsync());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="object"></param>
    /// <returns></returns>
    private static ValueTask<string> GetStringOfAsync<T>(Task @object) => ValueTask.FromResult("void");

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="object"></param>
    /// <returns></returns>
    private static async ValueTask<string> GetStringOfAsync<T>(Task<T> @object) => GetStringOf(await @object);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="object"></param>
    /// <returns></returns>
    private static ValueTask<string> GetStringOfAsync<T>(ValueTask @object) => ValueTask.FromResult("void");

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="object"></param>
    /// <returns></returns>
    private static async ValueTask<string> GetStringOfAsync<T>(ValueTask<T> @object) => GetStringOf(await @object);

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
        return string.Join(Environment.NewLine, Invocation.MethodInvocationTarget.GetParameters()
            .Select((ParameterInfo, Index) => $"{ParameterInfo.Name} {GetStringOf((dynamic)Invocation.Arguments[Index])}"));
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
