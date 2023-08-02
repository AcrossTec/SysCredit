namespace SysCredit.Api.Extensions;

using FluentValidation;
using FluentValidation.Results;

using SysCredit.Api.Attributes;
using SysCredit.Api.Helpers;
using SysCredit.Api.ViewModels;

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

    public static IServiceResult<TData?> CreateResult<TData>(this ValidationResult Result, string MethodId, string ErrorCategory, string ErrorCode, string ErrorMessage, TData? Data = default)
    {
        return new ServiceResult<TData?>
        {
            Status = new ErrorStatus
            {
                MethodId = MethodId,
                HasError = !Result.IsValid,
                ErrorCategory = ErrorCategory,
                ErrorMessage = ErrorMessage,
                ErrorCode = ErrorCode,
                Errors = Result.ToDictionary()
            },
            Data = Data
        };
    }

    public static ValueTask<IServiceResult<TData?>> CreateResultAsync<TData>(this ValidationResult Result, string MethodId, string ErrorCategory, string ErrorCode, string ErrorMessage, TData? Data = default)
    {
        return ValueTask.FromResult(Result.CreateResult(MethodId, ErrorCategory, ErrorCode, ErrorMessage, Data));
    }

    public static ValueTask<IServiceResult<TData?>> CreateResultAsync<TData>(this ValidationResult Result, Type CategoryType, string MethodId, int CodeIndex, string ErrorMessage, TData? Data = default)
    {
        return ValueTask.FromResult(Result.CreateResult(MethodId,
            CategoryType.GetErrorCategory() ?? throw new InvalidOperationException($"El tipo {CategoryType.Name} no es un tipo de categoría."),
            CategoryType.GetErrorCode(MethodId, CodeIndex) ?? throw new InvalidOperationException($"El tipo {CategoryType.Name} no posee un método de código de errores."),
            ErrorMessage, Data));
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
