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
}
