namespace SysCredit.Api.Extensions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using SysCredit.Api.Helpers;
using SysCredit.Api.ViewModels;

public static class ResponseExtensions
{
    private static ValueTask<IResponse<IViewModel?>> ToResponseWithReplaceDataAsync(this IResponse Result, IViewModel? Data)
    {
        return ValueTask.FromResult<IResponse<IViewModel?>>(new Response<IViewModel>
        {
            Data = Data,
            Status = Result.Status
        });
    }

    public static ValueTask<IResponse<TData?>> ToResponseWithReplaceDataAsync<TServiceResult, TData>(this IServiceResult<TServiceResult?> ServiceResult, TData? Data)
    {
        return ValueTask.FromResult<IResponse<TData?>>(new Response<TData>
        {
            Data = Data,
            Status = ServiceResult.Status
        });
    }

    public static async ValueTask<IResponse<TData?>> ToResponseWithReplaceDataAsync<TServiceResult, TData>(this ValueTask<IServiceResult<TServiceResult?>> ServiceResultTask, TData? Data)
    {
        return await (await ServiceResultTask).ToResponseWithReplaceDataAsync(Data);
    }

    public static ValueTask<IResponse<T?>> ToResponseAsync<T>(this T? Data, ErrorStatus? Status = null)
    {
        return ValueTask.FromResult(Data.ToResponse(Status));
    }

    public static IResponse<T?> ToResponse<T>(this T? Data, ErrorStatus? Status = null)
    {
        return new Response<T?>
        {
            Status = Status ?? new(),
            Data = Data
        };
    }

    public static async ValueTask<IActionResult> ToActionResultAsync(this IResponse Result, int Status, IViewModel ViewModel)
    {
        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(ViewModel));
        }
        else
        {
            return StatusCode(Status, Result);
        }
    }

    public static async ValueTask<IActionResult> ToActionResultAsync<TServiceResult>(this ValueTask<IServiceResult<TServiceResult?>> ResultTask, int Status, IViewModel ViewModel)
    {
        return await (await ResultTask).ToActionResultAsync(Status, ViewModel);
    }

    private static ObjectResult StatusCode(int StatusCode, object? Value)
    {
        return new ObjectResult(Value)
        {
            StatusCode = StatusCode
        };
    }
}
