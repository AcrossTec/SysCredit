namespace SysCredit.Api.Extensions;

using FluentValidation.Results;

/// <summary>
/// 
/// </summary>
public static class ValidationResultExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Result"></param>
    /// <returns></returns>
    public static bool HasError(this ValidationResult Result)
    {
        return Result.IsValid is false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Result"></param>
    /// <returns></returns>
    public static IDictionary<string, object?> ErrorsToDictionary(this ValidationResult Result)
    {
        return Result.Errors.GroupBy(Error => Error.PropertyName)
                     .ToDictionary<IGrouping<string, ValidationFailure>, string, object?>(
                       Property => Property.Key,
                       Property => Property.Select(Property => Property.ErrorMessage).ToArray());
    }
}
