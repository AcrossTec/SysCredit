namespace SysCredit.Api.Patchers;

using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;

using HarmonyLib;

using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;
using SysCredit.Helpers;

using static String;

/// <summary>
///     Crea un intercepción ó proxy que será ejecutado en vez del método estático del Store.
/// </summary>
[HarmonyPatch]
public static class StorePatcher
{
    /// <summary>
    ///     Obtiene todos los métodos del Store que será parchados.
    /// </summary>
    /// <remarks>
    ///     Patching multiple methods:
    ///     https://harmony.pardeike.net/articles/annotations.html#patching-multiple-methods
    /// </remarks>
    /// <returns>
    ///     Regresa un array con todos los métodos del Store que serán parchados.
    /// </returns>
    private static IEnumerable<MethodBase> TargetMethods()
    {
        // Obtiene todos los Stores del sistema.
        // Sólo se considera un Store todas aquellas clases que tiene el atributo Store.
        var Stores = from Type in AccessTools.GetTypesFromAssembly(typeof(StorePatcher).Assembly)
                     where Type.IsStore()
                     select Type;

        // Obtener todos los métodos del Store que están marcados con MethodIdAttribute y que sean asincronos.
        var Methods = from Method in Stores.SelectMany(([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] Type) => Type.GetMethods(BindingFlags.Public | BindingFlags.Static))
                      where Method.HasMethodId() && Method.IsAsyncMethod()
                      select Method;

        return Methods;
    }

    /// <summary>
    ///     Patching: Prefix
    ///     https://harmony.pardeike.net/articles/patching-prefix.html
    /// </summary>
    /// <param name="__originalMethod">
    ///     Método original que será ejecutado.
    /// </param>
    /// <param name="Store">
    ///     Contexto de base de datos.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> para ejecutar <paramref name="__originalMethod"/> y <see langword="false"/> para saltar <paramref name="__originalMethod"/>.
    /// </returns>
    private static bool Prefix(MethodInfo __originalMethod, ref IStore Store)
    {
        BeforeProceed(Store, __originalMethod);
        return true;
    }

    /// <summary>
    ///     Patching: Postfix
    ///     https://harmony.pardeike.net/articles/patching-prefix.html
    /// </summary>
    /// <param name="__result">
    ///     Es el retorno de <paramref name="__originalMethod"/> después de ser ejecutado.
    /// </param>
    /// <param name="__originalMethod">
    ///     Método original que fue ejecutado.
    /// </param>
    /// <param name="Store">
    ///     Contexto de base de datos.
    /// </param>
    /// <returns>
    ///     Regresa una nueva versión de <paramref name="__result"/> con la misma información.
    /// </returns>
    [RequiresUnreferencedCode("Uso de operaciones dynamic en IntercepAsync para deducir el tipo de Result.")]
    private static void Postfix(ref object __result, MethodInfo __originalMethod, ref IStore Store)
    {
        // El primer argumento puede ser el valor de retorno sin el uso de: ref
        // Se puede extender el valor de retorno regresando el mismo valor con las modificaciones aplicadas
        __result = InterceptAsync(Store, __originalMethod, (dynamic)__result);
    }

    /// <summary>
    ///     Muestra Logs para <paramref name="OriginalMethod"/> del <paramref name="Store"/> antes de ser ejecutado.
    /// </summary>
    /// <param name="OriginalMethod">
    ///     Método original del Store que se está interceptando.
    /// </param>
    /// <param name="Store">
    ///     Contexto de base de datos.
    /// </param>
    private static void BeforeProceed(IStore Store, MethodInfo OriginalMethod)
    {
        Store.Logger.LogInformation("\n[STORE: Invoke] {MethodInfo}\n{Args}", OriginalMethod, CreateStringLogParameters(OriginalMethod));
    }

    /// <summary>
    ///     Muestra Logs para <paramref name="OriginalMethod"/> del <paramref name="Store"/> después de ser ejecutado.
    /// </summary>
    /// <param name="OriginalMethod">
    ///     Método original del Store que se está interceptando.
    /// </param>
    /// <param name="Store">
    ///     Contexto de base de datos.
    /// </param>
    /// <param name="Result">
    ///     Objeto retornado por <paramref name="OriginalMethod"/> después de ser invocado.
    /// </param>
    /// <returns>
    ///     <see cref="Task.CompletedTask"/>.
    /// </returns>
    private static async Task AfterProceedAsync<TResult>(IStore Store, MethodInfo OriginalMethod, TResult? Result = default)
    {
        string ReturnInfo = await GetStringOfAsync(Result);
        Store.Logger.LogInformation("\n[STORE: Result] {MethodInfo}\nReturn: {Result}", OriginalMethod, ReturnInfo);
    }

    private static async Task InterceptAsync(IStore Store, MethodInfo OriginalMethod, Task Task)
    {
        try
        {
            await Task.ConfigureAwait(continueOnCapturedContext: false);
            await AfterProceedAsync(Store, OriginalMethod, Result: default(object));
        }
        catch (SqlException Exception)
        {
            throw CreateStoreException(Exception, OriginalMethod);
        }
        catch (Exception Exception)
        {
            throw CreateStoreException(Exception, OriginalMethod);
        }
    }

