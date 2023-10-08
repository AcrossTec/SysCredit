namespace SysCredit.Api.Extensions;

using SysCredit.Helpers;

/// <summary>
///     Métodos de utilería para configurar respuestas generalizadas de los Endpoints.
/// </summary>
public static class ResponseExtensions
{
    /// <summary>
    ///     Encapsula <paramref name="Data" /> en un objeto de tipo <see cref="IResponse{T}" />.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo de <see cref="IResponse{T}.Data" />.
    /// </typeparam>
    /// <param name="Data">
    ///     Valor de <see cref="IResponse{T}.Data" />.
    /// </param>
    /// <param name="Status">
    ///     Valor de <see cref="IResponse.Status" />.
    /// </param>
    /// <returns>
    ///     Regresa un nuevo objeto de tipo <see cref="IResponse{T}" /> encapsulando <paramref name="Data" />
    ///     en <see cref="IResponse{T}.Data" /> e información de la operación en un <see cref="ErrorStatus" />.
    /// </returns>
    public static ValueTask<IResponse<T>> ToResponseAsync<T>(this T Data, ErrorStatus? Status = default)
    {
        return ValueTask.FromResult(Data.ToResponse(Status));
    }

    /// <summary>
    ///     Encapsula <paramref name="Data" /> en un objeto de tipo <see cref="IResponse{T}" />.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo de <see cref="IResponse{T}.Data" />.
    /// </typeparam>
    /// <param name="Data">
    ///     Valor de <see cref="IResponse{T}.Data" />.
    /// </param>
    /// <param name="Status">
    ///     Valor de <see cref="IResponse.Status" />.
    /// </param>
    /// <returns>
    ///     Regresa un nuevo objeto de tipo <see cref="IResponse{T}" /> encapsulando <paramref name="Data" />
    ///     en <see cref="IResponse{T}.Data" /> e información de la operación en un <see cref="ErrorStatus" />.
    /// </returns>
    public static async ValueTask<IResponse<T>> ToResponseAsync<T>(this ValueTask<T> Data, ErrorStatus? Status = null)
    {
        return ToResponse(await Data, Status);
    }

    /// <summary>
    ///     Encapsula <paramref name="Data" /> en un objeto de tipo <see cref="IResponse{T}" />.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo de <see cref="IResponse{T}.Data" />.
    /// </typeparam>
    /// <param name="Data">
    ///     Valor de <see cref="IResponse{T}.Data" />.
    /// </param>
    /// <param name="Status">
    ///     Valor de <see cref="IResponse.Status" />.
    /// </param>
    /// <returns>
    ///     Regresa un nuevo objeto de tipo <see cref="IResponse{T}" /> encapsulando <paramref name="Data" />
    ///     en <see cref="IResponse{T}.Data" /> e información de la operación en un <see cref="ErrorStatus" />.
    /// </returns>
    public static IResponse<T> ToResponse<T>(this T Data, ErrorStatus? Status = null)
    {
        return new Response<T>
        {
            Status = Status ?? new(),
            Data = Data
        };
    }
}
