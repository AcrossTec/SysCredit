namespace SysCredit.Api.Extensions;

using FluentValidation;
using FluentValidation.Results;

using SysCredit.Api.Attributes;
using SysCredit.Api.Helpers;
using SysCredit.Api.ViewModels;

using System.Reflection;

public static class ViewModelExtensions
{
    public static async ValueTask<ValidationResult> ValidateAsync(this IViewModel ViewModel, IDictionary<string, object>? ContextData = null, CancellationToken Cancellation = default)
    {
        Type ValidatorType = ViewModel.SearchGenericTypeArgumentsFromGenericAttribute(typeof(ValidatorAttribute<>))![0];
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

    public static IServiceResult<TData?> CreateResult<TData>(this ValidationResult Result, string ErrorCategory, string ErrorCode, string ErrorMessage, TData? Data = default)
    {
        return new ServiceResult<TData?>
        {
            Status = new ErrorStatus
            {
                ErrorCategory = ErrorCategory,
                ErrorCode = ErrorCode,
                HasError = !Result.IsValid,
                ErrorMessage = ErrorMessage,
                Errors = Result.ToDictionary()
            },
            Data = Data
        };
    }

    public static ValueTask<IServiceResult<TData?>> CreateResultAsync<TData>(this ValidationResult Result, string ErrorCategory, string ErrorCode, string ErrorMessage, TData? Data = default)
    {
        return ValueTask.FromResult(Result.CreateResult(ErrorCategory, ErrorCode, ErrorMessage, Data));
    }

    public static ValueTask<IServiceResult<TData?>> CreateResultAsync<TData>(this ValidationResult Result, Type Type, int CodeIndex, string ErrorMessage, TData? Data = default)
    {
        return ValueTask.FromResult(Result.CreateResult(Type.GetErrorCategory(), Type.GetErrorCode(CodeIndex), ErrorMessage, Data));
    }

    public static IServiceResult<TData?> CreateResult<TData>(this TData? Data)
    {
        return new ServiceResult<TData?> { Data = Data };
    }

    public static ValueTask<IServiceResult<TData?>> CreateResultAsync<TData>(this TData? Data)
    {
        return ValueTask.FromResult(Data.CreateResult());
    }

    public static async ValueTask<IServiceResult<TData?>> CreateResultAsync<TData>(this ValueTask<TData?> ValueTask)
    {
        return await (await ValueTask).CreateResultAsync();
    }
}
