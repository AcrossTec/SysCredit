namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.LoanTypes;
using SysCredit.Api.Requests.PaymentFrequencies;
using SysCredit.Api.Services;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;


/// <summary>
/// 
/// </summary>
[ApiController]
[Route("Api/[Controller]")]
public class PaymentFrequencyController(IPaymentFrequencyService PaymentFrequencyService, ILogger<PaymentFrequencyController> Logger) : ControllerBase
{
    /// <summary>
    /// Este método es un controlador HTTP GET que responde a la ruta "/Api/PaymentFrequency"
    /// Convierte el resultado en una respuesta HTTP, con códigos de estado 200 OK y 500 Internal Server Error definidos.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<LoanTypeInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchPaymentFrequencyAsync()
    {
        Logger.LogInformation("EndPoint[GET]: /Api/PaymentFrequency");
        return await PaymentFrequencyService.FetchPaymentFrequencyAsync().ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="PaymentFrequencyId"></param>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpPut("{PaymentFrequencyId}")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(IResponse<UpdatePaymentFrequencyRequest>))]
    public async Task<IActionResult> UpdatePaymentFrequency([FromRoute] long PaymentFrequencyId, [FromBody] UpdatePaymentFrequencyRequest Request)
    {
        var Result = await PaymentFrequencyService.UpdatePaymentFrequencyAsync(PaymentFrequencyId, Request);

        if(Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(Request));
        }
        else
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }

    /// <summary>
    ///     Esta acción lleva a cabo el borrado lógico de 
    ///     una frecuencia de pago en particular. Con códigos
    ///     de estado 200 OK y 500 Internal Server Error definidos
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpDelete("{PaymentFrequencyId}")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(IResponse<DeleteLoanTypeRequest>))]
    public async Task<IActionResult> PaymentFrequencyTypeAsync([FromRoute] DeletePaymentFrequencyRequest Request)
    {
        var Result = await PaymentFrequencyService.DeletePaymentFrequencyAsync(Request);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(Request));
        }
        else
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }

    /// <summary>
    /// Este método es un controlador HTTP GET que responde a la ruta "/Api/PaymentFrequency/Complete"
    /// Convierte el resultado en una respuesta HTTP, con códigos de estado 200 OK y 500 Internal Server Error definidos.
    /// </summary>
    /// <returns></returns>
    [HttpGet("Complete")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<LoanTypeInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchPaymentFrequencyCompleteAsync()
    {
        Logger.LogInformation("EndPoint[GET]: /Api/PaymentFrequency/Complete");
        return await PaymentFrequencyService.FetchPaymentFrequencyCompleteAsync().ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="PaymentFrequencyId"></param>
    /// <returns></returns>
    [HttpGet("{PaymentFrequencyId}")]
    [ProducesResponseType(typeof(IResponse<PaymentFrequencyInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchPaymentFrequencyByIdAsync(long PaymentFrequencyId)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/FetchPaymentFrequencyById");
        return await PaymentFrequencyService.FetchPaymentFrequencyByIdAsync(PaymentFrequencyId)!.ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(IResponse<CreatePaymentFrequencyRequest>))]
    public async Task<IActionResult> InsertPaymentFrequencyAsync([FromBody] CreatePaymentFrequencyRequest Request)
    {
        var Result = await PaymentFrequencyService.InsertPaymentFrequencyAsync(Request);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(Request));
        }
        else
        {
            return StatusCode(StatusCodes.Status201Created, Result);
        }
    }
}