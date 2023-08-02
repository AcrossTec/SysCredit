namespace SysCredit.Api.Extensions;

using SysCredit.Api.Exceptions;

public static class ExceptionExtensions
{
    public static IEnumerable<Exception> GetExceptions(this Exception? Ex)
    {
        while (Ex is not null)
        {
            yield return Ex;
            Ex = Ex.InnerException;
        }
    }

    public static IEnumerable<string> GetMessages(this Exception? Ex)
    {
        return Ex.GetExceptions().Select(Ex => Ex.Message);
    }

    public static SysCreditException ToSysCreditException(this Exception Ex, Type CategoryType, string MethodId, int ErrorCodeIndex, string ErrorMessage, Exception InnerException = null!)
    {
        SysCreditException SysCreditEx = new SysCreditException(Ex.Message, InnerException);
        SysCreditEx.Status.MethodId = MethodId;
        SysCreditEx.Status.ErrorMessage = ErrorMessage;
        SysCreditEx.Status.ErrorCategory = CategoryType.GetErrorCategory();
        SysCreditEx.Status.ErrorCode = CategoryType.GetErrorCode(MethodId, ErrorCodeIndex);
        SysCreditEx.Status.Errors.Add(nameof(Ex.Message), Ex.GetMessages().ToArray());
        return SysCreditEx;
    }
}
