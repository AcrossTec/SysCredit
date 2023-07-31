namespace SysCredit.Api.Exceptions;

using SysCredit.Api.Helpers;

public class SysCreditException : Exception
{
    public SysCreditException()
    {
    }

    public SysCreditException(ErrorStatus Status) : base(Status.ErrorMessage)
    {
        this.Status = Status with { HasError = true };
    }

    public SysCreditException(string Message) : base(Message)
    {
    }

    public SysCreditException(Exception InnerException) : base(null, InnerException)
    {
    }

    public SysCreditException(string Message, Exception InnerException) : base(Message, InnerException)
    {
    }

    public ErrorStatus Status { get; } = new() { HasError = true };
}
