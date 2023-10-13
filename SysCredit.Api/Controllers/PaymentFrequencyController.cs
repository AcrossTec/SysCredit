namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.PaymentFrequencies;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;


/// <summary>
///     Endpoint para las distintas operaciones relacionadas a la frecuencia de pago
/// </summary>
/// <param name="PaymentFrequencyService">
///     Servicio que tiene toda la funcionalidad y operaciones relacionadas a la frecuencia de pago
/// </param>
/// <param name="Logger">
///     Objeto ILogger para el controlador
/// </param>
[ApiController]
[Route("Api/[Controller]")]
public class PaymentFrequencyController(IPaymentFrequencyService PaymentFrequencyService, ILogger<PaymentFrequencyController> Logger) : ControllerBase
{
    /// <summary>
    ///     Obtiene los registros de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <returns>
    ///     Regresa todos los registros de la tabla <see cref="Models.PaymentFrequency"/>
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<LoanTypeInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<IAsyncEnumerable<LoanTypeInfo>>>> FetchPaymentFrequencyAsync()
    {
        Logger.LogInformation("EndPoint[GET]: /Api/PaymentFrequency");
        return Ok(await PaymentFrequencyService.FetchPaymentFrequencyAsync().ToResponseAsync());
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
    /// <returns>
    ///     Regresa un Http 204
    /// </returns>
    [HttpPut("{PaymentFrequencyId}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdatePaymentFrequencyAsync([FromRoute] long PaymentFrequencyId, [FromBody] UpdatePaymentFrequencyRequest Request)
    {
        Logger.LogInformation("EndPoint[PUT]: /Api/PaymentFrequency/{PaymentFrequencyId}", Request.PaymentFrequencyId);
        await PaymentFrequencyService.UpdatePaymentFrequencyAsync(PaymentFrequencyId, Request);
        return NoContent();
    }

    /// <summary>
    ///     Elimina un registro de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <param name="Request">
    ///    Id del <see cref="Models.PaymentFrequency"/> a eliminar
    /// </param>
    /// <returns>
    ///     Retorna un Http 204
    /// </returns>
    [HttpDelete("{PaymentFrequencyId}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeletePaymentFrequencyAsync([FromRoute] DeletePaymentFrequencyRequest Request)
    {
        Logger.LogInformation("EndPoint[DELETE]: /Api/PaymentFrequency/{PaymentFrequencyId}", Request.PaymentFrequencyId);
        await PaymentFrequencyService.DeletePaymentFrequencyAsync(Request);
        return NoContent();
    }

    /// <summary>
    ///     Obtiene todos los registros completos de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <returns>
    ///     Regresa todos los registros completos de la tabla <see cref="Models.PaymentFrequency"/>
    /// </returns>
    [HttpGet("Complete")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<LoanTypeInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<IAsyncEnumerable<LoanTypeInfo>>>> FetchPaymentFrequencyCompleteAsync()
    {
        Logger.LogInformation("EndPoint[GET]: /Api/PaymentFrequency/Complete");
        return Ok(await PaymentFrequencyService.FetchPaymentFrequencyCompleteAsync().ToResponseAsync());
    }

    /// <summary>
    ///     Obtiene un registro de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <param name="PaymentFrequencyId">
    ///     Id obtenido de la ruta
    /// </param>
    /// <returns>
    ///     Regresa un registro de la tabla <see cref="Models.PaymentFrequency"/>
    /// </returns>
    [HttpGet("{PaymentFrequencyId}")]
    [ProducesResponseType(typeof(IResponse<PaymentFrequencyInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<PaymentFrequencyInfo>>> FetchPaymentFrequencyByIdAsync(long PaymentFrequencyId)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/FetchPaymentFrequencyById");
        return Ok(await PaymentFrequencyService.FetchPaymentFrequencyByIdAsync(PaymentFrequencyId)!.ToResponseAsync());
    }

    /// <summary>
    ///     Crear una nueva frecuencia de pago en la base de datos
    /// </summary>
    /// <param name="Request">
    ///     Datos usado para crear la frecuencia de pago
    /// </param>
    /// <returns>
    ///     Regresa el nuevo Id de la frecuencia de pago creado
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<EntityId>>> InsertPaymentFrequencyAsync([FromBody] CreatePaymentFrequencyRequest Request)
    {
        Logger.LogInformation("EndPoint[POST]: /Api/FetchPaymentFrequencyById");
        var Result = await PaymentFrequencyService.InsertPaymentFrequencyAsync(Request).ToResponseAsync();
        return StatusCode(StatusCodes.Status201Created, Result);
    }
}
