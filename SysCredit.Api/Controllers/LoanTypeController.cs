namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.LoanType;
using SysCredit.Api.Requests.LoanTypes;

using SysCredit.Helpers;

/// <summary>
/// 
/// </summary>
/// <param name="LoanTypeService"></param>
[ApiController]
[Route("Api/[Controller]")]
public class LoanTypeController(ILoanTypeService LoanTypeService, ILogger<LoanTypeController> Logger) : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<LoanTypeInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchLoanTypeAsync()
    {
        Logger.LogInformation("EndPoint[GET]: /Api/LoanType");
        return await LoanTypeService.FetchLoanTypeAsync().ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("Complete")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<LoanTypeInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchLoanTypeComplete()
    {
        return await LoanTypeService.FetchLoanTypeCompleteAsync().ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="LoanTypeId"></param>
    /// <returns></returns>
    [HttpGet("{LoanTypeId}")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<LoanTypeInfo?>), StatusCodes.Status200OK)]
    public async Task<IResponse> FetchLoanTypeByIdAsync(long? LoanTypeId)
    {
        return await LoanTypeService.FetchLoanTypeByIdAsync(LoanTypeId).ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpDelete("{LoanTypeId}")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(IResponse<DeleteLoanTypeRequest>))]
    public async Task<IActionResult> DeleteLoanTypeAsync([FromRoute] DeleteLoanTypeRequest Request)
    {
        var Result = await LoanTypeService.DeleteLoanTypeAsync(Request);

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
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(IResponse<CreateLoanTypeRequest>))]
    public async Task<IActionResult> InsertLoanTypeAsync([FromBody] CreateLoanTypeRequest Request)
    {
        var Result = await LoanTypeService.InsertLoanTypeAsync(Request);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(Request));
        }
        else
        {
            return StatusCode(StatusCodes.Status201Created, Result);
        }
    }

    /// <summary>
    ///     Actualiza un LoanType por su id
    /// </summary>
    /// <param name="Request">Contiene parametros necesarios para modificar</param>
    /// <param name="LoanTypeId">Id del LoanType</param>
    /// <returns>Retorna un mensaje de respuesta si fue correcta o hubo una excepción</returns>
    [HttpPut("{LoanTypeId}")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(IResponse<UpdateLoanTypeRequest>))]
    public async Task<IActionResult> UpdateLoanTypeAsync([FromRoute] long LoanTypeId, [FromBody] UpdateLoanTypeRequest Request)
    {
        var Result = await LoanTypeService.UpdateLoanTypeAsync(LoanTypeId, Request);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(Request));
        }
        else
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