    private static async Task<TResult?> InterceptAsync<TResult>(IStore Store, MethodInfo OriginalMethod, Task<TResult?> Task)
    {
        try
        {
            TResult? ProceedAsyncResult = await Task.ConfigureAwait(continueOnCapturedContext: false);
            await AfterProceedAsync(Store, OriginalMethod, ProceedAsyncResult);
            return ProceedAsyncResult;
        }
        catch (SqlException Exception)
        {
            throw CreateStoreException(Exception, OriginalMethod);
        }
        catch (Exception Exception)
        {
            throw CreateStoreException(Exception, OriginalMethod);
        }
    }

    private static async ValueTask InterceptAsync(IStore Store, MethodInfo OriginalMethod, ValueTask Task)
    {
        try
        {
            await Task.ConfigureAwait(continueOnCapturedContext: false);
            await AfterProceedAsync(Store, OriginalMethod, Result: default(object));
        }
        catch (SqlException Exception)
        {
            throw CreateStoreException(Exception, OriginalMethod);
        }
        catch (Exception Exception)
        {
            throw CreateStoreException(Exception, OriginalMethod);
        }
    }

    private static async ValueTask<TResult?> InterceptAsync<TResult>(IStore Store, MethodInfo OriginalMethod, ValueTask<TResult?> Task)
    {
        try
        {
            var ProceedAsyncResult = await Task.ConfigureAwait(continueOnCapturedContext: false);
            await AfterProceedAsync(Store, OriginalMethod, ProceedAsyncResult);
            return ProceedAsyncResult;
        }
        catch (SqlException Exception)
        {
            throw CreateStoreException(Exception, OriginalMethod);
        }
        catch (Exception Exception)
        {
            throw CreateStoreException(Exception, OriginalMethod);
        }
    }

    private static IAsyncEnumerable<TSource> InterceptAsync<TSource>(IStore Store, MethodInfo OriginalMethod, IAsyncEnumerable<TSource> Source)
    {
        Store.Logger.LogInformation("\n[STORE: Result] {MethodInfo}\n{Args}", OriginalMethod, CreateStringLogParameters(OriginalMethod));
        return Source;
    }

    /// <summary>
    ///     Crea un <see cref="StoreException"/> desde un <see cref="SqlException"/>.
    /// </summary>
    /// <param name="OriginalMethod">
    ///     Información del método de servicio actual en ejecución.
    /// </param>
    /// <param name="Exception">
    ///     Excepción lanzada por el proveedor de base de datos.
    /// </param>
    /// <returns>
    ///     Regresa un <see cref="StoreException" /> con información detallada del error actual.
    /// </returns>
    private static StoreException CreateStoreException(SqlException Exception, MethodInfo OriginalMethod)
    {
        return Exception.CreateExceptionUsingMethodInfo<StoreException>(OriginalMethod, Status =>
        {
            Status.ErrorCode = OriginalMethod.GetStoreSQLServerErrorCode();
            Status.ErrorMessage = OriginalMethod.GetStoreSQLServerErrorCodeMessage();
            Status.Extensions.Add(SysCreditConstants.SqlProcedureKey, Exception.Procedure);
            Status.Errors = new Dictionary<string, object?>
            {
                [SysCreditConstants.SqlErrorsKey] = Exception.Errors.Cast<SqlError>().Select(SqlError => SqlError.Message).ToArray()
            };
        });
    }

    /// <summary>
    ///     Crea un <see cref="StoreException"/> desde un <see cref="SqlException"/>.
    /// </summary>
    /// <param name="OriginalMethod">
    ///     Información del método de servicio actual en ejecución.
    /// </param>
    /// <param name="Exception">
    ///     Excepción lanzada por el proveedor de base de datos.
    /// </param>
    /// <returns>
    ///     Regresa un <see cref="StoreException" /> con información detallada del error actual.
    /// </returns>
    private static StoreException CreateStoreException(Exception Exception, MethodInfo OriginalMethod)
    {
        return Exception.CreateExceptionUsingMethodInfo<StoreException>(OriginalMethod, Status =>
        {
            Status.ErrorCode = OriginalMethod.GetStoreExecuteErrorCode();
            Status.ErrorMessage = OriginalMethod.GetStoreExecuteErrorCodeMessage();
        });
    }

    /// <summary>
    ///     Concatena en un <see cref="String"/> la información de todos los argumentos del método del Store. 
    /// </summary>
    /// <param name="OriginalMethod">
    ///     Método original del Store que se está interceptando.
    /// </param>
    /// <returns>
    ///     Regresa la información de todos los argumendos de <paramref name="OriginalMethod"/> como un <see cref="String"/>.
    /// </returns>
    private static string CreateStringLogParameters(MethodInfo OriginalMethod)
    {
        var Arguments = OriginalMethod.GetParameters();
        return Join(Environment.NewLine, Arguments.Select((ParameterInfo, Index) => $"{ParameterInfo.Name} {GetStringOf(Arguments[Index])}"));
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
}
