namespace SysCredit.Api.Exceptions;

using SysCredit.Helpers;

/// <summary>
/// 
/// </summary>
/// <param name="Status"></param>
/// <param name="InnerException"></param>
public class SysCreditException(ErrorStatus Status, Exception? InnerException = null) : Exception(Status.ErrorMessage, InnerException)
{
    /// <summary>
    /// 
    /// </summary>
    public ErrorStatus Status { get; } = Status with { HasError = true };
}
