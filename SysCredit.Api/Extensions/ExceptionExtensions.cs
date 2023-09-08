namespace SysCredit.Api.Extensions;

using SysCredit.Api.Exceptions;

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
    /// <param name="CategoryType"></param>
    /// <param name="MethodId"></param>
    /// <param name="ErrorCodeIndex"></param>
    /// <param name="ErrorMessage"></param>
    /// <param name="InnerException"></param>
    /// <returns></returns>
    public static SysCreditException ToSysCreditException(this Exception Ex, Type CategoryType, string MethodId, int ErrorCodeIndex, string ErrorMessage, Exception InnerException = null!)
    {
        SysCreditException SysCreditEx = new(Ex.Message, InnerException);
        SysCreditEx.Status.MethodId = MethodId;
        SysCreditEx.Status.ErrorMessage = ErrorMessage;
        SysCreditEx.Status.ErrorCategory = CategoryType.GetErrorCategory();
        SysCreditEx.Status.ErrorCode = CategoryType.GetErrorCode(MethodId, ErrorCodeIndex);
        SysCreditEx.Status.Errors.Add(nameof(Ex.Message), Ex.GetMessages().ToArray());
        return SysCreditEx;
    }
}
