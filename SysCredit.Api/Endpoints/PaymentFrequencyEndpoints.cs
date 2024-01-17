namespace SysCredit.Api.Endpoints;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.PaymentFrequencies;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

using IPaymentFrequencyService = global::SysCredit.Api.Interfaces.Services.IPaymentFrequencyService;

/// <summary>
///     Endpoint para las distintas operaciones relacionadas a la frecuencia de pago.
/// </summary>
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
        /*
         Api Rest
         ASP NET 4 Tipos

         1) MVC -> Controller
            Se puede retornar Razor Pages y Código HTML y JavaScript
            (Server Rendering)

         2) Web API -> API Controller
            Solamente Endpoints que retoran datos.
            No procesa nada relacionado a HTML ni JavaScript
            
         3) Rest API Basados en Pages: MVC + Web API
            Todo el código está en .cshtml

         4) Minimal API
            Rest API mediante programación funcional (Express Nodejs)

         Extra) Rest API desde GRPC autogenerado (Sólo es válido con C#)
                Archivos protos y generan el código necesario para soportar Web API
         */
        ArgumentNullException.ThrowIfNull(Endpoints);

        RouteGroupBuilder RouteGroup = Endpoints.MapGroup("/Api/PaymentFrequency");

        RouteGroup.MapGet("/", FetchPaymentFrequencyAsync)
                  .Produces<IResponse<IAsyncEnumerable<PaymentFrequencyInfo>>>(StatusCodes.Status200OK)
                  .Produces<IResponse<ProblemHttpResult>>(StatusCodes.Status500InternalServerError);

        RouteGroup.MapGet("/{PaymentFrequencyId}", FetchPaymentFrequencyByIdAsync)
                  .Produces<IResponse<PaymentFrequencyInfo?>>(StatusCodes.Status200OK)
                  .Produces<IResponse<ProblemHttpResult>>(StatusCodes.Status500InternalServerError)
                  .WithName(nameof(FetchPaymentFrequencyByIdAsync));

        RouteGroup.MapGet("/Complete", FetchPaymentFrequencyCompleteAsync)
                  .Produces<IResponse<IAsyncEnumerable<PaymentFrequency>>>(StatusCodes.Status200OK)
                  .Produces<IResponse<ProblemHttpResult>>(StatusCodes.Status500InternalServerError);

        RouteGroup.MapPost("/", InsertPaymentFrequencyAsync)
                  .Produces<IResponse<EntityId>>(StatusCodes.Status201Created)
                  .Produces<IResponse<ProblemHttpResult>>(StatusCodes.Status500InternalServerError)
                  .Produces<IResponse<ValidationProblemDetails>>(StatusCodes.Status400BadRequest);

        RouteGroup.MapPut("/{PaymentFrequencyId}", UpdatePaymentFrequencyAsync)
                  .Produces(StatusCodes.Status204NoContent)
                  .Produces<IResponse<ProblemHttpResult>>(StatusCodes.Status500InternalServerError)
                  .Produces<IResponse<ValidationProblemDetails>>(StatusCodes.Status400BadRequest);

        RouteGroup.MapDelete("/{PaymentFrequencyId}", DeletePaymentFrequencyAsync)
                  .Produces(StatusCodes.Status204NoContent)
                  .Produces<IResponse<ProblemHttpResult>>(StatusCodes.Status500InternalServerError)
                  .Produces<IResponse<ValidationProblemDetails>>(StatusCodes.Status400BadRequest);

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
    /// <param name="Service">
    ///     Objeto de servicio con toda la lógica de negocio usada por este Endpoint.
    /// </param>
    /// <param name="Logger">
    ///     Objeto de Log para informar sobre los pasos que está haciendo este Endpoint.
    /// </param>
    /// <returns>
    ///     Regresa todos los registros de la tabla <see cref="Models.PaymentFrequency"/>
    /// </returns>
    public static async Task<Results<Ok<IResponse<IAsyncEnumerable<PaymentFrequencyInfo>>>, ProblemHttpResult>> FetchPaymentFrequencyAsync(
        [FromServices] IPaymentFrequencyService Service,
        [FromServices] ILogger<Endpoint<PaymentFrequency>> Logger)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/PaymentFrequency");
        return TypedResults.Ok(await Service.FetchPaymentFrequencyAsync().ToResponseAsync());
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
    /// <param name="Service">
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
        [FromServices] IPaymentFrequencyService Service,
        [FromServices] ILogger<Endpoint<PaymentFrequency>> Logger)
    {
        Logger.LogInformation("EndPoint[PUT]: /Api/PaymentFrequency/{PaymentFrequencyId}", Request.PaymentFrequencyId);
        await Service.UpdatePaymentFrequencyAsync(PaymentFrequencyId, Request);
        return TypedResults.NoContent();
    }

    /// <summary>
    ///     Elimina un registro de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <param name="PaymentFrequencyId">
    ///    Id del <see cref="Models.PaymentFrequency"/> a eliminar.
    /// </param>
    /// <param name="Service">
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
        [FromServices] IPaymentFrequencyService Service,
        [FromServices] ILogger<Endpoint<PaymentFrequency>> Logger)
    {
        Logger.LogInformation("EndPoint[DELETE]: /Api/PaymentFrequency/{PaymentFrequencyId}", PaymentFrequencyId);
        await Service.DeletePaymentFrequencyAsync(new(PaymentFrequencyId));
        return TypedResults.NoContent();
    }

    /// <summary>
    ///     Obtiene todos los registros completos de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <param name="Service">
    ///     Objeto de servicio con toda la lógica de negocio usada por este Endpoint.
    /// </param>
    /// <param name="Logger">
    ///     Objeto de Log para informar sobre los pasos que está haciendo este Endpoint.
    /// </param>
    /// <returns>
    ///     Regresa todos los registros completos de la tabla <see cref="Models.PaymentFrequency"/>
    /// </returns>
    public static async Task<Results<Ok<IResponse<IAsyncEnumerable<PaymentFrequency>>>, ProblemHttpResult>> FetchPaymentFrequencyCompleteAsync(
        [FromServices] IPaymentFrequencyService Service,
        [FromServices] ILogger<Endpoint<PaymentFrequency>> Logger)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/PaymentFrequency/Complete");
        return TypedResults.Ok(await Service.FetchPaymentFrequencyCompleteAsync().ToResponseAsync());
    }

    /// <summary>
    ///     Obtiene un registro de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <param name="PaymentFrequencyId">
    ///     Id obtenido de la ruta
    /// </param>
    /// <param name="Service">
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
        [FromServices] IPaymentFrequencyService Service,
        [FromServices] ILogger<Endpoint<PaymentFrequency>> Logger)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/PaymentFrequency/{PaymentFrequencyId}", PaymentFrequencyId);
        return TypedResults.Ok(await Service.FetchPaymentFrequencyByIdAsync(PaymentFrequencyId).ToResponseAsync());
    }

    /// <summary>
    ///     Crear una nueva frecuencia de pago en la base de datos
    /// </summary>
    /// <param name="Request">
    ///     Datos usado para crear la frecuencia de pago
    /// </param>
    /// <param name="Service">
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
        [FromServices] IPaymentFrequencyService Service,
        [FromServices] ILogger<Endpoint<PaymentFrequency>> Logger,
        [FromServices] HttpContext HttpContext,
        [FromServices] LinkGenerator LinkGenerator)
    {
        Logger.LogInformation("EndPoint[POST]: /Api/PaymentFrequency");
        var Result = await Service.InsertPaymentFrequencyAsync(Request).ToResponseAsync();

        // References:
        //  https://github.com/dotnet/aspnetcore/issues/45101
        //  https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core

        var PaymentFrequencyLink = LinkGenerator.GetUriByName(
            HttpContext, nameof(FetchPaymentFrequencyByIdAsync),
            new RouteValueDictionary(new KeyValuePair<string, object?>[] { new("PaymentFrequencyId", Result.Data.Id) }));

        return TypedResults.Created(PaymentFrequencyLink, Result);
    }
}
