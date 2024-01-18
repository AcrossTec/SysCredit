namespace SysCredit.Api.Endpoints;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Requests.PaymentFrequencies;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

/// <summary>
///     Endpoint para las distintas operaciones relacionadas a la frecuencia de pago.
/// </summary>
/// <seealso href="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/min-api-filters?view=aspnetcore-8.0" />
[Endpoints]
public static class PaymentFrequencyEndpoints
{
    /// <summary>
    ///     Método que registra todos los Endpoints relacionados al Payment Frequency.
    /// </summary>
    /// <param name="Endpoints">
    ///     Objeto de contrucción para configurar cada Endpoint de Payment Frecuency.
    /// </param>
    /// <returns>
    ///     Regresa <paramref name="Endpoints"/> con un tipo de tipo <see cref="IEndpointConventionBuilder"/>.
    /// </returns>
    public static IEndpointConventionBuilder MapPaymentFrequencyEndpoints(this IEndpointRouteBuilder Endpoints)
    {
        RouteGroupBuilder RouteGroup = Endpoints.MapGroup("/Api/PaymentFrequency").WithTags("PaymentFrequency", "Catalog");

        RouteGroup.MapGet("/", FetchPaymentFrequencyAsync)
                  .Produces<IResponse<IAsyncEnumerable<PaymentFrequencyInfo>>>(StatusCodes.Status200OK)
                  .Produces<IResponse<ProblemHttpResult>>(StatusCodes.Status500InternalServerError)
                  .WithName(nameof(FetchPaymentFrequencyAsync))
                  .WithSummary("Endpoint para obtener todas las frecuencias de pago")
                  .WithOpenApi();

        RouteGroup.MapGet("/{PaymentFrequencyId}", FetchPaymentFrequencyByIdAsync)
                  .Produces<IResponse<PaymentFrequencyInfo?>>(StatusCodes.Status200OK)
                  .Produces<IResponse<ProblemHttpResult>>(StatusCodes.Status500InternalServerError)
                  .WithName(nameof(FetchPaymentFrequencyByIdAsync))
                  .WithSummary("Endpoint para obtener una frecuencias de pago por Id")
                  .WithOpenApi();

        RouteGroup.MapGet("/Complete", FetchPaymentFrequencyCompleteAsync)
                  .Produces<IResponse<IAsyncEnumerable<PaymentFrequency>>>(StatusCodes.Status200OK)
                  .Produces<IResponse<ProblemHttpResult>>(StatusCodes.Status500InternalServerError)
                  .WithName(nameof(FetchPaymentFrequencyCompleteAsync))
                  .WithSummary("Endpoint para obtener todas las frecuencias de pago tal como está en base de datos")
                  .WithOpenApi();

        RouteGroup.MapPost("/", InsertPaymentFrequencyAsync)
                  .Produces<IResponse<EntityId>>(StatusCodes.Status201Created)
                  .Produces<IResponse<ProblemHttpResult>>(StatusCodes.Status500InternalServerError)
                  .Produces<IResponse<ValidationProblemDetails>>(StatusCodes.Status400BadRequest)
                  .WithName(nameof(InsertPaymentFrequencyAsync))
                  .WithSummary("Endpoint para registrar una frecuencias de pago")
                  .WithOpenApi();

        RouteGroup.MapPut("/{PaymentFrequencyId}", UpdatePaymentFrequencyAsync)
                  .Produces(StatusCodes.Status204NoContent)
                  .Produces<IResponse<ProblemHttpResult>>(StatusCodes.Status500InternalServerError)
                  .Produces<IResponse<ValidationProblemDetails>>(StatusCodes.Status400BadRequest)
                  .WithName(nameof(UpdatePaymentFrequencyAsync))
                  .WithSummary("Endpoint para actualizar una frecuencias de pago")
                  .WithOpenApi();

        RouteGroup.MapDelete("/{PaymentFrequencyId}", DeletePaymentFrequencyAsync)
                  .Produces(StatusCodes.Status204NoContent)
                  .Produces<IResponse<ProblemHttpResult>>(StatusCodes.Status500InternalServerError)
                  .Produces<IResponse<ValidationProblemDetails>>(StatusCodes.Status400BadRequest)
                  .WithName(nameof(DeletePaymentFrequencyAsync))
                  .WithSummary("Endpoint para borrar una frecuencias de pago")
                  .WithOpenApi();

        return new PaymentFrequencyEndpointsConventionBuilder(RouteGroup);
    }

