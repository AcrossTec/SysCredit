﻿namespace SysCredit.Api.Extensions;

using SysCredit.Helpers;

/// <summary>
/// 
/// </summary>
public static class ResponseExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TServiceResult"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <param name="ServiceResult"></param>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static ValueTask<IResponse<TData?>> ToResponseWithReplaceDataAsync<TServiceResult, TData>(this IServiceResult<TServiceResult?> ServiceResult, TData? Data)
    {
        return ValueTask.FromResult<IResponse<TData?>>(new Response<TData>
        {
            Data = Data,
            Status = ServiceResult.Status
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TServiceResult"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <param name="ServiceResult"></param>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static async ValueTask<IResponse<TData?>> ToResponseWithReplaceDataAsync<TServiceResult, TData>(this ValueTask<IServiceResult<TServiceResult?>> ServiceResult, TData? Data)
    {
        return await ToResponseWithReplaceDataAsync(await ServiceResult, Data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Data"></param>
    /// <param name="Status"></param>
    /// <returns></returns>
    public static ValueTask<IResponse<T?>> ToResponseAsync<T>(this T? Data, ErrorStatus? Status = null)
    {
        return ValueTask.FromResult(Data.ToResponse(Status));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Data"></param>
    /// <param name="Status"></param>
    /// <returns></returns>
    public static async ValueTask<IResponse<T?>> ToResponseAsync<T>(this ValueTask<T?> Data, ErrorStatus? Status = null)
    {
        return ToResponse(await Data, Status);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Data"></param>
    /// <param name="Status"></param>
    /// <returns></returns>
    public static IResponse<T?> ToResponse<T>(this T? Data, ErrorStatus? Status = null)
    {
        return new Response<T?>
        {
            Status = Status ?? new(),
            Data = Data
        };
    }
}
