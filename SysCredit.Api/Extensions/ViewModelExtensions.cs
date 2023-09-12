namespace SysCredit.Api.Extensions;

using Dapper;

using FluentValidation;
using FluentValidation.Results;

using SysCredit.Api.Attributes;
using SysCredit.Api.ViewModels;

using SysCredit.Helpers;

using System.Data;
using System.Reflection;

/// <summary>
/// 
/// </summary>
public static class ViewModelExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ViewModel"></param>
    /// <param name="ContextData"></param>
    /// <param name="Cancellation"></param>
    /// <returns></returns>
    public static async ValueTask<ValidationResult> ValidateAsync(
        this IViewModel ViewModel,
        IDictionary<string, object>? ContextData = null,
        CancellationToken Cancellation = default)
    {
        Type ValidatorType = ViewModel.LookupGenericTypeArgumentsFromGenericAttribute(typeof(ValidatorAttribute<>))![0];
        IValidator Validator = (IValidator)Activator.CreateInstance(ValidatorType)!;
        IValidationContext Context = (IValidationContext)Activator.CreateInstance(typeof(ValidationContext<>).MakeGenericType(ViewModel.GetType()), new object[] { ViewModel })!;

        if (ContextData is not null)
        {
            foreach (var Data in ContextData)
            {
                Context.RootContextData.Add(Data);
            }
        }

        return await Validator.ValidateAsync(Context, Cancellation);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="Result"></param>
    /// <param name="MethodId"></param>
    /// <param name="ErrorCategory"></param>
    /// <param name="ErrorCode"></param>
    /// <param name="ErrorMessage"></param>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static IServiceResult<TData?> CreateServiceResult<TData>(
        this ValidationResult Result,
        string MethodId, string ErrorCategory, string ErrorCode, string ErrorMessage,
        TData? Data = default)
    {
        return new ServiceResult<TData?>
        {
            Status = new ErrorStatus
            {
                MethodId = MethodId,
                ErrorCategory = ErrorCategory,
                ErrorMessage = ErrorMessage,
                ErrorCode = ErrorCode,
                Errors = Result.ErrorsToDictionary(),
                HasError = Result.HasError(),
            },
            Data = Data
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="Result"></param>
    /// <param name="MethodId"></param>
    /// <param name="ErrorCategory"></param>
    /// <param name="ErrorCode"></param>
    /// <param name="ErrorMessage"></param>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static ValueTask<IServiceResult<TData?>> CreateServiceResultAsync<TData>(
        this ValidationResult Result,
        string MethodId, string ErrorCategory, string ErrorCode, string ErrorMessage,
        TData? Data = default)
    {
        return ValueTask.FromResult(Result.CreateServiceResult(MethodId, ErrorCategory, ErrorCode, ErrorMessage, Data));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="Result"></param>
    /// <param name="CategoryType"></param>
    /// <param name="MethodId"></param>
    /// <param name="CodeIndex"></param>
    /// <param name="ErrorMessage"></param>
    /// <param name="Data"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static async ValueTask<IServiceResult<TData?>> CreateServiceResultAsync<TData>(
        this ValidationResult Result,
        string MethodId, Type CategoryType, string ErrorCode, string ErrorMessage,
        TData? Data = default)
    {
        return await Result.CreateServiceResultAsync(MethodId, CategoryType.GetErrorCategory()!, ErrorCode, ErrorMessage, Data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static IServiceResult<TData?> CreateServiceResult<TData>(this TData? Data)
    {
        return new ServiceResult<TData?> { Data = Data };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static ValueTask<IServiceResult<TData?>> CreateServiceResultAsync<TData>(this TData? Data)
    {
        return ValueTask.FromResult(Data.CreateServiceResult());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static async ValueTask<IServiceResult<TData?>> CreateServiceResultAsync<TData>(this ValueTask<TData?> Data)
    {
        return CreateServiceResult(await Data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ViewModel"></param>
    /// <returns></returns>
    public static DynamicParameters ToDynamicParameters(this IViewModel ViewModel)
    {
        var Parameters = new DynamicParameters();

        PropertyInfo[] Properties = ViewModel.GetType().GetProperties();

        foreach (var Property in Properties)
        {
            switch (Property.GetValue(ViewModel))
            {
                case IEnumerable<IViewModel> Values:
                {
                    Parameters.Add(Property.Name, Values.ToDataTable(), DbType.Object, ParameterDirection.Input);
                    break;
                }
                case object Value:
                {
                    Parameters.Add(Property.Name, Value);
                    break;
                }
            }
        }

        return Parameters;
    }
}