    /// <summary>
    ///     Wrap RouteGroupBuilder with a non-public type to avoid a potential future behavioral breaking change.
    /// </summary>
    /// <param name="InnerBuilder">
    ///     Ver el argumento Endpoints de <see cref="MapPaymentFrequencyEndpoints(IEndpointRouteBuilder)"/>.
    /// </param>
    private sealed class PaymentFrequencyEndpointsConventionBuilder(RouteGroupBuilder InnerBuilder) : IEndpointConventionBuilder
    {
        private IEndpointConventionBuilder InnerAsConventionBuilder => InnerBuilder;

        public void Add(Action<EndpointBuilder> Convention) => InnerAsConventionBuilder.Add(Convention);

        public void Finally(Action<EndpointBuilder> FinallyConvention) => InnerAsConventionBuilder.Finally(FinallyConvention);
    }

    /// <summary>
    ///     Obtiene los registros de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <param name="Manager">
    ///     Objeto de servicio con toda la lógica de negocio usada por este Endpoint.
    /// </param>
    /// <param name="Logger">
    ///     Objeto de Log para informar sobre los pasos que está haciendo este Endpoint.
    /// </param>
    /// <returns>
    ///     Regresa todos los registros de la tabla <see cref="Models.PaymentFrequency"/>
    /// </returns>
    public static async Task<Results<Ok<IResponse<IAsyncEnumerable<PaymentFrequencyInfo>>>, ProblemHttpResult>> FetchPaymentFrequencyAsync(
        [FromServices] IManager<PaymentFrequency> Manager,
        [FromServices] ILogger<Endpoint<PaymentFrequency>> Logger)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/PaymentFrequency");
        return TypedResults.Ok(await Manager.FetchPaymentFrequencyAsync().ToResponseAsync());
    }

    /// <summary>
    ///     Actualiza un registro de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <param name="PaymentFrequencyId">
    ///     Id recibido de la ruta
    /// </param>
    /// <param name="Request">
    ///     Datos que se van a actualizar del <see cref="Models.PaymentFrequency"/>
    /// </param>
    /// <param name="Manager">
    ///     Objeto de servicio con toda la lógica de negocio usada por este Endpoint.
    /// </param>
    /// <param name="Logger">
    ///     Objeto de Log para informar sobre los pasos que está haciendo este Endpoint.
    /// </param>
    /// <returns>
    ///     Regresa un Http 204
    /// </returns>
    public static async Task<Results<NoContent, ProblemHttpResult, ValidationProblem>> UpdatePaymentFrequencyAsync(
        [FromRoute] long PaymentFrequencyId,
        [FromBody] UpdatePaymentFrequencyRequest Request,
        [FromServices] IManager<PaymentFrequency> Manager,
        [FromServices] ILogger<Endpoint<PaymentFrequency>> Logger)
    {
        Logger.LogInformation("EndPoint[PUT]: /Api/PaymentFrequency/{PaymentFrequencyId}", Request.PaymentFrequencyId);
        await Manager.UpdatePaymentFrequencyAsync(PaymentFrequencyId, Request);
        return TypedResults.NoContent();
    }

    /// <summary>
    ///     Elimina un registro de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <param name="PaymentFrequencyId">
    ///    Id del <see cref="Models.PaymentFrequency"/> a eliminar.
    /// </param>
    /// <param name="Manager">
    ///     Objeto de servicio con toda la lógica de negocio usada por este Endpoint.
    /// </param>
    /// <param name="Logger">
    ///     Objeto de Log para informar sobre los pasos que está haciendo este Endpoint.
    /// </param>
    /// <returns>
    ///     Retorna un Http 204.
    /// </returns>
    public static async Task<Results<NoContent, ProblemHttpResult, ValidationProblem>> DeletePaymentFrequencyAsync(
        [FromRoute] long PaymentFrequencyId,
        [FromServices] IManager<PaymentFrequency> Manager,
        [FromServices] ILogger<Endpoint<PaymentFrequency>> Logger)
    {
        Logger.LogInformation("EndPoint[DELETE]: /Api/PaymentFrequency/{PaymentFrequencyId}", PaymentFrequencyId);
        await Manager.DeletePaymentFrequencyAsync(new(PaymentFrequencyId));
        return TypedResults.NoContent();
    }

    /// <summary>
    ///     Obtiene todos los registros completos de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <param name="Manager">
    ///     Objeto de servicio con toda la lógica de negocio usada por este Endpoint.
    /// </param>
    /// <param name="Logger">
    ///     Objeto de Log para informar sobre los pasos que está haciendo este Endpoint.
    /// </param>
    /// <returns>
    ///     Regresa todos los registros completos de la tabla <see cref="Models.PaymentFrequency"/>
    /// </returns>
    public static async Task<Results<Ok<IResponse<IAsyncEnumerable<PaymentFrequency>>>, ProblemHttpResult>> FetchPaymentFrequencyCompleteAsync(
        [FromServices] IManager<PaymentFrequency> Manager,
        [FromServices] ILogger<Endpoint<PaymentFrequency>> Logger)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/PaymentFrequency/Complete");
        return TypedResults.Ok(await Manager.FetchPaymentFrequencyCompleteAsync().ToResponseAsync());
    }

    /// <summary>
    ///     Obtiene un registro de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <param name="PaymentFrequencyId">
    ///     Id obtenido de la ruta
    /// </param>
    /// <param name="Manager">
    ///     Objeto de servicio con toda la lógica de negocio usada por este Endpoint.
    /// </param>
    /// <param name="Logger">
    ///     Objeto de Log para informar sobre los pasos que está haciendo este Endpoint.
    /// </param>
    /// <returns>
    ///     Regresa un registro de la tabla <see cref="Models.PaymentFrequency"/>
    /// </returns>
    public static async Task<Results<Ok<IResponse<PaymentFrequencyInfo?>>, ProblemHttpResult>> FetchPaymentFrequencyByIdAsync(
        [FromRoute] long PaymentFrequencyId,
        [FromServices] IManager<PaymentFrequency> Manager,
        [FromServices] ILogger<Endpoint<PaymentFrequency>> Logger)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/PaymentFrequency/{PaymentFrequencyId}", PaymentFrequencyId);
        return TypedResults.Ok(await Manager.FetchPaymentFrequencyByIdAsync(PaymentFrequencyId).ToResponseAsync());
    }

    /// <summary>
    ///     Crear una nueva frecuencia de pago en la base de datos
    /// </summary>
    /// <param name="Request">
    ///     Datos usado para crear la frecuencia de pago
    /// </param>
    /// <param name="Manager">
    ///     Objeto de servicio con toda la lógica de negocio usada por este Endpoint.
    /// </param>
    /// <param name="Logger">
    ///     Objeto de Log para informar sobre los pasos que está haciendo este Endpoint.
    /// </param>
    /// <param name="HttpContext">
    ///     Objeto que tiene toda la información del Request actual del Endpoint.
    /// </param>
    /// <param name="LinkGenerator">
    ///     Objeto de utilería para generar enlaces Relativos o Absolutos.
    /// </param>
    /// <returns>
    ///     Regresa el nuevo Id de la frecuencia de pago creado
    /// </returns>
    public static async Task<Results<Created<IResponse<EntityId>>, ProblemHttpResult, ValidationProblem>> InsertPaymentFrequencyAsync(
        [FromBody] CreatePaymentFrequencyRequest Request,
        [FromServices] IManager<PaymentFrequency> Manager,
        [FromServices] ILogger<Endpoint<PaymentFrequency>> Logger,
        [FromServices] HttpContext HttpContext,
        [FromServices] LinkGenerator LinkGenerator)
    {
        Logger.LogInformation("EndPoint[POST]: /Api/PaymentFrequency");
        var Result = await Manager.InsertPaymentFrequencyAsync(Request).ToResponseAsync();

        // References:
        //  https://github.com/dotnet/aspnetcore/issues/45101
        //  https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core

        var PaymentFrequencyLink = LinkGenerator.GetUriByName(
            HttpContext, nameof(FetchPaymentFrequencyByIdAsync),
            new RouteValueDictionary(new KeyValuePair<string, object?>[] { new("PaymentFrequencyId", Result.Data.Id) }));

        return TypedResults.Created(PaymentFrequencyLink, Result);
    }
}
