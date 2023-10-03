namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Services;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests;
using SysCredit.Api.Requests.Guarantors;
using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;

/// <summary>
/// 
/// </summary>
/// <param name="GuarantorService"></param>
[ApiController]
[Route("Api/[Controller]")]
public class GuarantorController(IGuarantorService GuarantorService, ILogger<GuarantorController> Logger) : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(IResponse<CreateGuarantorRequest>))]
    public async Task<ActionResult<IResponse<EntityId>>> InsertGuarantorAsync([FromBody] CreateGuarantorRequest Request)
    {
        Logger.LogInformation("EndPoint[POST]: /Api/Guarantor");
        var Result = await GuarantorService.InsertGuarantorAsync(Request);
        return StatusCode(StatusCodes.Status201Created, Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="GuarantorId">Id de Guarantor</param>
    /// <returns></returns>
    [HttpGet("{GuarantorId}")]
    [ProducesResponseType(typeof(IResponse<GuarantorInfo?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchGuarantorById(long? GuarantorId)
    {
        Logger.LogInformation("Endpoint [Get]: Api/Guarantor/{GuarantorId}",GuarantorId);
        return await GuarantorService.FetchGuarantorByIdAsync(GuarantorId).ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<FetchGuarantor>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<IAsyncEnumerable<FetchGuarantor>>> FetchGuarantorsAsync([FromQuery] PaginationRequest Request)
    {
        if (Request.Offset.HasValue && Request.Limit.HasValue)
        {
            return await GuarantorService.FetchGuarantorAsync(Request).ToResponseAsync();
        }
        else
        {
            return await GuarantorService.FetchGuarantorAsync().ToResponseAsync();
        }
    }

    [HttpDelete("{GuarantorId}")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(IResponse<DeleteGuarantorRequest>))]
    public async Task<ActionResult<IResponse<DeleteGuarantorRequest>>> DeleteGuarantorAsync([FromRoute] DeleteGuarantorRequest Request)
    {
        var Result = await GuarantorService.DeleteGuarantorByIdAsync(Request);
        
        return StatusCode(StatusCodes.Status204NoContent);
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpGet("Search")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<GuarantorInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<IAsyncEnumerable<GuarantorInfo>>> SearchGuarantorAsync([FromQuery] SearchRequest Request)
    {
        return await GuarantorService.SearchGuarantorAsync(Request).ToResponseAsync();
    }
}

