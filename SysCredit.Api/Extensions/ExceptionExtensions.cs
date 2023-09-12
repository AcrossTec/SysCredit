namespace SysCredit.Api.Extensions;

using SysCredit.Api.Exceptions;
using SysCredit.Api.Properties;

using System.Reflection;

/// <summary>
/// 
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Ex"></param>
    /// <returns></returns>
    public static IEnumerable<Exception> GetExceptions(this Exception? Ex)
    {
        while (Ex is not null)
        {
            yield return Ex;
            Ex = Ex.InnerException;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Ex"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetMessages(this Exception? Ex)
    {
        return Ex.GetExceptions().Select(Ex => Ex.Message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Ex"></param>
    /// <returns></returns>
    public static IDictionary<string, object?> ExceptionsToDictionary(this Exception? Ex)
    {
        return Ex.GetExceptions().ToDictionary<Exception, string, object?>(Ex => Ex.GetType().Name, Ex => Ex.Message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Ex"></param>
    /// <param name="MethodInfo"></param>
    /// <param name="ErrorCode"></param>
    /// <returns></returns>
    public static SysCreditException ToSysCreditException(this Exception Ex, MethodBase? MethodInfo, string ErrorCode)
    {
        SysCreditException SysCreditEx = new(new()
        {
            MethodId = MethodInfo.GetMethodId(),
            ErrorCode = ErrorCode,
            ErrorMessage = ErrorCodeMessages.GetMessage(ErrorCode),
            ErrorCategory = MethodInfo.GetErrorCategory(),
            Errors = Ex.ExceptionsToDictionary()
        }, Ex);

        return SysCreditEx;
    }
}
