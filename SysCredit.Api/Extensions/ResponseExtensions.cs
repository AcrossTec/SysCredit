namespace SysCredit.Api.Extensions;

using SysCredit.Api.Helpers;

public static class ResponseExtensions
{
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
}
